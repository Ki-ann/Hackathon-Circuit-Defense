using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform followTarget;

    private void LateUpdate()
    {
        transform.position = followTarget.position;
    }

    public void SetFollowTarget(Transform followTarg)
    {
        followTarget = followTarg;
    }
}
