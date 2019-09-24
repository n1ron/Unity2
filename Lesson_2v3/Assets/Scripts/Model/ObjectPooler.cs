using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Geekbrains
{
    public class ObjectPooler : MonoBehaviour, IInitialization
    {
        [SerializeField] private List<Pool> _pools;
        private Dictionary<AmmunitionType, Queue<Ammunition>> _poolDictionary;
        private Transform _transform;

        public void OnStart()
        {
            _transform = this.gameObject.transform;
            _poolDictionary = new Dictionary<AmmunitionType, Queue<Ammunition>>();

            foreach (Pool pool in _pools)
            {
                Queue<Ammunition> objectsPool = new Queue<Ammunition>();
                for (int i = 0; i < pool.Size; i++)
                {
                    Ammunition obj = Instantiate(pool.Ammunition);
                    obj.SetActive(false);
                    objectsPool.Enqueue(obj);
                }

                _poolDictionary.Add(pool.AmmunitionType, objectsPool);
            }
        }

        public Ammunition TakeFromPool (AmmunitionType ammunitionType, Vector3 position, Quaternion rotation)
        {
            if (!_poolDictionary.ContainsKey(ammunitionType)) return null;
            Ammunition obj = _poolDictionary[ammunitionType].Dequeue();
            if (obj.isActiveAndEnabled) ResetObjectFromPool(obj);

            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.LoadAmmunition();

            _poolDictionary[ammunitionType].Enqueue(obj); //здесь объект уже возвращается в пул, его остается только деактивировать 

            return obj;
        }

        public void ResetObjectFromPool(Ammunition obj)
        {
            if (!obj.isActiveAndEnabled) return;

            obj.SetActive(false);
            obj.transform.position = _transform.position;
            obj.transform.rotation = _transform.rotation;
            obj.StopLosingDamage();
        }
    }
}


