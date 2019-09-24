using System;
using UnityEngine;
using System.Collections.Generic;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        private MovingPoints _movingPoints;
        private Queue<Transform> _points;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;

            _movingPoints = Camera.main.GetComponent<MovingPoints>();
            _movingPoints.AddToList(this);

            _points = new Queue<Transform>();
        }


        private void Update()
        {
            if (_points.Count > 0)
            {
                SetTarget(_points.Peek());
            }              

            if (target != null)
                agent.SetDestination(target.position);


	        character.Move(agent.remainingDistance > agent.stoppingDistance ? agent.desiredVelocity : Vector3.zero,
		        false, false);
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }

        public void EnqueueTarget(Transform target)
        {
            _points.Enqueue(target);
        }

        public void DequeueTarget()
        {
            if (_points.Count > 0) _points.Dequeue();
        }
    }
}
