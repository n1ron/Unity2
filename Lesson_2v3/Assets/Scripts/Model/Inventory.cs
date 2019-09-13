using UnityEngine;

namespace Geekbrains
{
	public sealed class Inventory : IInitialization
	{
		private Weapon[] _weapons = new Weapon[5];

		public Weapon[] Weapons => _weapons;

		public FlashLightModel FlashLight { get; private set; }

		public void OnStart()
		{
			_weapons = Main.Instance.Player.GetComponentsInChildren<Weapon>();

			foreach (var weapon in Weapons)
			{
				weapon.IsVisible = false;
			}

			FlashLight = Object.FindObjectOfType<FlashLightModel>();
		}

        //todo Добавить функционал

        public void SelectWeapon(int i)
        {
            Main.Instance.WeaponController.Off();
            var tempWeapon = Weapons[i];
            if (tempWeapon != null)
            {
                Main.Instance.WeaponController.On(tempWeapon);
            }
        }

        public void SelectNextWeapon()
        {
            int tempI = System.Array.IndexOf(Weapons, Main.Instance.WeaponController.Weapon);
            if (tempI == Weapons.Length - 1) SelectWeapon(0);
            else SelectWeapon(tempI + 1);
        }
        
        public void SelectPreviousWeapon()
        {
            int tempI = System.Array.IndexOf(Weapons, Main.Instance.WeaponController.Weapon);
            if (tempI == 0) SelectWeapon(Weapons.Length - 1);
            else SelectWeapon(tempI - 1);
        }


        public void RemoveWeapon(Weapon weapon)
        {
            
        }

        private void SwitchWeapon()
        {

        }
    }
}