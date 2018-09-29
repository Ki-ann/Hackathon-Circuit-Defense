using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAngleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject triggerItem;
    [SerializeField] private float lowerAngleLimit;
    [SerializeField] private float upperAngleLimit;
    [SerializeField] private Transform uiPos;
    [SerializeField] private PlacementPointer pp;
    [SerializeField] private HandAngleTrigger otherTrigger;
    [SerializeField] private BoolVariable placementMode;
    [HideInInspector] public bool activeState = false;
    private FollowTransform ft;
    private bool prevState = false;

    private void Start()
    {
        ft = triggerItem.GetComponent<FollowTransform>();
    }

    private void Update()
    {
        if (otherTrigger.activeState)
        {
            return;
        }

        if(!activeState && !otherTrigger.activeState)
        {
            placementMode.Value = false;
        }

        if (transform.rotation.eulerAngles.z > lowerAngleLimit && transform.rotation.eulerAngles.z < upperAngleLimit)
        {
            activeState = true;
        }
        else
        {
            activeState = false;
        }

        if (activeState != prevState)
        {
            prevState = activeState;

            triggerItem.SetActive(activeState);
            
            if (activeState)
            {
                ft.SetFollowTarget(uiPos);
                pp.SetPointerOrigin(otherTrigger.transform);
                placementMode.Value = true;
            }
        }
    }
}
