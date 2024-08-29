using UnityEngine;

// Simple class to controll sounds of the car, based on engine throttle and RPM, and skid velocity.
//[RequireComponent (typeof (Drivetrain))]
[RequireComponent (typeof (CarController))]
public class SoundController : MonoBehaviour {
public float MIN = 0.5f;
	
	public AudioClip skid;
	

	AudioSource skidSource;
	
	//CarController1 car;
	//Drivetrain drivetrain;
	
	AudioSource CreateAudioSource (AudioClip clip) {
		GameObject go = new GameObject("audio");
		go.transform.parent = transform;
		go.transform.localPosition = Vector3.zero;
		go.transform.localRotation = Quaternion.identity;
		go.AddComponent(typeof(AudioSource));
		go.GetComponent<AudioSource>().clip = clip;
		go.GetComponent<AudioSource>().loop = true;
		go.GetComponent<AudioSource>().volume = 0;
		go.GetComponent<AudioSource>().Play();
		return go.GetComponent<AudioSource>();
	}
	
	void Start () {
		
		//engineSource = CreateAudioSource(engine);
		skidSource = CreateAudioSource(skid);
		//car = GetComponent (typeof (CarController1)) as CarController1;
		//drivetrain = GetComponent (typeof (Drivetrain)) as Drivetrain;
	}
	
	void Update () {
		var engineSource = GetComponent<AudioSource>();
		//engineSource.pitch = MIN + 1.3f * drivetrain.rpm / drivetrain.maxRPM;

		//skidSource.volume = Mathf.Clamp01( Mathf.Abs(car.slipVelo) * 0.2f - 0.5f );
	}
}
