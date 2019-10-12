using System.Collections.Generic;
using UnityEngine;

namespace Geekbrains
{
	public class PathBot : MonoBehaviour
	{
		[SerializeField]
		private Color _lineColor = Color.red;
		private List<Transform> _nodes = new List<Transform>();
		// OnDrawGizmosSelected()
		private void OnDrawGizmos()
		{
			Gizmos.color = _lineColor;
			var pathTransforms = GetComponentsInChildren<Transform>();
			_nodes = new List<Transform>();
			foreach (var t in pathTransforms)
			{
				if (t != transform)
				{
					_nodes.Add(t);
				}
			}
			for (var i = 0; i < _nodes.Count; i++)
			{
				var currentNode = _nodes[i].position;
				var previousNode = Vector3.zero;
				if (i > 0)
				{
					previousNode = _nodes[i - 1].position;
				}
				else if (i == 0 && _nodes.Count > 1)
				{
					previousNode = _nodes[_nodes.Count - 1].position;
				}
				Gizmos.DrawLine(previousNode, currentNode);
				Gizmos.DrawWireSphere(currentNode, 0.3f);
			}
		}
	}
}