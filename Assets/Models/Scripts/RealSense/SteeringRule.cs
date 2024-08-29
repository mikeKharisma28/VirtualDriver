using UnityEngine;
using System;

namespace RSUnityToolkit
{
    [AddComponentMenu("")]
	[SteeringTrigger.SteeringTriggerAtt]
    public class SteeringRule : BaseRule
    {
        #region Public Properties

        public float DistanceThresholdX = 0f;

        public float DistanceThresholdY = 0f;

        public float DistanceThresholdZ = 0f;

        #endregion

        #region Private Properties

        private float _roll, _pitch, _yaw;

        #endregion

        #region C'tor

        public SteeringRule(): base()
        {
            FriendlyName = "Two Hands Steering Rule";
        }

        #endregion

        #region Public Methods

       	override public string GetIconPath()
		{
			return @"RulesIcons/two-hands-tracking";			
		}
		
        protected override bool OnRuleEnabled()
        {
            SenseToolkitManager.Instance.SetSenseOption(SenseOption.SenseOptionID.Hand);
            return true;
        }
		
		protected override void OnRuleDisabled()
		{
			SenseToolkitManager.Instance.UnsetSenseOption(SenseOption.SenseOptionID.Hand);
		}

        public override string GetRuleDescription()
        {
            return "Track the user's hands to calculate rotation values to steer";
        }

        public override bool Process(Trigger trigger)
        {
            trigger.ErrorDetected = false;
            if (!SenseToolkitManager.Instance.IsSenseOptionSet(SenseOption.SenseOptionID.Hand))
            {
                trigger.ErrorDetected = true;
                Debug.LogError("Hand Analysis Module Not Set");
                return false;
            }

            if (!(trigger is SteeringTrigger))
            {
                trigger.ErrorDetected = true;
                return false;
            }

            #region Steer
            if (trigger is SteeringTrigger)
            {
                //AcquireFrame
                if (SenseToolkitManager.Instance.Initialized && SenseToolkitManager.Instance.HandDataOutput != null)
                {
                    if (SenseToolkitManager.Instance.HandDataOutput.QueryNumberOfHands() > 1)
                    {
                        PXCMHandData.IHand leftHand = null;
                        PXCMHandData.IHand rightHand = null;

                        //Query both hands
                        if (SenseToolkitManager.Instance.HandDataOutput.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_LEFT_HANDS, 0, out leftHand) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                        {
                            if (SenseToolkitManager.Instance.HandDataOutput.QueryHandData(PXCMHandData.AccessOrderType.ACCESS_ORDER_RIGHT_HANDS, 0, out rightHand) >= pxcmStatus.PXCM_STATUS_NO_ERROR)
                            {
                                SteeringTrigger steeringTrig = (SteeringTrigger)trigger;
                                float yCurDiff = rightHand.QueryMassCenterWorld().y * 100 - leftHand.QueryMassCenterWorld().y * 100;
                                float zCurDiff = rightHand.QueryMassCenterWorld().z * 100 - leftHand.QueryMassCenterWorld().z * 100;

                                //Initialization
                                if (steeringTrig.Restart)
                                {
                                    _roll = yCurDiff;
                                    _pitch = (float)(Math.Atan2(yCurDiff, zCurDiff));
                                    _yaw = zCurDiff;
                                    steeringTrig.Restart = false;
                                }

                                steeringTrig.Roll = yCurDiff - _roll;
                                steeringTrig.Roll *= -1;
                                if (Math.Abs(steeringTrig.Roll) < DistanceThresholdX)
                                {
                                    steeringTrig.Roll = 0;
                                }

                                steeringTrig.Pitch = (float)(Math.Atan2(yCurDiff, zCurDiff) - _pitch);
                                if (Math.Abs(steeringTrig.Pitch) < DistanceThresholdZ)
                                {
                                    steeringTrig.Pitch = 0;
                                }

                                steeringTrig.Yaw = zCurDiff - _yaw;

                                if (Math.Abs(steeringTrig.Yaw) < DistanceThresholdY)
                                {
                                    steeringTrig.Yaw = 0;
                                }

                                if ((steeringTrig.Roll == steeringTrig.Yaw) && (steeringTrig.Yaw == steeringTrig.Pitch) && (steeringTrig.Pitch == 0))
                                {
                                    return false;
                                }

                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            #endregion

            return false;
        }
        #endregion
    }
}