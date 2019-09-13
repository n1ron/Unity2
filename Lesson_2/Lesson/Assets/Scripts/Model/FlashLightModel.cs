using UnityEngine;

namespace Geekbrains
{
	public sealed class FlashLightModel : BaseObjectScene
	{
		public Light Light;
        public Transform GoFollow;
        public Vector3 VecOffset;
        public float BatteryChargeCurrent;
        public float Speed = 10;
        private float _batteryChargeMax = 50;
        public float Intensity = 1.5f;
        public float Share;
        public float TakeAwayTheIntensity;

		public float Charge => BatteryChargeCurrent / BatteryChargeMax;

		public float BatteryChargeMax => _batteryChargeMax;

		protected override void Awake()
		{
			base.Awake();
			Light = GetComponent<Light>();

            GoFollow = Camera.main.transform;
            VecOffset = transform.position - GoFollow.position;
			BatteryChargeCurrent = BatteryChargeMax;
			Light.intensity = Intensity;
            Share = BatteryChargeMax / 4;
            TakeAwayTheIntensity = Intensity / (BatteryChargeMax * 100);
		}
	}
}