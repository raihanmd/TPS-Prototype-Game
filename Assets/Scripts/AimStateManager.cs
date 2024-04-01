using UnityEngine;

public class AimStateManager : MonoBehaviour {
    [SerializeField] Cinemachine.AxisState xAxis, yAxis;
    [SerializeField] Transform camFollowPos;

    void Update() {
        xAxis.Update(Time.deltaTime);
        yAxis.Update(Time.deltaTime);
    }

    private void LateUpdate() {
        camFollowPos.localEulerAngles = new Vector3(yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);
    }
}
