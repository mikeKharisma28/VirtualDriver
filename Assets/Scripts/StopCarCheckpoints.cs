using UnityEngine;
using UnityStandardAssets.ImageEffects;

class StopCarCheckpoints : MonoBehaviour {
    public GameObject player;
    CarController controller;
    public static bool isAccomplished = false;

	void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<CarController>();
	}

    void OnTriggerEnter()
    {
        Instance instance = new Instance();
        instance.FindPlayerAndDisable();

        controller.Move(0);
        controller.ApplyBraking(-1, 0);
    }

    void OnTriggerStay()
    {
        controller.Move(0);
        controller.ApplyBraking(-1, 0);
        player.GetComponent<LightsManager>().LightsManagerInput(-1, controller.gearIndex);

        if ((player.GetComponent<Rigidbody>().velocity.magnitude * 3.6f) < 1)
        {
            isAccomplished = true;
            Camera[] cameras = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Camera>();
            for (int i = 0; i < cameras.Length; i++)
            {
                cameras[i].GetComponent<BlurOptimized>().enabled = true;
            }
            GameObject.Find("MainCamera").GetComponent<Camera>().GetComponent<BlurOptimized>().enabled = true;
        }
    }
}
