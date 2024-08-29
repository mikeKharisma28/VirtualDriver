using UnityEngine;

public class WheelAlignment : MonoBehaviour {
	public WheelCollider correspondingCollider;
	public Transform brakeMesh;
	private float rotationValue = 0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	//void FixedUpdate () {

	//	// define a hit point for the raycast collision
	//	RaycastHit hit;

	//	/* Find the collider's center point, you need to do this because the center of the collider 
	//	 * might not actually be the real position if the transform's off.
	//	 */
	//	Vector3 colliderCenterPoint = correspondingCollider.transform.TransformPoint (correspondingCollider.center);

	//	/* Now cast a ray out from the wheel collider's center the distance of the suspension, if 
	//	 * it hits something, then use the "hit" variable's data to find where the wheel hit, if it 
	//	 * didn't, then set the wheel to be fully extended along the suspension.
	//	 */
	//	if (Physics.Raycast (colliderCenterPoint, -correspondingCollider.transform.up, out hit, 
	//	                    correspondingCollider.suspensionDistance + correspondingCollider.radius)) {
	//		transform.position = hit.point + (correspondingCollider.transform.up * 
	//			correspondingCollider.radius);
	//		brakeMesh.position = hit.point + (correspondingCollider.transform.up * 
	//			correspondingCollider.radius);
	//	} else {
	//		transform.position = colliderCenterPoint - (correspondingCollider.transform.up * (correspondingCollider.radius));
 //           brakeMesh.position = colliderCenterPoint - (correspondingCollider.transform.up * (correspondingCollider.radius));
	//	}

	//	/* Now set the wheel rotation to the rotation of the collider combined with a new
	//	 * rotation value. This new value is the rotation around the axle, and the rotation from 
	//	 * steering input.
	//	 */
	//	transform.rotation = correspondingCollider.transform.rotation * 
	//		Quaternion.Euler (rotationValue, correspondingCollider.steerAngle, 0);
	//	brakeMesh.rotation = correspondingCollider.transform.rotation * 
	//		Quaternion.Euler (0, correspondingCollider.steerAngle, 0);

	//	// Increase the rotation value by the rotation speed (in degrees per second)
	//	rotationValue += correspondingCollider.rpm * (360 / 60) * Time.deltaTime;

	//	/* Define a wheelhit object, this stores all of the data from the wheel collider and will
	//	 * allow us to determine the slip of the tire.
	//	 */
	//	WheelHit correspondingGroundHit;
	//	correspondingCollider.GetGroundHit (out correspondingGroundHit);

	//	/* if the slip of the tire is greater than 2.0, and the slip prefab exists, create an instance
	//	 * of it on the ground at a zero rotation.
	//	 */
	//}

    void FixedUpdate()
    {
        Quaternion q;
        Vector3 p;

        correspondingCollider.GetWorldPose(out p, out q);
        transform.position = p;
        transform.rotation = q;

        brakeMesh.position = p;
        brakeMesh.rotation = correspondingCollider.transform.rotation * Quaternion.Euler(0, correspondingCollider.steerAngle, 0);
    }
}
