using UnityEngine;

namespace Geekbrains
{
	public class InputController : BaseController, IOnUpdate
	{
		private KeyCode _savePlayer = KeyCode.C;
		private KeyCode _loadPlayer = KeyCode.V;
		private KeyCode _screenshot = KeyCode.Q;

		public InputController()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		public void OnUpdate()
		{
			if (!IsActive) return;

			if (Input.GetKeyDown(_savePlayer))
			{
				Main.Instance.SaveDataRepository.Save();
			}

			if (Input.GetKeyDown(_loadPlayer))
			{
				Main.Instance.SaveDataRepository.Load();
			}

			if (Input.GetKeyDown(_screenshot))
			{
				Main.Instance.PhotoController.FirstMethod();
			}
		}
	}
}