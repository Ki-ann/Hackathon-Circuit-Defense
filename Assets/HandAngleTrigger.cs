using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAngleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject triggerItem;
    [SerializeField] private float speedThreshold;

    private float prevAngle;

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
        prevAngle = transform.rotation.z;
    }

    private void Update()
    {
        if (otherTrigger.activeState)
        {
            return;
        }

        if (!activeState && !otherTrigger.activeState)
        {
            placementMode.Value = false;
        }

        //if (Mathf.Abs(transform.rotation.eulerAngles.z - prevAngle) / Time.deltaTime > speedThreshold)
        //{
        //    Debug.Log(Mathf.Abs(transform.rotation.eulerAngles.z - prevAngle) / Time.deltaTime);
        //    activeState = !activeState;
        //}

        if(Mathf.Abs(transform.rotation.z - prevAngle) > speedThreshold)
        {
            Debug.Log(Mathf.Abs(transform.rotation.z - prevAngle));
            activeState = !activeState;
        }

        prevAngle = transform.rotation.z;

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
