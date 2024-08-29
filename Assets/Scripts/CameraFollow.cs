using UnityEngine;

public class CameraFollow : MonoBehaviour {
    Transform targetPlayer;
    Transform minimap;

	// Use this for initialization
	void Start () {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        minimap = GameObject.Find("Minimap").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(targetPlayer.position.x, targetPlayer.position.y + 10f, targetPlayer.position.z);
        transform.rotation = Quaternion.Euler(90, targetPlayer.eulerAngles.y, 0);
    }
}