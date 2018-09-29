using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class InputManager : MonoBehaviour {
	[SerializeField] private Camera cam;
	[SerializeField] private bool useKeyboard = false;
	public bool LeftClick { get; private set; }
    [SerializeField] private VRTK_ControllerEvents lControllerEvents;
    [SerializeField] private VRTK_ControllerEvents RControllerEvents;

    [HideInInspector] public float MouseScroll;
	public struct MousePosition {
		public float x, z;
	}
	public MousePosition mousePosition;

	void Update () 
	{
		if (useKeyboard) {
			LeftClick = Input.GetButtonDown ("Fire1");
			GetMousePos ();
			CycleSelectedObject ();
		}
        else
        {
            LeftClick = lControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress) || RControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress);
        }
	}

	void CycleSelectedObject () 
	{
		MouseScroll = Input.GetAxis ("Mouse ScrollWheel");
	}

	void GetMousePos () 
	{
		Vector3 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);
		mousePosition.x = mousePos.x;
		mousePosition.z = mousePos.z;
	}
}