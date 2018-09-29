using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShopItem : ScriptableObject
{
    public string ItemName;
    public int ItemCost;
    public GameObject ItemPrefab;
}