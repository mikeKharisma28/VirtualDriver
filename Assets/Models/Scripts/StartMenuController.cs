using UnityEngine;
public class StartMenuController : MonoBehaviour {
	public WheelCollider wheelFrontLeft;
	public WheelCollider wheelFrontRight;

	// Use this for initialization
	void Start () {
        wheelFrontLeft.steerAngle = -35f;
        wheelFrontRight.steerAngle = -35f;
	}
}
