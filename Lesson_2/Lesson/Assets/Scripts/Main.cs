using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	public class Main : MonoBehaviour
	{
		public FlashLightController FlashLightController { get; private set; }
		public InputController InputController { get; private set; }
		public PlayerController PlayerController { get; private set; }
		public WeaponController WeaponController { get; private set; }
		public SelectionController SelectionController { get; private set; }
		public Inventory Inventory { get; private set; }
		public Transform Player { get; private set; }
		public Transform MainCamera { get; private set; }
		private IOnUpdate[] _controllers;

		public static Main Instance { get; private set; }

		private void Awake()
		{
			Instance = this;

			MainCamera = Camera.main.transform;
			Player = GameObject.FindGameObjectWithTag("Player").transform;

			Inventory = new Inventory();
			
			PlayerController = new PlayerController(new UnitMotor(
				GameObject.FindObjectOfType<CharacterController>().transform));
			FlashLightController = new FlashLightController();
			InputController = new InputController();
			WeaponController = new WeaponController();
			SelectionController = new SelectionController();
			_controllers = new IOnUpdate[5];
			_controllers[0] = FlashLightController;
			_controllers[1] = InputController;
			_controllers[2] = PlayerController;
			_controllers[3] = WeaponController;
			_controllers[4] = SelectionController;
		}

		public void OnStartCoroutine(IEnumerator routine)
		{
			StartCoroutine(routine);
		}

		private void Start()
		{
			Inventory.OnStart();
			FlashLightController.OnStart();
			PlayerController.On();
			InputController.On();
		}

		private void Update()
		{
			for (var index = 0; index < _controllers.Length; index++)
			{
				var controller = _controllers[index];
				controller.OnUpdate();
			}
		}
		private void OnGUI()
		{
			GUI.Label(new Rect(0, 0, 100, 100), $"{1 / Time.deltaTime:0.0}");
		}
	}
}