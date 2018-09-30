using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardItem : MonoBehaviour
{
    public Transform faceTarget;
    
    private void LateUpdate()
    {
        if (!faceTarget){
            faceTarget = VRTK.VRTK_SDKManager.GetLoadedSDKSetup().actualHeadset.transform;
            return;
        }

        transform.LookAt(transform.position + faceTarget.rotation * Vector3.forward, faceTarget.rotation * Vector3.up);
    }
}
