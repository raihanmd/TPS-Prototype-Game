using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	[SerializeField] private float moveSpeed;
	[SerializeField] private float groundYOffset;
	[SerializeField] private LayerMask groundmask;
	[SerializeField] private float gravity = -9.81f;

	[HideInInspector] public Vector3 direction;
	private CharacterController controller;
	private float hInput, vInput;
	private Animator animator;
	private Vector3 spherePos, velocity;

	private void Start() {
		animator = GetComponent<Animator>();
		controller = GetComponent<CharacterController>();
	}

	private void Update() {
		GetDirectionAndMove();
		Gravity();
	}

	private void GetDirectionAndMove() {
		hInput = Input.GetAxis("Horizontal");
		vInput = Input.GetAxis("Vertical");

		direction = transform.forward * vInput + transform.right * hInput;

		animator.SetFloat("speed", moveSpeed * Math.Abs(vInput));

		controller.Move(moveSpeed * Time.deltaTime * direction);
	}

	private bool IsGrounded() {
		spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
		return Physics.CheckSphere(spherePos, controller.radius - .05f, groundmask);
	}

	private void Gravity() {
		if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
		else if (velocity.y < 0) velocity.y = -2;

		controller.Move(velocity * Time.deltaTime);
	}

	private void OnDrawGizmos() {
		if (controller != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(spherePos, controller.radius - .05f);
		}
	}
}
