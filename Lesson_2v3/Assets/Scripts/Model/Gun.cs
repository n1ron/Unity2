using UnityEngine;

namespace Geekbrains
{
	public class Gun : Weapon
	{
		public sealed override void Fire()
		{
			if (!_isReady) return;
			if (Clip.CountAmmunition <= 0) return;
			if (!Ammunition) return;
			var temAmmunition = Main.Instance.ObjectPooler.TakeFromPool(Ammunition.Type, _barrel.position, _barrel.rotation);
			temAmmunition.AddForce(AddDeviation(_barrel.forward, _deviation) * _force);
			Clip.CountAmmunition--;
			_isReady = false;
			Invoke(nameof(ReadyShoot), _rechergeTime);
			//_timer.Start(_rechergeTime);
		}
	}
}