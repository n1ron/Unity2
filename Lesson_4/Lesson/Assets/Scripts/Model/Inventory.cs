using UnityEngine;

namespace Geekbrains
{
	public class Inventory : IInitialization
	{
		private Weapon[] _weapons = new Weapon[5];

		public Weapon[] Weapons => _weapons;
		private int _selectIndexWeapon = 0;

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

		/// <summary>
		/// Выбор оружия цифрами 
		/// </summary>
		/// <param name="weaponNumber">Индекс оружия</param>
		public Weapon SelectWeapon(int weaponNumber)
		{
			if (weaponNumber < 0 || weaponNumber >= Weapons.Length) return null;

			var tempWeapon = Weapons[weaponNumber];
			return tempWeapon;
		}

		/// <summary>
		/// Прокрутки оружия колесом мыши
		/// </summary>
		/// <param name="scrollWheel">Инкремент или декремент индекса</param>
		public Weapon SelectWeapon(MouseScrollWheel scrollWheel)
		{
			if (scrollWheel == MouseScrollWheel.Up)
			{
				if (_selectIndexWeapon < Weapons.Length - 1)
				{
					_selectIndexWeapon++;
				}
				else
				{
					_selectIndexWeapon = -1;
				}
				return SelectWeapon(_selectIndexWeapon);
			}

			if (_selectIndexWeapon <= 0)
			{
				_selectIndexWeapon = Weapons.Length;
			}
			else
			{
				_selectIndexWeapon--;
			}
			return SelectWeapon(_selectIndexWeapon);
		}

		public void AddWeapon(Weapon weapon)
		{
			
		}

		// Добавить функционал
	}
}