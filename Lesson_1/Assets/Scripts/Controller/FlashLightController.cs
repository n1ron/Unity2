using UnityEngine;

namespace Geekbrains
{
	public sealed class FlashLightController : BaseController, IOnUpdate, IInitialization
	{
		private FlashLightModel _flashLightModel;
		private FlashLightUi _flashLightUi;

		public void Init()
		{
			_flashLightModel = Object.FindObjectOfType<FlashLightModel>();
			_flashLightUi = Object.FindObjectOfType<FlashLightUi>();
		}

		public override void On()
		{
			if(IsActive) return;
			if (_flashLightModel == null) return;
			if (_flashLightUi == null) return;
			if (_flashLightModel.BatteryChargeCurrent <= 0) return;
			base.On();
			_flashLightModel.Switch(true);
		}

		public override void Off()
		{
			if (!IsActive) return;
			base.Off();
			_flashLightModel.Switch(false);
		}

		public void OnUpdate()
		{
            if (_flashLightModel.ShowBatteryLevel() < 1) _flashLightUi.SetActive(true);
            else _flashLightUi.SetActive(false);

            _flashLightUi.FillAmount = _flashLightModel.ShowBatteryLevel();

            if (!IsActive)
            {
                _flashLightModel.ChargeBattery();                
                return;
            }
			_flashLightModel.Rotation();
			if (_flashLightModel.SpendBattery())
			{
            }
			else
			{
				Off();
			}
        }
	}
}