using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {
	private Grid grid;

	private void Awake() 
	{
		grid = FindObjectOfType<Grid>() ;
	}

	public Vector3 GetNearestPosition(Vector3 nearPoint) {
		var FinalPosition = grid.GetNearestPointOnGrid (nearPoint);
		FinalPosition.y = 0;
		return FinalPosition;
	}
}