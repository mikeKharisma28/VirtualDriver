using UnityEngine;

public class CarCheckpoints : MonoBehaviour {
    public Transform[] checkPointArray;
    public static int currentCheckpoint = 0;
    public static int currentLap = 0;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < checkPointArray.Length; i++)
        {
            checkPointArray[i] = GameObject.Find("Checkpoint " + (i + 1).ToString()).GetComponent<Transform>();
        }
	}

    // Update is called once per frame
    void Update()
    {

	}
}
