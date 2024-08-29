using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Globalization;

public class RPMMeter : MonoBehaviour {
	public RawImage rpmNeedle;
	Powertrain powertrain; 
	CarController carController;

	public Text unitUI;
	public Text speedUI;
	public Text gearUI;

	public float maxRPM;
	public float maxRPMAngle = 315.0f;
	public float zeroRPMAngle = 225.0f;

	public bool isMetric = true;
	private int speed;
	private int gear;
	private string gearText;
	private float rpm;

	private string[] gears;

	// Use this for initialization
	void Start () {
		powertrain = GetComponent (typeof(Powertrain)) as Powertrain;
        carController = GetComponent(typeof(CarController)) as CarController;

		rpmNeedle = GameObject.Find ("Needle").GetComponent<RawImage> ();
		unitUI = GameObject.Find ("Unit").GetComponent<Text> ();
		speedUI = GameObject.Find ("Speed").GetComponent<Text> ();
		gearUI = GameObject.Find ("Gear").GetComponent<Text> ();
		gears = new string[powertrain.gears.Length];
		for (int i = 0; i < powertrain.gears.Length; i++) {
			switch (powertrain.gears[i]) {
			case -1:
				gears[i] = "R";
				break;
			case 0:
				gears[i] = "N";
				break;
			default:
                if (!Powertrain.isAutomatic)
                    gears[i] = powertrain.gears[i].ToString();
                else
                    gears[i] = "D" + powertrain.gears[i].ToString();
				break;
			}
		}
		maxRPM = powertrain.RPMForTorque[4];
        gearText = gears[carController.gearIndex + 1];
	}
	
	// Update is called once per frame
	void Update () {
		SetMeter ();
	}

	private void SetMeter() {
		switch (isMetric) {
		case true:
			unitUI.text = "km/h";
			break;
		case false:
			unitUI.text = "mph";
			break;
		}

		gearUI.text = gearText;
		speedUI.text = speed.ToString();

		float offset = 360 - maxRPMAngle;
		float zeroRPMOffsetAngle = zeroRPMAngle + offset;

		float offsetAngle = zeroRPMOffsetAngle - (zeroRPMOffsetAngle * (rpm / maxRPM));
		float angle = offsetAngle - offset;

		float rotateAngle = 0.0f;
		if (angle <= 0) {
			rotateAngle = 360.0f + angle;
		} else {
			rotateAngle = angle;
		}

        rpmNeedle.transform.eulerAngles = new Vector3(0, 0, rotateAngle);
	}

	public void SetRPM(float currentRPM) {
		rpm = currentRPM;
	}

	public void SetSpeed(float currentSpeed) {
		if (isMetric == false) {
			currentSpeed *= 0.621371f;
		}
		speed = Mathf.RoundToInt (currentSpeed);
	}

	public void SetGear(int currentGear) {
		gear = currentGear + 1;
		gearText = gears [gear];
	}
}
