using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public bool LeftClick { get; private set; }
    [HideInInspector] public float MouseScroll;
    public struct MousePosition {
        public float x, z;
    }
    public MousePosition mousePosition;

	//void Update () 
	//{
	//	LeftClick = Input.GetButtonDown ("Fire1");
	//	GetMousePos ();
	//	CycleSelectedObject();
	//}

	//void CycleSelectedObject () 
	//{
	//	MouseScroll = Input.GetAxis ("Mouse ScrollWheel");
	//}
	
	//void GetMousePos () 
	//{
	//	Vector3 mousePos = cam.ScreenToWorldPoint (Input.mousePosition);
	//	mousePosition.x = mousePos.x;
	//	mousePosition.z = mousePos.z;
	//}
}