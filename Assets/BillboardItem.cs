using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardItem : MonoBehaviour
{
    public Transform faceTarget;
    
    private void LateUpdate()
    {
        if (!faceTarget)
            return;

        transform.LookAt(transform.position + faceTarget.rotation * Vector3.forward, faceTarget.rotation * Vector3.up);
    }
}
