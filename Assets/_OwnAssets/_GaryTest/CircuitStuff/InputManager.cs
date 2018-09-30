using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private bool useKeyboard = false;
    public bool LeftClick { get; private set; }

    [SerializeField] private VRTK_ControllerEvents lControllerEvents;
    [SerializeField] private VRTK_ControllerEvents rControllerEvents;
    [SerializeField] private float touchpadXDeadZone;

    [SerializeField] private UIHandler uIHandler;

    [HideInInspector] public float MouseScroll;
    [HideInInspector] public float ShiftMouseScroll;
    private bool shiftDown;

    public struct MousePosition
    {
        public float x, z;
    }
    public MousePosition mousePosition;

    private void Start()
    {
        lControllerEvents.TouchpadReleased += LControllerEvents_TouchpadReleased;
        rControllerEvents.TouchpadReleased += RControllerEvents_TouchpadReleased;
    }

    private void RControllerEvents_TouchpadReleased(object sender, ControllerInteractionEventArgs e)
    {
        Vector2 rCAxis = rControllerEvents.GetAxis(VRTK_ControllerEvents.Vector2AxisAlias.Touchpad);
        if (rCAxis.x > touchpadXDeadZone || rCAxis.x < -touchpadXDeadZone)
        {
            Debug.Log("NO");
            if (rCAxis.x > touchpadXDeadZone)
            {
                uIHandler.ScrollShop(1);
                MouseScroll = 1;
            }
            else if (rCAxis.x < -touchpadXDeadZone)
            {
                uIHandler.ScrollShop(-1);
                MouseScroll = -1;
            }
        }
    }

    private void LControllerEvents_TouchpadReleased(object sender, ControllerInteractionEventArgs e)
    {
        Vector2 lCAxis = lControllerEvents.GetAxis(VRTK_ControllerEvents.Vector2AxisAlias.Touchpad);
        if (lCAxis.x > touchpadXDeadZone || lCAxis.x < -touchpadXDeadZone)
        {
            Debug.Log("YES");
            if (lCAxis.x > touchpadXDeadZone)
            {
                uIHandler.ScrollShop(1);
                MouseScroll = 1;
            }
            else if (lCAxis.x < -touchpadXDeadZone)
            {
                uIHandler.ScrollShop(-1);
                MouseScroll = -1;
            }
        }
    }

    void Update()
    {
        if (useKeyboard)
        {
            LeftClick = Input.GetButtonDown("Fire1");
            GetMousePos();
            GetBothShift();
            CycleSelectedObject();
            CycleSelectedItemType();
        }
        else
        {
            LeftClick = lControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress) || rControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TriggerPress);
            
        }
    }

    void CycleSelectedItemType()
    {
        ShiftMouseScroll = shiftDown ? Input.GetAxis("Mouse ScrollWheel") : 0;

    }
    void CycleSelectedObject()
    {
        MouseScroll = shiftDown ? 0 : MouseScroll = Input.GetAxis("Mouse ScrollWheel");
    }

    void GetBothShift()
    {
        shiftDown = (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
    }
    void GetMousePos()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.x = mousePos.x;
        mousePosition.z = mousePos.z;
    }
}