using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitPlacer : MonoBehaviour {
	public CircuitPart selectedCircuitPart;
	public List<Buildings> ListOfTurrets;
	public List<Wire> ListOfWires;
	public List<Battery> ListOfBatteries;
	private GameObject targetToMove;
	private InputManager inputManager;
	[SerializeField] private IntVariable money;
	[HideInInspector] public enum ItemType { TURRETS, WIRES, BATTERIES };
 private ItemType selectedItemType = ItemType.TURRETS;

 void Start () {
 targetToMove = selectedCircuitPart.transform.Find ("Target").gameObject;
 inputManager = FindObjectOfType<InputManager> ();
	}

	void Update () {
        if (inputManager.useKeyboard)
        {
            if (inputManager.MouseScroll != 0)
            {
                ChangeSelectedPart(selectedItemType);

                if (selectedCircuitPart.visual.GetComponent<Collider>() != null)
                {
                    if (selectedCircuitPart.visual.GetComponent<Collider>().enabled)
                    {
                        selectedCircuitPart.visual.GetComponent<Collider>().enabled = false;
                    }
                }
            }
            if (inputManager.ShiftMouseScroll != 0f)
            {
                ChangeSelectedItemType();
            }
            targetToMove.transform.position = new Vector3(inputManager.mousePosition.x, 0, inputManager.mousePosition.z);

            if (inputManager.LeftClick)
            {
                PlacePart();
            }
        }

        else
        {
            //ChangeSelected
            targetToMove.transform.position = new Vector3(inputManager.mousePosition.x, 0, inputManager.mousePosition.z);

            if (inputManager.LeftClick)
            {
                PlacePart();
            }
        }
	}

    public void SelectWithName(string name)
    {
        CircuitPart cp = null;

        switch (name)
        {
            case "Bullet":
                cp = ListOfTurrets[0];
                break;
            case "Explosive":
                cp = ListOfTurrets[1];
                break;
            case "Shield":
                cp = ListOfTurrets[2];
                break;
            case "Bullet2":
                cp = ListOfTurrets[3];
                break;
            case "Explosive2":
                cp = ListOfTurrets[4];
                break;
            case "Shield2":
                cp = ListOfTurrets[5];
                break;
            case "Wire":
                cp = ListOfWires[0];
                break;
            case "Wire2":
                cp = ListOfWires[1];
                break;
            case "Battery":
                cp = ListOfBatteries[0];
                break;
            case "Battery2":
                cp = ListOfBatteries[1];
                break;
            default:
                break;
        }
        cp.visual.transform.position = selectedCircuitPart.visual.transform.position;
        selectedCircuitPart.gameObject.SetActive(false);

        selectedCircuitPart = cp;
        selectedCircuitPart.gameObject.SetActive(true);
        targetToMove = selectedCircuitPart.target.gameObject;
    }

	// Hard coded lmao
	public void SetSelectedItemType (ItemType changeTo) {
		selectedItemType = changeTo;
	}

	void PlacePart () {
		Vector3 snapArea = selectedCircuitPart.snapArea;

		if (selectedCircuitPart.m_gridSystem.CheckFreeSpace (snapArea) && money.Value - selectedCircuitPart.Cost >= 0) {
			money.Value -= selectedCircuitPart.Cost;
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

		switch (itemType) {
			case ItemType.TURRETS:
				index = ListOfTurrets.IndexOf ((Buildings) selectedCircuitPart);
				listSize = ListOfTurrets.Count;
				break;
			case ItemType.WIRES:
				index = ListOfWires.IndexOf ((Wire) selectedCircuitPart);
				listSize = ListOfWires.Count;
				break;
			case ItemType.BATTERIES:
				index = ListOfBatteries.IndexOf ((Battery) selectedCircuitPart);
				listSize = ListOfWires.Count;
				break;
			default:
				break;
		}

		if (inputManager.MouseScroll < 0) //move left
			index--;
		if (inputManager.MouseScroll > 0) //move right
			index++;

		if (index >= listSize) {
			index = 0;
		}

		if (index < 0) {
			index = listSize - 1;
		}

		CircuitPart nextSelected = null;

		switch (itemType) {
			case ItemType.TURRETS:
				nextSelected = ListOfTurrets[index];
				break;
			case ItemType.WIRES:
				nextSelected = ListOfWires[index];
				break;
			case ItemType.BATTERIES:
				nextSelected = ListOfBatteries[index];
				break;
			default:
				break;
		}

		nextSelected.visual.transform.position = selectedCircuitPart.visual.transform.position;
		selectedCircuitPart.gameObject.SetActive (false);

		selectedCircuitPart = nextSelected;
		selectedCircuitPart.gameObject.SetActive (true);
		targetToMove = selectedCircuitPart.target.gameObject;
	}
	void ChangeSelectedItemType () {

		int index = (int) selectedItemType;
		int listSize = Enum.GetNames (typeof (ItemType)).Length;

		if (inputManager.ShiftMouseScroll < 0f) //move left
			index--;
		if (inputManager.ShiftMouseScroll > 0f) //move right
			index++;

		if (index >= listSize) {
			index = 0;
		}

		if (index < 0) {
			index = listSize - 1;
		}

		selectedItemType = (ItemType) index;

		CircuitPart nextSelected = null;

		switch (selectedItemType) {
			case ItemType.TURRETS:
				nextSelected = ListOfTurrets[0];
				break;
			case ItemType.WIRES:
				nextSelected = ListOfWires[0];
				break;
			case ItemType.BATTERIES:
				nextSelected = ListOfBatteries[0];
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