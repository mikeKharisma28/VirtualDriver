using UnityEngine;

public class KeyboardInput : MonoBehaviour {
    CarController carController;
    LightsManager lightsManager;
	// Use this for initialization
	void Start () {
        carController = GetComponent<CarController>();
        lightsManager = GetComponent<LightsManager>();
	}
	
	// Update is called once per frame
	void Update () {
        carController.Move(Input.GetAxis("Accelerate / Brake"));
        carController.ApplyBraking(Input.GetAxis("Accelerate / Brake"), Input.GetAxis("Handbrake"));
        carController.Steer(Input.GetAxis("Steer"));
        carController.Transmission(Input.GetAxis("Accelerate / Brake"), Input.GetAxis("Accelerate / Brake"));

        lightsManager.LightsManagerInput(Input.GetAxis("Accelerate / Brake"), carController.gearIndex);
	}
}
