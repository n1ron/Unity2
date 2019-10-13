using System.IO;
using UnityEngine;

namespace Geekbrains
{
	public sealed class SaveDataRepository
	{
		private readonly IData<SerializableGameObject> _data;

		private const string _folderName = "dataSave";
		private const string _fileName = "data.bat";
		private string _path;

		public SaveDataRepository()
		{
			if (Application.platform == RuntimePlatform.WebGLPlayer)
			{
				_data = new PlayerPrefsData();
			}
			else
			{
				_data = new JsonData<SerializableGameObject>();
			}
			_path = Path.Combine(Application.dataPath, _folderName);
			
		}

		public void Save()
		{
			if (!Directory.Exists(Path.Combine(_path)))
			{
				Directory.CreateDirectory(_path);
			}
			var player = new SerializableGameObject
			{
				Pos = Main.Instance.Player.position,
				Name = "Roman",
				IsEnable = true
			};

			_data.Save(player, Path.Combine(_path, _fileName));
		}

		public void Load()
		{
			var file = Path.Combine(_path, _fileName);
			if (!File.Exists(file)) return;
			var newPlayer = _data.Load(file);
			Main.Instance.Player.position = newPlayer.Pos;
			Main.Instance.Player.name = newPlayer.Name;
			Main.Instance.Player.gameObject.SetActive(newPlayer.IsEnable);

			Debug.Log(newPlayer);
		}
	}
}