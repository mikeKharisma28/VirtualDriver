using UnityEngine;

namespace RSUnityToolkit
{
	[AddComponentMenu("")]
    public class SteeringTrigger : Trigger
    {
		
		public class SteeringTriggerAtt : TriggerAtt
		{
		}
		
        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public float Roll { get; set; }
		
		public bool Restart = false;
		
		protected override string GetAttributeName()
		{
			return typeof(SteeringTriggerAtt).Name;
		}
		
		protected override string GetFriendlyName()
		{
			return "Steering Trigger";
		}

/*		 public override void SetDefaults(BaseAction actionOwner)
        {
            Rules = new BaseRule[1];
            Rules[0] = actionOwner.AddHiddenComponent<TwoHandsInteractionRule>();
        }*/
    }
}