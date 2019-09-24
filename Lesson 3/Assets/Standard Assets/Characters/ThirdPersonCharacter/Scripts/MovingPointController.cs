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
            _listOfColliders = new List<AICharacterControl>();
            
        }

        private void OnCollisionEnter(Collision collision)
        {
            var temp = collision.gameObject.GetComponent<AICharacterControl>();
            temp.DequeueTarget();
            _listOfColliders.Remove(temp);
            if (_listOfColliders.Count == 0) Destroy(gameObject);
        }

        public void GetList(List<AICharacterControl> list)
        {
            foreach (var ai in list)
            {
                if (ai != null) _listOfColliders.Add(ai);
            }
        }
    }
}
