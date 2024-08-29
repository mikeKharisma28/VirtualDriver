using UnityEngine;
//using System;
//using System.Diagnostics;
using RSUnityToolkit;

public class BrakingAction : VirtualWorldBoxAction {

    #region Public Fields
    [BaseAction.ShowAtFirst]
    public Defaults SetDefaultsTo = Defaults.HandTracking;
    public static float brakeInput = 0.0f;
    #endregion

    #region Private Fields
    [SerializeField]
	[HideInInspector]
	private Defaults _lastDefaults = Defaults.HandTracking;

    private bool _actionTriggered = false;

    private float handbrakeInput = 0.0f;

    CarController carController;
    LightsManager lightsManager;
    #endregion

    #region Public methods
    public override bool IsSupportCustomTriggers()
    {
        return false;
    }

    public override string GetActionDescription()
    {
        return "This Action links the transformation of the associated Game Object to the real world tracked source";
    }

    public override void SetDefaultTriggerValues(int index, Trigger trigger)
    {
		if (SetDefaultsTo == Defaults.HandTracking)
		{
	        switch (index)
	        {
	            case 0:
	                trigger.FriendlyName = "Start Event";
	                ((EventTrigger)trigger).Rules = new BaseRule[1] { AddHiddenComponent<HandClosedRule>() };
	                break;
	            case 1:
	                ((TrackTrigger)trigger).Rules = new BaseRule[1] { AddHiddenComponent<HandTrackingRule>() };
	                break;
	            case 2:
	                trigger.FriendlyName = "Stop Event";
	                ((EventTrigger)trigger).Rules = new BaseRule[1] { AddHiddenComponent<HandOpennedRule>() };
	                break;
	        }
		}	
    } 

	public override void UpdateInspector()
	{
		if (_lastDefaults != SetDefaultsTo)
		{
			CleanSupportedTriggers();
	        SupportedTriggers = null;
	        InitializeSupportedTriggers();
			_lastDefaults = SetDefaultsTo;
		}		
	}
	
    #endregion

    #region Protected methods
    override protected void SetDefaultTriggers()
    {
        SupportedTriggers = new Trigger[3]{
			AddHiddenComponent<EventTrigger>(),
			AddHiddenComponent<TrackTrigger>(),
			AddHiddenComponent<EventTrigger>()};			
    }
	
    #endregion	

    #region Private Methods
    void Start()
    {
        carController = GetComponent<CarController>();
        lightsManager = GetComponent<LightsManager>();
    }

    //Stopwatch stopwatch = new Stopwatch();
    void Update()
    {
        handbrakeInput = Input.GetAxis("Handbrake");
        carController.ApplyBraking(brakeInput, handbrakeInput);
        lightsManager.LightsManagerInput(brakeInput, GetComponent<CarController>().gearIndex);
		updateVirtualWorldBoxCenter();
		
	   	ProcessAllTriggers();
        
        //Start Event
        if (!_actionTriggered && SupportedTriggers[0].Success)
        {
            //stopwatch.Start();
            _actionTriggered = true;
        }

        //if (brakeInput == -1)
        //{
        //    stopwatch.Stop();
        //    UnityEngine.Debug.Log(ts.Milliseconds);
        //    TimeSpan ts = stopwatch.Elapsed;
        //    stopwatch.Reset();
        //}

        if (_actionTriggered)
        {
            brakeInput = -1;
        }

        //Stop Event
        if (_actionTriggered && SupportedTriggers[2].Success)
        {
            brakeInput = 0;
            _actionTriggered = false;
        }

        if (!_actionTriggered)
            return;
    }
    #endregion
	
	#region Nested Types
	public enum Defaults
	{
		HandTracking
	}
	#endregion
	
	#region Menu
	#if UNITY_EDITOR
	[UnityEditor.MenuItem ("Toolkit/Add Action/Braking")]
	static void AddThisAction () 
	{
		AddAction<BrakingAction>();
	} 
	
	#endif
	#endregion
	
}
 