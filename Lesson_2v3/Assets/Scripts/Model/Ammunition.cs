using UnityEngine;

namespace Geekbrains
{
	public abstract class Ammunition : BaseObjectScene
	{
		[SerializeField] protected float _timeToReset = 5;
		[SerializeField] protected float _baseDamage = 10;
        protected float _curDamage;
		protected float _lossOfDamageAtTime = 0.2f;

		public AmmunitionType Type = AmmunitionType.Bullet;

        public void LoadAmmunition()
        {
            _curDamage = _baseDamage;
            InvokeRepeating(nameof(LossOfDamage), 0, 1);
            Main.Instance.ObjectPooler.ResetObjectInTime(this, _timeToReset);
        }

		public void AddForce(Vector3 dir)
		{
			if (!Rigidbody) return;
			Rigidbody.AddForce(dir);
		}

        protected void LossOfDamage()
        {
            _curDamage -= _lossOfDamageAtTime;
        }

        public void StopLosingDamage()
        {
            CancelInvoke(nameof(LossOfDamage));
        }
    }
}