using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnabler : MonoBehaviour
{
    [SerializeField] private Behaviour behaviour;

    private void OnEnable()
    {
        behaviour.enabled = true;
    }

    //private void OnEnable()
    //{

    //}

    //private IEnumerator  
}
