using UnityEngine;
using System.Collections;

public class CarSound : MonoBehaviour {
    private AudioSource carSound;

    private const float lowPitchMin = 1f;
    private const float lowPitchMax = 6f;
    private const float lowPitch = 0.35f;
    private const float highPitch = 1f;

    private const float reductionFactor = 0.1f;
    
    Rigidbody carRigidBody;
	// Use this for initialization
	void Awake () {
        carSound = GetComponent<AudioSource>();
        carRigidBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // The pitch is interpolated between the min and max values, according to the car's revs.
        float pitch = ULerp(lowPitchMin, lowPitchMax, Mathf.Clamp((GetComponent<CarController>().motorRPM / 10000f), 0, 1));
        pitch = Mathf.Min(lowPitchMax, pitch);

        // clamp to minimum pitch (note, not clamped to max for high revs while burning out)
        pitch = Mathf.Min(lowPitchMax, pitch);
        carSound.pitch = pitch * 1f * 0.3f;
	}

    // unclamped versions of Lerp and Inverse Lerp, to allow value to exceed the from-to range
    private static float ULerp(float from, float to, float value)
    {
        return (1.0f - value) * from + value * to;
    }
}
