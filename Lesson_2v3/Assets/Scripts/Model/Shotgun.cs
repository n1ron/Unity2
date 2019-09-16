using UnityEngine;

namespace Geekbrains
{
    public sealed class Shotgun : Weapon
    {
        [SerializeField] private int _pellets; //число дробин в патроне

        public override void Fire()
		{
			if (!_isReady) return;
			if (Clip.CountAmmunition <= 0) return;
			if (!Ammunition) return;
            for (int i = 0; i < _pellets; i++)
            {
                var temAmmunition = Main.Instance.ObjectPooler.TakeFromPool(Ammunition.Type, _barrel.position, _barrel.rotation);
                temAmmunition.AddForce(AddDeviation(_barrel.forward, _deviation) * _force);
            }
            Clip.CountAmmunition--;
			_isReady = false;
			Invoke(nameof(ReadyShoot), _rechergeTime);
		}
	}
}