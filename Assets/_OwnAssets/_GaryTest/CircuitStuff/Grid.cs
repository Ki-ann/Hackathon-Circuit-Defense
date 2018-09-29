﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour {
	[HideInInspector] public Dictionary<Vector3, GameObject> GridParts = new Dictionary<Vector3, GameObject> ();
	[SerializeField] private float size = 1f;
	public float Size { get { return size; } }
	public Vector3 GetNearestPointOnGrid (Vector3 position) {
		position -= transform.position;

		int xCount = Mathf.RoundToInt (position.x / size);
		int yCount = Mathf.RoundToInt (position.y / size);
		int zCount = Mathf.RoundToInt (position.z / size);

		Vector3 result = new Vector3 (
			(float) xCount * size,
			(float) yCount * size,
			(float) zCount * size
		);

		result += transform.position;
		return result;
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.yellow;

		for (float x = 0; x < 10; x += size) {
			for (float z = 0; z < 10; z += size) {
				Vector3 point = GetNearestPointOnGrid (new Vector3 (x, 0f, z));
				Gizmos.DrawSphere (point, 0.1f);
			}
		}
	}

}