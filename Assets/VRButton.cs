using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using VRTK.Controllables;

public class VRButton : MonoBehaviour
{
    public VRTK_BaseControllable controllable;
    public string outputOnMax = "Maximum Reached";
    public string outputOnMin = "Minimum Reached";
    [SerializeField] private UnityEvent onButtonDown;

    protected virtual void OnEnable()
    {
        controllable = (controllable == null ? GetComponent<VRTK_BaseControllable>() : controllable);
        controllable.MaxLimitReached += MaxLimitReached;
        controllable.MinLimitReached += MinLimitReached;
    }

    protected virtual void MaxLimitReached(object sender, ControllableEventArgs e)
    {
        if (outputOnMax != "")
        {
            Debug.Log(outputOnMax);
        }

        onButtonDown.Invoke();
    }

    protected virtual void MinLimitReached(object sender, ControllableEventArgs e)
    {
        if (outputOnMin != "")
        {
            Debug.Log(outputOnMin);
        }
    }
}
