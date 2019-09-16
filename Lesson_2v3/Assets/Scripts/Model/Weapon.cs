using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
    public abstract class Weapon : BaseObjectScene
    {
		[SerializeField] private int _bulletsInClip = 30;
        [SerializeField] private int _countClip = 5;
		public Ammunition Ammunition;
		public Clip Clip;

		protected AmmunitionType[] _ammunitionType = {AmmunitionType.Bullet};

		[SerializeField] protected Transform _barrel;
		[SerializeField] protected float _force = 999;
		[SerializeField] protected float _rechergeTime = 0.2f;
        [SerializeField] protected float _deviation = 0.01f;
        private Queue<Clip> _clips = new Queue<Clip>();

		protected bool _isReady = true;
		//protected Timer _timer = new Timer();

		protected virtual void Start()
		{
			for (var i = 0; i <= _countClip; i++)
			{
				AddClip(new Clip { CountAmmunition = _bulletsInClip });
			}

			ReloadClip();
		}

		public abstract void Fire();

		//protected virtual void Update()
		//{
		//	_timer.Update();
		//	if (_timer.IsEvent())
		//	{
		//		ReadyShoot();
		//	}
		//}

		protected void ReadyShoot()
		{
			_isReady = true;
		}

		protected void AddClip(Clip clip)
		{
			_clips.Enqueue(clip);
		}

		public void ReloadClip()
		{
			if (CountClip <= 0) return;
			Clip = _clips.Dequeue();
		}

		public int CountClip => _clips.Count;

        /// <summary>
        /// Добавление отклонения пули
        /// </summary>
        /// <param name="direction">изначальное направление</param>
        /// <param name="deviation">степерь отклонения</param>
        /// <returns></returns>
        public Vector3 AddDeviation(Vector3 direction, float deviation)
        {
            Vector3 newDeviation = new Vector3(direction.x + Random.Range(-deviation, deviation), direction.y + Random.Range(-deviation, deviation), direction.z);
            return newDeviation;
        }
    }
}