using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementPointer : MonoBehaviour
{
    private InputManager inputManager;
    [SerializeField] private BoolVariable placementState;
    int layerMask;

    private void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        layerMask = LayerMask.NameToLayer("Ground");
        layerMask = ~layerMask;
    }

    private void Update()
    {
        if (placementState.Value)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
                inputManager.mousePosition.x = hit.point.x;
                inputManager.mousePosition.z = hit.point.z;
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.red);
            }
        }
    }
}