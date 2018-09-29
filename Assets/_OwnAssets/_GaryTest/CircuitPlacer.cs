using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitPlacer : MonoBehaviour {
	public CircuitPart selectedCircuitPart;
	public List<Turret> ListOfTurrets;
    public List<Wire> ListOfWires;
    //list of power
	private GameObject targetToMove;
	private InputManager inputManager;
    [HideInInspector] public enum ItemType { TURRETS, WIRES, POWERSOURCES};
    private ItemType selectedItemType = ItemType.TURRETS;

	void Start () {
		targetToMove = selectedCircuitPart.transform.Find ("Target").gameObject;
		inputManager = FindObjectOfType<InputManager> ();
	}

	void Update () {
		if (inputManager.MouseScroll != 0f)
			ChangeSelectedPart (selectedItemType);

		targetToMove.transform.position = new Vector3 (inputManager.mousePosition.x, 0, inputManager.mousePosition.z);

		if (inputManager.LeftClick) {
			PlacePart ();
		}
	}

    public void SetSelectedItemType(ItemType changeTo)
    {
        selectedItemType = changeTo;
    }

	void PlacePart () {
		Vector3 snapArea = selectedCircuitPart.snapArea;
		Debug.Log (selectedCircuitPart.m_gridSystem.CheckFreeSpace (snapArea));
		if (selectedCircuitPart.m_gridSystem.CheckFreeSpace (snapArea)) {
			CircuitPart placedCircuitPart = Instantiate (selectedCircuitPart.prefab, Vector3.zero, Quaternion.identity).GetComponent<CircuitPart> ();
			placedCircuitPart.visual.transform.position = snapArea;
			placedCircuitPart.snapArea = snapArea;
			placedCircuitPart.isPlaced = true;
			placedCircuitPart.target.gameObject.SetActive (false);
			placedCircuitPart.AddSelfToGridSystem ();
		}
	}

	void ChangeSelectedPart (ItemType itemType) {

        int index = 0;
        int listSize = 0;

        switch (itemType)
        {
            case ItemType.TURRETS:
                index = ListOfTurrets.IndexOf((Turret)selectedCircuitPart);
                listSize = ListOfTurrets.Count;
                break;
            case ItemType.WIRES:
                index = ListOfWires.IndexOf((Wire)selectedCircuitPart);
                listSize = ListOfWires.Count;
                break;
            case ItemType.POWERSOURCES:
                break;
            default:
                break;
        }
        

		if (inputManager.MouseScroll < 0f) //move left
			index--;
		if (inputManager.MouseScroll > 0f) //move right
			index++;

		if (index >= listSize) {
			index = 0;
		}

		if (index < 0) {
			index = listSize - 1;
		}

        CircuitPart nextSelected = null;

        switch (itemType)
        {
            case ItemType.TURRETS:
                nextSelected = ListOfTurrets[index];
                break;
            case ItemType.WIRES:
                nextSelected = ListOfWires[index];
                break;
            case ItemType.POWERSOURCES:
                break;
            default:
                break;
        }


		nextSelected.visual.transform.position = selectedCircuitPart.visual.transform.position;
		selectedCircuitPart.gameObject.SetActive (false);

		selectedCircuitPart = nextSelected;
		selectedCircuitPart.gameObject.SetActive (true);
		targetToMove = selectedCircuitPart.transform.Find ("Target").gameObject;
	}
}