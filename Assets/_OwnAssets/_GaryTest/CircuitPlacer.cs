using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitPlacer : MonoBehaviour {
	public CircuitPart selectedCircuitPart;
	public List<CircuitPart> ListOfTurrets;
	private GameObject targetToMove;
	private InputManager inputManager;

	void Start () {
		targetToMove = selectedCircuitPart.transform.Find ("Target").gameObject;
		inputManager = FindObjectOfType<InputManager> ();
	}

	void Update () {
		if (inputManager.MouseScroll != 0f)
			ChangeSelectedPart ();

		targetToMove.transform.position = new Vector3 (inputManager.mousePosition.x, 0, inputManager.mousePosition.z);

		if (inputManager.LeftClick) {
			PlacePart ();
		}
	}

	void PlacePart () {
		Vector3 snapArea = selectedCircuitPart.snapArea;
		CircuitPart placedCircuitPart = Instantiate (selectedCircuitPart.prefab, Vector3.zero, Quaternion.identity).GetComponent<CircuitPart> ();
		placedCircuitPart.visual.transform.position = snapArea;
		placedCircuitPart.snapArea = snapArea;
		placedCircuitPart.AddSelfToGridSystem ();
		placedCircuitPart.transform.Find ("Target").gameObject.SetActive (false);
	}

	void ChangeSelectedPart () {
		int index = ListOfTurrets.IndexOf (selectedCircuitPart);
		if (inputManager.MouseScroll < 0f) //move left
			index--;
		if (inputManager.MouseScroll > 0f) //move right
			index++;

		if (index >= ListOfTurrets.Count) {
			index = 0;
		}

		if (index < 0)
		{
			index = ListOfTurrets.Count - 1 ;
		}

		CircuitPart nextSelected = ListOfTurrets[index];
		nextSelected.visual.transform.position = selectedCircuitPart.visual.transform.position;
		selectedCircuitPart.gameObject.SetActive (false);

		selectedCircuitPart = nextSelected;
		selectedCircuitPart.gameObject.SetActive (true);
		targetToMove = selectedCircuitPart.transform.Find ("Target").gameObject;
	}
}