using UnityEngine;
using System;
using System.Diagnostics;
using RSUnityToolkit;

public class SteeringAction : BaseAction {

	#region Public Fields	

	public float RotateDumpingFactor = 0.5f;	

	public float SmoothingFactor = 0;
	
	public SmoothingUtility.SmoothingTypes SmoothingType = SmoothingUtility.SmoothingTypes.Weighted;
	
	#endregion

	#region Private Fields

	private float _xRot = 0f;
	private float _yRot = 0f;
	private float _zRot = 0f;

	private bool _actionTriggered = false;
	
	private SmoothingUtility _rotationSmoothingUtility = new SmoothingUtility();

    private float steerInput = 0f;
	#endregion
	
	#region CTOR	
	public SteeringAction() : base()
	{

	}	
	
	#endregion	

	#region Public Methods	
	/// <summary>
	/// Determines whether this instance is support custom triggers.
	/// </summary>	
	public override bool IsSupportCustomTriggers()
	{
		return false;
	}
	
	/// <summary>
	/// Returns the actions's description for GUI purposes.
	/// </summary>
	/// <returns>
	/// The action description.
	/// </returns>
	public override string GetActionDescription()
	{ 
		return "This Action steer the car.";
	}
	
	/// <summary>
	/// Sets the default trigger values (for the triggers set in SetDefaultTriggers() )
	/// </summary>
	/// <param name='index'>
	/// Index of the trigger.
	/// </param>
	/// <param name='trigger'>
	/// A pointer to the trigger for which you can set the default rules.
	/// </param>
	public override void SetDefaultTriggerValues(int index, Trigger trigger)
	{				
		switch (index)
		{
		case 0:
			trigger.FriendlyName = "Start Event";
			((EventTrigger)trigger).Rules = new BaseRule[1] { AddHiddenComponent<HandClosedRule>() };
			break;
		case 1:
			((SteeringTrigger)trigger).Rules = new BaseRule[1] {AddHiddenComponent<SteeringRule>() };
			break;
		case 2:
			trigger.FriendlyName = "Stop Event";
			((EventTrigger)trigger).Rules = new BaseRule[1] { AddHiddenComponent<HandOpennedRule>() };
			break;
		}
	}
	
	#endregion
	
	#region Protected Methods	
	
	/// <summary>
	/// Sets the default triggers for that action.
	/// </summary>
	protected override void SetDefaultTriggers()
	{	
		SupportedTriggers = new Trigger[3]{
		AddHiddenComponent<EventTrigger>(),
		AddHiddenComponent<SteeringTrigger>(),
		AddHiddenComponent<EventTrigger>()};				
	}

    #endregion

    #region Private Methods
    Stopwatch stopwatch = new Stopwatch();
    void Update() 
	{
        GetComponent<CarController>().Steer(steerInput);
		ProcessAllTriggers();

        //Start Event
        if ( !_actionTriggered && SupportedTriggers[0].Success )
		{			
			_actionTriggered = true;
            stopwatch.Start();
            ((SteeringTrigger)SupportedTriggers[1]).Restart = true;
			return;
		}

        if (steerInput != 0f)
        {
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            UnityEngine.Debug.Log(ts.Milliseconds);
            stopwatch.Reset();
        }

        //Stop Event
        if ( _actionTriggered && SupportedTriggers[2].Success )
		{
            _actionTriggered = false;
            steerInput = 0f;
        }
		
		if ( !_actionTriggered )
		{
			return;
		}		
		
		SteeringTrigger trgr = (SteeringTrigger)SupportedTriggers[1];

		if( trgr.Success )
		{				
			if (RotateDumpingFactor == 0)
			{
				UnityEngine.Debug.LogError("RotateDumpingFactor must not be zero. Changing it to 1");
				RotateDumpingFactor = 1;
			}

            _zRot = -trgr.Roll / RotateDumpingFactor;

            if (SmoothingFactor > 0)
			{
				Vector3 vec = new Vector3(_xRot,_yRot,_zRot);
				
				vec = _rotationSmoothingUtility.ProcessSmoothing(SmoothingType, SmoothingFactor, vec);
				_zRot = vec.z;
			}
			
			//Set Axis && Rotate
            _zRot = Mathf.Clamp(_zRot, -1 * GetComponent<CarController>().maxSteeringAngle, GetComponent<CarController>().maxSteeringAngle);
            steerInput = -1 * (_zRot / GetComponent<CarController>().maxSteeringAngle);
		}		
	}
	
	#endregion
	
	#region Menu
	#if UNITY_EDITOR
	
	[UnityEditor.MenuItem ("Toolkit/Add Action/Steering")]
	static void AddThisAction () 
	{
		AddAction<SteeringAction>();
	} 
	
	#endif
	#endregion
}
 