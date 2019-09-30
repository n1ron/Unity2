using UnityEngine;
using System.Collections.Generic;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class MovingPointController : MonoBehaviour
    {
        private MovingPoints _movingPoints;
        private List<AICharacterControl> _listOfColliders;
        
        void Start()
        {
            _movingPoints = Camera.main.GetComponent<MovingPoints>();            
        }

        private void OnTriggerEnter(Collider other)
        {
            var temp = other.gameObject.GetComponent<AICharacterControl>();
            temp.DequeueTarget();
            _listOfColliders.Remove(temp);
            if (_listOfColliders.Count == 0) Destroy(gameObject);
        }

        public void GetList(List<AICharacterControl> list)
        {
            _listOfColliders = new List<AICharacterControl>(list);
        }
    }
}
