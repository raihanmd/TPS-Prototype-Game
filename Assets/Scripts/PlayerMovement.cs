using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] private float rotationSpeed = 5f;
	[SerializeField] private Transform cameraTransform;
	[SerializeField] private CharacterController controller;

	private Animator animator;
	private Vector3 movement;
	private float moveSpeed;

	void Start() {
		animator = GetComponent<Animator>();
	}

	void Update() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 cameraForward = cameraTransform.forward;
		Vector3 cameraRight = cameraTransform.right;

		cameraForward.y = 0;
		cameraRight.y = 0;

		cameraForward.Normalize();
		cameraRight.Normalize();

		movement = moveVertical * cameraForward + moveHorizontal * cameraRight;
		movement = Vector3.ClampMagnitude(movement, 1f);

		moveSpeed = Input.GetKey(KeyCode.LeftShift) ? 7f : 4f;

		float speed = movement.magnitude;
		animator.SetFloat("speed", speed * moveSpeed);

		if (movement != Vector3.zero) {
			Quaternion targetRotation = Quaternion.LookRotation(movement);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
		}
		controller.Move(moveSpeed * Time.deltaTime * movement);
	}

}
