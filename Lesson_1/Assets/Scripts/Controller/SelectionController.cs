using UnityEngine;

namespace Geekbrains
{
    public sealed class SelectionController : BaseController, IOnUpdate, IInitialization
    {
        private SelectionUi _selectionUi;
        private Ray _ray;
        private Vector2 _center;
        private float _selectionDistance = 100;
        private RaycastHit _hit;
        private Camera _camera;

        public void Init()
        {
            _selectionUi = Object.FindObjectOfType<SelectionUi>();
            _center = new Vector2(Screen.width/2, Screen.height/2);
            _camera = Camera.main;
            //_ray = _camera.ScreenPointToRay(_center);
        }

        public void OnUpdate()
        {
            //if (Physics.Raycast(_ray, out _hit, _selectionDistance))
            if (Physics.Raycast(_camera.ScreenPointToRay(_center), out _hit, _selectionDistance))
            {
                _selectionUi.SetActive(true);
                _selectionUi.Text = _hit.transform.ToString();
            }
            else _selectionUi.SetActive(false);
        }      
    }
}


