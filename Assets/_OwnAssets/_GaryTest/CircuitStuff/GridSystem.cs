using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {
	private Grid grid;

	private void Awake () {
		grid = FindObjectOfType<Grid> ();
	}

	public Vector3 GetNearestPosition (Vector3 nearPoint) {
		var FinalPosition = grid.GetNearestPointOnGrid (nearPoint);
		FinalPosition.y = 0;
		return FinalPosition;
	}

	public CircuitPart[] GetNeighbouringParts (Vector3 position) {
		position = grid.GetNearestPointOnGrid (position);

		GameObject partUp, partDown, partLeft, partRight;

		return new CircuitPart[] {
			grid.GridParts.TryGetValue (position + Vector3.forward * grid.Size, out partUp) ? partUp.GetComponent<CircuitPart> () : null,
				grid.GridParts.TryGetValue (position + Vector3.right * grid.Size, out partRight) ? partRight.GetComponent<CircuitPart> () : null,
				grid.GridParts.TryGetValue (position - Vector3.forward * grid.Size, out partDown) ? partDown.GetComponent<CircuitPart> () : null,
				grid.GridParts.TryGetValue (position - Vector3.right * grid.Size, out partLeft) ? partLeft.GetComponent<CircuitPart> () : null
		};
	}
	public void AddToGridSystem (Vector3 position, GameObject part) {
		grid.GridParts.Add (position, part);
	}

	public bool CheckFreeSpace (Vector3 position) {
		//return true if free
		//return false if not free
		return !grid.GridParts.ContainsKey (position);
	}

	public void RemoveFromGridSystem (GameObject destroyedPart) {
		grid.GridParts.Remove (destroyedPart.GetComponent<CircuitPart> ().snapArea);
	}
}