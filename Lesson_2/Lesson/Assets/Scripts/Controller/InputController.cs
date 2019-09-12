using UnityEngine;

namespace Geekbrains
{
	public class InputController : BaseController, IOnUpdate
	{

		private KeyCode _activeFlashLight = KeyCode.F;
		private KeyCode _cancel = KeyCode.Escape;
		private KeyCode _reloadClip = KeyCode.R;

		public InputController()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}
		
		public void OnUpdate()
		{
			if (!IsActive) return;
			if (Input.GetKeyDown(_activeFlashLight))
			{
				Main.Instance.FlashLightController.Switch();
			}
			//todo реализовать выбор оружия по колесику мыши

			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				SelectWeapon(0);
			}

			if (Input.GetKeyDown(_cancel))
			{
				Main.Instance.WeaponController.Off();
				Main.Instance.FlashLightController.Off();
			}

			if (Input.GetKeyDown(_reloadClip))
			{
				Main.Instance.WeaponController.ReloadClip();
			}
		}


		/// <summary>
		/// Выбор оружия
		/// </summary>
		/// <param name="i">Номер оружия</param>
		private void SelectWeapon(int i)
		{
			Main.Instance.WeaponController.Off();
			var tempWeapon = Main.Instance.Inventory.Weapons[i]; // инкапсулировать
			if (tempWeapon != null)
			{
				Main.Instance.WeaponController.On(tempWeapon);
			}
		}
	}
}