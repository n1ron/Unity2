using UnityEngine;

namespace Geekbrains
{
	public abstract class Ammunition : BaseObjectScene
	{
		[SerializeField] protected float _timeToDestruct = 10;
		[SerializeField] protected float _baseDamage = 10;
		protected float _curDamage;
		protected float _lossOfDamageAtTime = 0.2f;

		public AmmunitionType Type = AmmunitionType.Bullet;

		protected override void Awake()
		{
			base.Awake();
			_curDamage = _baseDamage;
		}

		private void Start()
		{
            // Вернуть в пул
            DestroyAmmunition(_timeToDestruct);
            InvokeRepeating(nameof(LossOfDamage), 0, 1);
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

        protected void DestroyAmmunition(float timeToDestruct = 0)
        {
            Destroy(gameObject, timeToDestruct);
            CancelInvoke(nameof(LossOfDamage));
        }
	}
}