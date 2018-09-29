using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class InputManager : MonoBehaviour {
	[SerializeField] private Camera cam;
	[SerializeField] private bool useKeyboard = false;
	public bool LeftClick { get; private set; }
    [SerializeField] private VRTK_ControllerEvents lControllerEvents;
    [SerializeField] private VRTK_ControllerEvents rControllerEvents;
    [SerializeField] private float touchpadXDeadZone;

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
            LeftClick = lControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress) || rControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress);

            if (lControllerEvents.GetAxisState(VRTK_ControllerEvents.Vector2AxisAlias.Touchpad, SDK_BaseController.ButtonPressTypes.PressDown))
            {
                Vector2 lCAxis = lControllerEvents.GetAxis(VRTK_ControllerEvents.Vector2AxisAlias.Touchpad);
                if(lCAxis.x > touchpadXDeadZone || lCAxis.x < -touchpadXDeadZone)
                {
                    MouseScroll = lCAxis.x;
                }
            }
            else if (rControllerEvents.GetAxisState(VRTK_ControllerEvents.Vector2AxisAlias.Touchpad, SDK_BaseController.ButtonPressTypes.PressDown))
            {
                Vector2 rCAxis = rControllerEvents.GetAxis(VRTK_ControllerEvents.Vector2AxisAlias.Touchpad);
                if (rCAxis.x > touchpadXDeadZone || rCAxis.x < -touchpadXDeadZone)
                {
                    MouseScroll = rCAxis.x;
                }
            }
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