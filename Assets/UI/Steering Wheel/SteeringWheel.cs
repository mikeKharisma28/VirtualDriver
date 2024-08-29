using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SteeringWheel : MonoBehaviour {
	public RawImage steeringWheel;
	public WheelCollider correspondingCollider; //could be left or right front wheel.

	// Use this for initialization
	void Start () {
		steeringWheel = GameObject.Find ("SteeringWheel").GetComponent<RawImage> ();
	}
	
	// Update is called once per frame
	void Update () {
		float steerAngle = 0f;
		steerAngle = -correspondingCollider.steerAngle * 2.5f;
		steeringWheel.transform.eulerAngles = new Vector3 (0, 0, steerAngle);
	}
}
