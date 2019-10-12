using System;
using UnityEngine;

namespace Geekbrains
{
	[RequireComponent(typeof(Renderer)), ExecuteInEditMode]
	public class TestAttribute : MonoBehaviour
	{
		[HideInInspector] public float TestPublic;

		public SerializableGameObject SerializableGameObject;
		private const int _min = 0;
		private const int _max = 100;
		[Header("Test variables")]
		[ContextMenuItem("Randomize Number", nameof(Randomize))]
		[SerializeField] private float _testPrivate = 7;

		[Range(_min, _max)]
		public int SecondTest;

		private bool _testBool;

		[Space(60)]
		[SerializeField, Multiline(5)] private string _testMultiline;
		[Space(60)]
		[SerializeField, TextArea(5, 5), Tooltip("Tooltip text")] private string _testTextArea;


        private void Update()
        {
            GetComponent<Renderer>().sharedMaterial.color = UnityEngine.Random.ColorHSV();
        }

        private void Randomize()
		{
			_testPrivate = UnityEngine.Random.Range(_min, _max);
			TestObsolete();
			
		}

		[Obsolete("Устарело. Используй что-то другое")]
		private void TestObsolete()
		{
		}
	}
}