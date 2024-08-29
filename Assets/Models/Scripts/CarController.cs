using UnityEngine;

public class CarController : MonoBehaviour {
	public WheelCollider wheelFrontLeft;
	public WheelCollider wheelFrontRight;
	public WheelCollider wheelRearLeft;
	public WheelCollider wheelRearRight;

	public Rigidbody carRigidBody;

	Powertrain carPowertrain;
	RPMMeter rpmMeter;

	public int gearIndex = 0;
	public float velocity;
	public float maxSteeringAngle = 0f;

	private float minRPM = 100f;
	private float maxRPM;
	public float motorRPM;
    public float brakeTorque = 1000f;
    private float totalMotorTorque;

    private float m_OldRotation;
    [Range(0, 1)] [SerializeField] private float m_SteerHelper;
    public float m_Rev;
    [SerializeField] private float m_SlipLimit;
	// Use this for initialization
	void Start () {
		carRigidBody = GetComponent<Rigidbody>();
        carPowertrain = GetComponent(typeof(Powertrain)) as Powertrain;
		rpmMeter = GetComponent(typeof(RPMMeter)) as RPMMeter;
		maxRPM = carPowertrain.RPMForTorque [4];
        rpmMeter.SetRPM(gearIndex);
	}
	
	// Update is called once per frame
	void Update () {
		velocity = carRigidBody.velocity.magnitude * 3.6f;
		rpmMeter.SetSpeed (velocity);
		rpmMeter.SetRPM (motorRPM);
	}

	public void Move(float accel) {
		accel = Mathf.Clamp (accel, 0, m_Rev);
        motorRPM = minRPM + 
            (GetWheelRPM() * carPowertrain.finalGearRatio * carPowertrain.gearRatiosCurve.Evaluate(gearIndex));

		totalMotorTorque = carPowertrain.torqueCurve.Evaluate (motorRPM) * carPowertrain.gearRatiosCurve.Evaluate (gearIndex)
			* carPowertrain.finalGearRatio * accel;

		switch (carPowertrain.m_CarDriveType) {
		case CarDriveTrainType.FrontWheelDrive:
			wheelFrontLeft.motorTorque = (totalMotorTorque / 2) * accel;
			wheelFrontRight.motorTorque = (totalMotorTorque / 2) * accel;
			break;
		case CarDriveTrainType.RearWheelDrive:
			wheelRearLeft.motorTorque = (totalMotorTorque / 2) * accel;
			wheelRearRight.motorTorque = (totalMotorTorque / 2) * accel;
			break;
		case CarDriveTrainType.FourWheelDrive:
			wheelFrontLeft.motorTorque = (totalMotorTorque / 4) * accel;
			wheelFrontRight.motorTorque = (totalMotorTorque / 4) * accel;
			wheelRearLeft.motorTorque = (totalMotorTorque / 4) * accel;
			wheelRearRight.motorTorque = (totalMotorTorque / 4) * accel;
			break;
		}

        SteerHelper();
	}

    public void Steer(float steering)
    {
        steering = Mathf.Clamp(steering, -1, 1);
        wheelFrontLeft.steerAngle = maxSteeringAngle * steering;
        wheelFrontRight.steerAngle = maxSteeringAngle * steering;
    }

 	public void ApplyBraking(float brake, float handbrake) {
        brake = -1 * Mathf.Clamp(brake, -1, 0);
        handbrake = Mathf.Clamp(handbrake, 0, 1);

		if (brake > 0 && handbrake > 0) {
			if (!carPowertrain.ABS) {
                wheelFrontLeft.brakeTorque = brakeTorque * brake;
                wheelFrontRight.brakeTorque = brakeTorque * brake;
				wheelRearLeft.brakeTorque = 100000000 * brake;
                wheelRearRight.brakeTorque = 100000000 * brake;
			} else {
				//braking with ABS
				wheelRearLeft.brakeTorque = 10000 * brake;
				wheelRearRight.brakeTorque = 10000 * brake;
			}

		} else if (brake > 0 && handbrake == 0) {
			if (!carPowertrain.ABS) {
				wheelFrontLeft.brakeTorque = brakeTorque * brake;
				wheelFrontRight.brakeTorque = brakeTorque * brake;
				wheelRearLeft.brakeTorque = brakeTorque * brake;
				wheelRearRight.brakeTorque = brakeTorque * brake;
			} else {
				//braking with ABS
			}

		} else if (brake == 0 && handbrake > 0) {
            wheelRearLeft.brakeTorque = 100000000 * handbrake;
            wheelRearRight.brakeTorque = 100000000 * handbrake;
		} else {
			wheelFrontLeft.brakeTorque = 0 * brake;
			wheelFrontRight.brakeTorque = 0 * brake;
			wheelRearLeft.brakeTorque = 0 * brake;
			wheelRearRight.brakeTorque = 0 * brake;
		}
	}

