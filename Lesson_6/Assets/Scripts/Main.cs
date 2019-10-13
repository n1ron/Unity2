using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	public class Main : MonoBehaviour
	{
		public InputController InputController { get; private set; }
		public PlayerController PlayerController { get; private set; }
		public PhotoController PhotoController { get; private set; }
		public SaveDataRepository SaveDataRepository { get; private set; }
		public Transform Player { get; private set; }
		public Camera MainCamera { get; private set; }
		private IOnUpdate[] _controllers;

		public static Main Instance { get; private set; }

		private void Awake()
		{
			Instance = this;

			MainCamera = Camera.main;
			Player = GameObject.FindGameObjectWithTag("Player").transform;

			SaveDataRepository = new SaveDataRepository();
			PhotoController = new PhotoController();
			
			PlayerController = new PlayerController(new UnitMotor(
				GameObject.FindObjectOfType<CharacterController>().transform));
			InputController = new InputController();

			_controllers = new IOnUpdate[2];
			_controllers[0] = InputController;
			_controllers[1] = PlayerController;
		}

		private void Start()
		{
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