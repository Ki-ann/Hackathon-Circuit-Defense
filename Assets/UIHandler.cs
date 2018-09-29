using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private CircuitPlacer circuitPlacer;

    public void UpdatePlacer(int itemType)
    {
        circuitPlacer.SetSelectedItemType((CircuitPlacer.ItemType)itemType);
    }
}