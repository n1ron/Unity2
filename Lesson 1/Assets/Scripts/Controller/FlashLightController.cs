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
			_flashLightUi.SetActive(true);
		}

		public override void Off()
		{
			if (!IsActive) return;
			base.Off();
			_flashLightModel.Switch(false);
			_flashLightUi.SetActive(false);
		}

		public void OnUpdate()
		{
			if(!IsActive) return;
			_flashLightModel.Rotation();
			if (_flashLightModel.EditBatteryCharge())
			{
				_flashLightUi.Text = _flashLightModel.BatteryChargeCurrent;
			}
			else
			{
				Off();
			}
		}
	}
}