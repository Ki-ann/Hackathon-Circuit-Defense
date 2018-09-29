using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlacementPointer : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private BoolVariable placementState;
    [SerializeField] private Transform pointerOrigin;
    int layerMask;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        layerMask = LayerMask.NameToLayer("Ground");
        layerMask = ~layerMask;
    }

    public void SetPointerOrigin(Transform originTrans)
    {
        pointerOrigin = originTrans;
    }

    private void Update()
    {
        if (placementState.Value)
        {
            RaycastHit hit;

            if (Physics.Raycast(pointerOrigin.position, pointerOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(pointerOrigin.position, pointerOrigin.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                inputManager.mousePosition.x = hit.point.x;
                inputManager.mousePosition.z = hit.point.z;
            }
            else
            {
                Debug.DrawRay(pointerOrigin.position, pointerOrigin.TransformDirection(Vector3.forward) * 1000, Color.red);
            }
        }
    }
}