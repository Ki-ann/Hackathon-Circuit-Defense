using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEnabler : MonoBehaviour
{
    [SerializeField] private Behaviour behaviour;

    private void OnEnable()
    {
        StartCoroutine(EnableAfter(1.3f, true));
    }

    private void OnDisable()
    {
        StartCoroutine(EnableAfter(1.3f, false));
    }

    private IEnumerator EnableAfter(float seconds, bool enableState)
    {
        yield return new WaitForSeconds(seconds);
        behaviour.enabled = enableState;
    }
}
