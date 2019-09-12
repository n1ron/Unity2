using UnityEngine;

namespace Geekbrains
{
	public sealed class FlashLightModel : BaseObjectScene
	{
		private Light _light;
		private Transform _goFollow;
		private Vector3 _vecOffset;
		public float BatteryChargeCurrent { get; private set; }
		[SerializeField] private float _speed = 10;
		[SerializeField] private float _batteryChargeMax = 5;

		protected override void Awake()
		{
			base.Awake();
			_light = GetComponent<Light>();
			_goFollow = Camera.main.transform;
			_vecOffset = transform.position - _goFollow.position;
			BatteryChargeCurrent = _batteryChargeMax;
		}

		public void Switch(bool value)
		{
			_light.enabled = value;
			if (!value) return;
			transform.position = _goFollow.position + _vecOffset;
			transform.rotation = _goFollow.rotation;
		}

		public void Rotation()
		{
			transform.position = _goFollow.position + _vecOffset;
			transform.rotation = Quaternion.Lerp(transform.rotation,
				_goFollow.rotation, _speed * Time.deltaTime);
		}

		public bool SpendBattery()
		{
			if (BatteryChargeCurrent > 0)
			{
				BatteryChargeCurrent -= Time.deltaTime;
				return true;
			}
			return false;
		}

        public void ChargeBattery()
        {
            if (BatteryChargeCurrent < _batteryChargeMax) BatteryChargeCurrent += Time.deltaTime;
        }

        public float ShowBatteryLevel()
        {
            return BatteryChargeCurrent / _batteryChargeMax;
        }
	}
}