using UnityEngine;

namespace Geekbrains
{
	public sealed class WeaponController : BaseController, IOnUpdate
	{
		[HideInInspector] public Weapon Weapon;
		private int _mouseButton = (int)MouseButton.LeftButton;

		public void OnUpdate()
		{
			if (!IsActive) return;
			if (Input.GetMouseButton(_mouseButton))
			{
				Weapon.Fire();
				UiInterface.WeaponUiText.ShowData(Weapon.Clip.CountAmmunition, Weapon.CountClip);
			}
		}

		public override void On(BaseObjectScene weapon)
		{
			if (IsActive) return;
			base.On(weapon);

			Weapon = weapon as Weapon;
			if (Weapon == null) return;
			Weapon.IsVisible = true;
			UiInterface.WeaponUiText.SetActive(true);
			UiInterface.WeaponUiText.ShowData(Weapon.Clip.CountAmmunition, Weapon.CountClip);
		}

		public override void Off()
		{
			if (!IsActive) return;
			base.Off();
			Weapon.IsVisible = false;
			Weapon = null;
			UiInterface.WeaponUiText.SetActive(false);
		}

		public void ReloadClip()
		{
			if (Weapon == null) return;
			Weapon.ReloadClip();
			UiInterface.WeaponUiText.ShowData(Weapon.Clip.CountAmmunition, Weapon.CountClip);
		}
	}
}