    public void Transmission(float accel, float brake)
    {
        accel = Mathf.Clamp(accel, 0, m_Rev);
        brake = -1 * Mathf.Clamp(brake, -1, 0);
        if (!Powertrain.isAutomatic)
        {
            if (Input.GetKeyUp(KeyCode.Z))
            {
                ShiftGearUp();
                rpmMeter.SetGear(gearIndex);
            }
            else if (Input.GetKeyUp(KeyCode.X))
            {
                ShiftGearDown();
                rpmMeter.SetGear(gearIndex);
            }
        }
        else
        {
            //condition when a car is not moving or fully stopped 
            if (velocity < 1)
            {
                //condition when throttle is not applied / released
                if (accel < 1 || brake < 0)
                {
                    //what happens if when the car is not at 1st gear
                    if (gearIndex < 1)
                    {
                        if (Input.GetKeyUp(KeyCode.Z))
                        {
                            ShiftGearUp();
                            rpmMeter.SetGear(gearIndex);
                        }
                        else if (Input.GetKeyUp(KeyCode.X))
                        {
                            ShiftGearDown();
                            rpmMeter.SetGear(gearIndex);
                        }
                    }
                    //what happens if when the car is at 1st gear or higher
                    else if (gearIndex >= 1)
                    {
                        if (Input.GetKeyUp(KeyCode.X))
                        {
                            gearIndex = 0;
                            rpmMeter.SetGear(gearIndex);
                        }
                    }
                }
            }
            //condition when car is moving forward
            else if (velocity > 1 && gearIndex > -1)
            {
                switch (brake > -1)
                {
                    case true:
                        //accel is applied
                        if (accel > 0)
                        {
                            if (gearIndex > carPowertrain.speedLimitsPerGear.Length)
                                return;

                            if (velocity >= carPowertrain.speedLimitsPerGear[gearIndex - 1])
                            {
                                ShiftGearUp();
                                rpmMeter.SetGear(gearIndex);
                            }
                        }
                        //accel is released
                        else
                        {
                            if (gearIndex == 1)
                                return;
                            if (velocity <= carPowertrain.speedLimitsPerGear[gearIndex - 2])
                            {
                                ShiftGearDown();
                                rpmMeter.SetGear(gearIndex);
                            }
                        }
                        break;
                    case false:
                        if (accel > 0)
                        {
                            if (gearIndex == 1)
                                return;
                            if (velocity <= carPowertrain.speedLimitsPerGear[gearIndex - 2])
                            {
                                ShiftGearDown();
                                rpmMeter.SetGear(gearIndex);
                            }
                        }
                        else
                        {
                            if (gearIndex == 1)
                                return;
                            if (velocity <= carPowertrain.speedLimitsPerGear[gearIndex - 2])
                            {
                                ShiftGearDown();
                                rpmMeter.SetGear(gearIndex);
                            }
                        }
                        break;
                }
            }
            else
            {
                //do nothing
            }
        }
    }

    float GetWheelRPM()
    {
        float output = 0f;
        switch (carPowertrain.m_CarDriveType)
        {
            case CarDriveTrainType.FrontWheelDrive:
                output = (wheelFrontLeft.rpm + wheelFrontRight.rpm) / 2;
                break;
            case CarDriveTrainType.RearWheelDrive:
                output = (wheelRearLeft.rpm + wheelRearRight.rpm) / 2;
                break;
            case CarDriveTrainType.FourWheelDrive:
                output = (wheelFrontLeft.rpm + wheelFrontRight.rpm + wheelRearLeft.rpm + wheelRearRight.rpm) / 4;
                break;
        }
        return output;
    }

	void ShiftGearUp() {
		if (gearIndex >= carPowertrain.gears.Length - 2) {
			gearIndex = carPowertrain.gears.Length - 2;
		} else {
			gearIndex++;
		}
	}

	void ShiftGearDown() {
		if (gearIndex <= -1) {
			gearIndex = -1;
		} else {
			gearIndex--;
		}
	}

    private void SteerHelper()
    {
        WheelHit wheelHit;

        wheelFrontLeft.GetGroundHit(out wheelHit);
        if (wheelHit.normal == Vector3.zero)
            return;

        wheelFrontRight.GetGroundHit(out wheelHit);
        if (wheelHit.normal == Vector3.zero)
            return;

        wheelRearLeft.GetGroundHit(out wheelHit);
        if (wheelHit.normal == Vector3.zero)
            return;

        wheelRearRight.GetGroundHit(out wheelHit);
        if (wheelHit.normal == Vector3.zero)
            return;
        // this if is needed to avoid gimbal lock problems that will make the car suddenly shift direction
        if (Mathf.Abs(m_OldRotation - transform.eulerAngles.y) < 10f)
        {
            var turnadjust = (transform.eulerAngles.y - m_OldRotation) * m_SteerHelper;
            Quaternion velRotation = Quaternion.AngleAxis(turnadjust, Vector3.up);
            carRigidBody.velocity = velRotation * carRigidBody.velocity;
        }
        m_OldRotation = transform.eulerAngles.y;
    }
}
