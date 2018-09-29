using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PlacementPointer : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private BoolVariable placementState;
    [SerializeField] private Transform pointerOrigin;
    [SerializeField] private LineRenderer lr;
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
            lr.enabled = true;

            RaycastHit hit;
            lr.SetPosition(0, pointerOrigin.position);

            if (Physics.Raycast(pointerOrigin.position, pointerOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                lr.startColor = Color.green;
                lr.endColor = Color.green;
                lr.SetPosition(1, hit.point);
                Debug.DrawRay(pointerOrigin.position, pointerOrigin.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                inputManager.mousePosition.x = hit.point.x;
                inputManager.mousePosition.z = hit.point.z;
            }
            else
            {
                lr.startColor = Color.red;
                lr.endColor = Color.red;
                lr.SetPosition(1, pointerOrigin.TransformDirection(Vector3.forward) * 1000);
                Debug.DrawRay(pointerOrigin.position, pointerOrigin.TransformDirection(Vector3.forward) * 1000, Color.red);
            }
        }
        else
        {
            lr.enabled = false;
        }
    }
}