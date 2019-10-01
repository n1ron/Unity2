using System;
using UnityEngine;
using UnityEngine.AI;

namespace Geekbrains
{
	public sealed class Bot : BaseObjectScene
	{
		public float Hp = 100;
		public Vision Vision;
		public Weapon Weapon; //todo с разным оружием 
		public Transform Target { get; set; }
		public NavMeshAgent Agent { get; private set; }
		private float _waitTime = 3;
        private float _alertTimer = 1.5f; //время перехода из режима alerted в detected
        private float _curAlertTime = 0;
        private float _attantionTimer = 7f; //время, которое противник продолжает искать, потеряв врага из вида
        private float _curAttentionTime = 0;
        private StateBot _stateBot;
		private Vector3 _point;

        public event Action<Bot> OnDieChange;

        private StateBot StateBot
		{
			get => _stateBot;
			set
			{
				_stateBot = value;
				switch (value)
				{
					case StateBot.Non:
						Color = Color.white;
						break;
					case StateBot.Patrol:
                        Color = Color.green;
                        break;
					case StateBot.Inspection:
                        Color = Color.yellow;
                        break;
					case StateBot.Detected:
                        Color = Color.red;
                        break;
                    case StateBot.Alerted:
                        Color = Color.cyan;
                        break;
                    case StateBot.Died:
                        Color = Color.gray;
                        break;
					default:
                        Color = Color.white;
                        break;
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();
			Agent = GetComponent<NavMeshAgent>();
            _curAlertTime = _alertTimer;
            _curAttentionTime = _attantionTimer;
        }

        private void OnEnable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange += SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange += SetDamage;
        }

        private void OnDisable()
        {
            var bodyBot = GetComponentInChildren<BodyBot>();
            if (bodyBot != null) bodyBot.OnApplyDamageChange -= SetDamage;

            var headBot = GetComponentInChildren<HeadBot>();
            if (headBot != null) headBot.OnApplyDamageChange -= SetDamage;
        }

        public void Tick()
		{
			if (StateBot == StateBot.Died) return;

            if (StateBot == StateBot.Detected)
            {
                MovePoint(Target.position); 
                Agent.stoppingDistance = 2;
                if (Vision.VisionM(transform, Target))
                {
                    Weapon.Fire();
                }
                if (!Vision.VisionM(transform, Target))
                {
                    _curAttentionTime -= Time.deltaTime;
                }
                if (_curAttentionTime <= 0)
                {
                    StateBot = StateBot.Non;
                    Agent.ResetPath();
                    _curAlertTime = _alertTimer;
                }                   
                return;
            }

            if (StateBot != StateBot.Alerted)
			{
				if (!Agent.hasPath)
				{
					if (StateBot != StateBot.Inspection)
					{
						if (StateBot != StateBot.Patrol)
						{
							StateBot = StateBot.Patrol;
							_point = Patrol.GenericPoint(transform);
                            MovePoint(_point);
							Agent.stoppingDistance = 0;
						}
						else
						{
							if (Vector3.Distance(_point, transform.position) <= 1)
							{
								StateBot = StateBot.Inspection;
								Invoke(nameof(ReadyPatrol), _waitTime);
							}
						}
					}
				}

				if (Vision.VisionM(transform, Target))
				{
					StateBot = StateBot.Alerted;
				}
			}
			else
			{
                if (_curAlertTime <= 0)
                {
                    _curAttentionTime = _attantionTimer;
                    StateBot = StateBot.Detected;
                }
                else
                {
                    _curAlertTime -= Time.deltaTime;
                }
            }
        }

		public void SetDamage(InfoCollision info)
		{
            StateBot = StateBot.Alerted; //todo реакциия на попадание - готово

            if (Hp > 0)
			{
				Hp -= info.Damage;
			}

			if (Hp <= 0)
			{
				StateBot = StateBot.Died;
				Agent.enabled = false;
				foreach (var child in GetComponentsInChildren<Transform>())
				{
					child.parent = null;
					var tempRbChild = child.GetComponent<Rigidbody>();
					if (!tempRbChild)
					{
						tempRbChild = child.gameObject.AddComponent<Rigidbody>();
					}
					//tempRbChild.AddForce(info.Dir * Random.Range(10, 300));
					
					Destroy(child.gameObject, 10);
				}

                OnDieChange?.Invoke(this);
            }
		}

		private void ReadyPatrol()
		{
			StateBot = StateBot.Non;
		}

		public void MovePoint(Vector3 point)
		{
			Agent.SetDestination(point);
		}
	}
}