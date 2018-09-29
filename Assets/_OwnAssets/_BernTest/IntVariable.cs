using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class IntVariable : ScriptableObject
{
    public int Value;

    [SerializeField] private int Constant;

    private void OnEnable()
    {
        Value = Constant;
    }
}
