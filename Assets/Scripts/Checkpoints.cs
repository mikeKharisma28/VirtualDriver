using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class Checkpoints : MonoBehaviour {
    static CarCheckpoints playerTransform;
    static Text textCurrentLap;
    static Text textTotalLaps;
    public static bool isAccomplished = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<CarCheckpoints>();
        textCurrentLap = GameObject.Find("CurrentLap").GetComponent<Text>();
        textTotalLaps = GameObject.Find("TotalLap").GetComponent<Text>();
        GameObject.Find("Checkpoint 1").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("New Text 1").GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("Checkpoint 2").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("New Text 2").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Checkpoint 3").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("New Text 3").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("Checkpoint 4").GetComponent<MeshRenderer>().enabled = false;
        GameObject.Find("New Text 4").GetComponent<MeshRenderer>().enabled = false;
    }

    void OnTriggerEnter()
    {
        if (transform == playerTransform.checkPointArray[CarCheckpoints.currentCheckpoint].transform)
        {
            if (CarCheckpoints.currentCheckpoint + 1 < playerTransform.checkPointArray.Length)
            {
                if (CarCheckpoints.currentCheckpoint == 0)
                    CarCheckpoints.currentLap++;
                CarCheckpoints.currentCheckpoint++;
                VisualAid(CarCheckpoints.currentCheckpoint);
                int totalLaps;
                int.TryParse(textTotalLaps.text, out totalLaps);
                if (CarCheckpoints.currentLap > totalLaps)
                {
                    Instance instance = new Instance();
                    instance.FindPlayerAndDisable();

                    Camera[] cameras = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Camera>();
                    for (int i = 0; i < cameras.Length; i++)
                    {
                        cameras[i].GetComponent<BlurOptimized>().enabled = true;
                    }
                    GameObject.Find("MainCamera").GetComponent<Camera>().GetComponent<BlurOptimized>().enabled = true;

                    playerTransform.GetComponent<CarController>().Move(0);
                    playerTransform.GetComponent<CarController>().ApplyBraking(-1, 0);
                    playerTransform.GetComponent<LightsManager>().LightsManagerInput(-1, playerTransform.GetComponent<CarController>().gearIndex);

                    isAccomplished = true;
                    for (int i = 0; i < playerTransform.checkPointArray.Length; i++)
                    {
                        playerTransform.GetComponent<CarCheckpoints>().checkPointArray[i].GetComponent<MeshRenderer>().enabled = false;
                        //playerTransform.GetComponent<CarCheckpoints>().checkPointArray[i].Find("New Text " + i).GetComponent<MeshRenderer>().enabled = false;
                    }
                }
            }
            else
            {
                CarCheckpoints.currentCheckpoint = 0;
                VisualAid(CarCheckpoints.currentCheckpoint);
            }
        }
    }

    void Update()
    {
        textCurrentLap.text = CarCheckpoints.currentLap.ToString();
    }

    void VisualAid(int currentCheckpoint)
    {
        for (int i = 0; i < playerTransform.checkPointArray.Length; i++)
        {
            playerTransform.checkPointArray[i].GetComponent<MeshRenderer>().enabled = false;
            GameObject.Find("New Text " + (i + 1).ToString()).GetComponent<MeshRenderer>().enabled = false;
        }

        playerTransform.checkPointArray[currentCheckpoint].GetComponent<MeshRenderer>().enabled = true;
        GameObject.Find("New Text " + (currentCheckpoint + 1).ToString()).GetComponent<MeshRenderer>().enabled = true;
    }
}