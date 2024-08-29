using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class RepositionCheckpoints : MonoBehaviour {
    public Transform[] parkingCheckpointsArray;
    public GameObject startingPointCones;
    public Text statusCheckpoints;
    public static bool isAccomplished = false;
    public static int currentCheckpoint = 0;

    void Start()
    {
        for (int i = 0; i < parkingCheckpointsArray.Length; i++)
        {
            parkingCheckpointsArray[i] = GameObject.Find("Checkpoint " + (i + 1).ToString()).GetComponent<Transform>();
        }
        VisualAid();
    }

    void OnTriggerEnter()
    {
        if (transform == parkingCheckpointsArray[currentCheckpoint].GetComponent<RepositionCheckpoints>().parkingCheckpointsArray[currentCheckpoint].transform)
        {
            if (currentCheckpoint + 1 < parkingCheckpointsArray.Length)
            {
                currentCheckpoint++;
                statusCheckpoints.text = currentCheckpoint.ToString();
                if (currentCheckpoint == 2)
                    startingPointCones.SetActive(false);
                VisualAid();
            }
            else
            {
                parkingCheckpointsArray[2].GetComponent<MeshRenderer>().enabled = false;
                isAccomplished = true;
                Camera[] cameras = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Camera>();
                for (int i = 0; i < cameras.Length; i++)
                {
                    cameras[i].GetComponent<BlurOptimized>().enabled = true;
                }
                GameObject.Find("MainCamera").GetComponent<Camera>().GetComponent<BlurOptimized>().enabled = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().Move(0);
                GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().ApplyBraking(-1, 0);
                GameObject.FindGameObjectWithTag("Player").GetComponent<LightsManager>().LightsManagerInput(-1, GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>().gearIndex);
            }
        }
    }

    void VisualAid()
    {
        for (int i = 0; i < parkingCheckpointsArray.Length; i++)
        {
            parkingCheckpointsArray[i].GetComponent<MeshRenderer>().enabled = false;
        }
        
        if (currentCheckpoint + 1 <= parkingCheckpointsArray.Length)
            parkingCheckpointsArray[currentCheckpoint].GetComponent<MeshRenderer>().enabled = true;
    }
}