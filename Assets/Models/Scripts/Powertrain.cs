using UnityEngine;

public enum CarDriveTrainType {
	RearWheelDrive,
	FrontWheelDrive,
	FourWheelDrive
}

public class Powertrain : MonoBehaviour {
	public CarDriveTrainType m_CarDriveType = CarDriveTrainType.FrontWheelDrive;
	public float[] RPMForTorque = new float[5];
	public float[] torqueAtRPM = new float[5];
	public AnimationCurve torqueCurve;
	private Keyframe[] torquePoints;

	public int[] gears = new int[8];
	[SerializeField] private float[] gearRatios = new float[8];
    public float[] speedLimitsPerGear;
	public AnimationCurve gearRatiosCurve;
	private Keyframe[] gearIndex;

	public float finalGearRatio;
	public bool ABS;
	public bool TractionControl;
    public static bool isAutomatic = false;
	// Use this for initialization

    public Powertrain()
    {
		finalGearRatio = 0f;
		ABS = false;
	}

	void Start () {
		torquePoints = new Keyframe[5];
		torquePoints [0] = new Keyframe (RPMForTorque[0], torqueAtRPM[0]);
		torquePoints [1] = new Keyframe (RPMForTorque[1], torqueAtRPM[1]);
		torquePoints [2] = new Keyframe (RPMForTorque[2], torqueAtRPM[2]);
		torquePoints [3] = new Keyframe (RPMForTorque[3], torqueAtRPM[3]);
		torquePoints [4] = new Keyframe (RPMForTorque[4], torqueAtRPM[4]);
		torqueCurve = new AnimationCurve (torquePoints);
		//0 is for reverse gear, 1 is for neutral. The rest gears are forward
		gearIndex = new Keyframe[gears.Length];
		for (int i = 0; i < gears.Length; i++) {
			gearIndex [i] = new Keyframe (gears[i], gearRatios[i]);
		}
		gearRatiosCurve = new AnimationCurve (gearIndex);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
