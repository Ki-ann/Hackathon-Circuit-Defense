using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopHandler : MonoBehaviour
{
    //[SerializeField] private int selectedIndex = 0;
    [SerializeField] private Transform leftTrans;
    [SerializeField] private Transform middleTrans;
    [SerializeField] private Transform rightTrans;
    [SerializeField] private TextMeshPro leftNameTMP;
    [SerializeField] private TextMeshPro middleNameTMP;
    [SerializeField] private TextMeshPro rightNameTMP;
    [SerializeField] private TextMeshPro leftCostTMP;
    [SerializeField] private TextMeshPro middleCostTMP;
    [SerializeField] private TextMeshPro rightCostTMP;
    [SerializeField] private CircuitPlacer circuitPlacer;

    public void OnValueChange(List<ShopItem> shopItems, int selected)
    {
        PrepareShop(selected == 0 ? null : shopItems[selected - 1], shopItems[selected], (selected + 1) >= shopItems.Count ? null : shopItems[selected + 1]);
        circuitPlacer.SelectWithName(shopItems[selected].ItemName);
    }

    private void PrepareShop(ShopItem leftObj, ShopItem middleObj, ShopItem rightObj)
    {
        ClearShop(leftTrans, leftNameTMP, leftCostTMP);
        ClearShop(middleTrans, middleNameTMP, middleCostTMP);
        ClearShop(rightTrans, rightNameTMP, rightCostTMP);

        PrepareItem(leftObj, leftTrans, leftNameTMP, leftCostTMP);
        PrepareItem(middleObj, middleTrans, middleNameTMP, middleCostTMP);
        PrepareItem(rightObj, rightTrans, rightNameTMP, rightCostTMP);
    }

    private void ClearShop(Transform parent, TextMeshPro tmpName, TextMeshPro tmpCost)
    {
        if (parent.childCount <= 0)
        {
            return;
        }

        Destroy(parent.GetChild(0).gameObject);
        tmpName.text = "";
        tmpCost.text = "";
    }

    private void PrepareItem(ShopItem shopObj, Transform parent, TextMeshPro tmpName, TextMeshPro tmpCost)
    {
        if (!shopObj)
        {
            return;
        }

        tmpName.text = shopObj.ItemName;
        tmpCost.text = "$" + shopObj.ItemCost;

        GameObject go = Instantiate(shopObj.ItemPrefab, parent);
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
    }
}