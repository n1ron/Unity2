using UnityEngine;

namespace Geekbrains
{
    public sealed class SelectionController : BaseController, IOnUpdate//, IInitialization
    {
        private Ray _ray;
        private Vector2 _center;
        private float _selectionDistance = 10;
        private RaycastHit _hit;
        private Camera _camera;

        public SelectionController()
        {
            _center = new Vector2(Screen.width/2, Screen.height/2);
            _camera = Camera.main;
            _ray = _camera.ScreenPointToRay(_center);
        }

        public void OnUpdate()
        {
            if(Physics.Raycast(_ray, out _hit, _selectionDistance))
            {
                var selection = _hit.transform;
                var selectionName = selection.GetComponent<PropertyName>();
                Debug.Log(selectionName);
            }
        }

        //надо инициализировать UI
    }
}


