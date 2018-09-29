using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAngleTrigger : MonoBehaviour
{
    [SerializeField] private GameObject triggerItem;
    [SerializeField] private float lowerAngleLimit;
    [SerializeField] private float upperAngleLimit;
    private bool state = false;
    private bool prevState = false;

    private void Update()
    {
        if (transform.rotation.eulerAngles.z > lowerAngleLimit && transform.rotation.eulerAngles.z < upperAngleLimit)
        {
            state = true;
        }
        else
        {
            state = false;
        }

        if (state != prevState)
        {
            prevState = state;

            triggerItem.SetActive(state);
        }
    }
}
