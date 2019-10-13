using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace Geekbrains.Editor.Test
{
    public class MyWindow : EditorWindow
    {
        public static GameObject ObjectInstantiate;
        public bool GroupEnabled;
        public bool RandomColor = true;
        public int CountObject = 1;
        public float Radius = 10;
        public bool RandomSize = true;
        private string[] _firstName = new string[5] { "Agressive ", "Positive ", "Cute ", "Loud ", "Bright " };
        private string[] _secondName = new string[5] {"Silver ", "Green ", "Yellow ", "Red ", "Blue "};
        private string[] _thirdName = new string[5] {"Bettle", "Submarine", "Tiger", "Jedy", "Lady"};
        private List<GameObject> _createdObjects = new List<GameObject>();
        private Vector3 _scale = new Vector3();

		private void OnGUI()
		{
			GUILayout.Label("Базовые настройки", EditorStyles.boldLabel);
			ObjectInstantiate =
				EditorGUILayout.ObjectField("Объект который хотим вставить",
						ObjectInstantiate, typeof(GameObject), true)
					as GameObject;
			GroupEnabled = EditorGUILayout.BeginToggleGroup("Дополнительные настройки",
				GroupEnabled);
			RandomColor = EditorGUILayout.Toggle("Случайный цвет", RandomColor);
			CountObject = EditorGUILayout.IntSlider("Количество объектов",
				CountObject, 1, 100);
            RandomSize = EditorGUILayout.Toggle("Случайный размер", RandomSize);
            Radius = EditorGUILayout.Slider("Радиус окружности", Radius, 10, 50);
			EditorGUILayout.EndToggleGroup();
			if (GUILayout.Button("Создать объекты"))
			{
				if (ObjectInstantiate)
				{
					GameObject root = new GameObject("Root");
                    _createdObjects.Add(root);
					for (int i = 0; i < CountObject; i++)
					{
						float angle = i * Mathf.PI * 2 / CountObject;
						Vector3 pos = new Vector3(Mathf.Cos(angle), 0,
							              Mathf.Sin(angle)) * Radius;
						GameObject temp = Instantiate(ObjectInstantiate, pos,
							Quaternion.identity);
						temp.name = NameGen();
						temp.transform.parent = root.transform;
                        if (RandomSize) _scale.x = _scale.y = _scale.z = Random.Range(0.2f, 4f);
                        temp.transform.localScale = _scale;
						var tempRenderer = temp.GetComponent<Renderer>();
						if (tempRenderer && RandomColor)
						{
							tempRenderer.material.color = Random.ColorHSV();
						}
                        _createdObjects.Add(temp);
					}
				}
			}
            if (GUILayout.Button("Удалить все созданные объекты"))
            {
                if (_createdObjects.Count > 0)
                {
                    foreach (GameObject thisObject in _createdObjects)
                    {
                        DestroyImmediate(thisObject);
                    }
                }
            }
        }

        private string NameGen()
        {
            return _firstName[Random.Range(0,5)] + " " + _secondName[Random.Range(0, 5)] + " " + _thirdName[Random.Range(0, 5)];
        } 
	}
}