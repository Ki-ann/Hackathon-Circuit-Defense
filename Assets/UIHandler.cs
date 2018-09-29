using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private CircuitPlacer circuitPlacer;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject shopMenu;

    [SerializeField] private ShopHandler sh;

    [SerializeField] private List<ShopItem> turretSI;
    [SerializeField] private List<ShopItem> wireSI;
    [SerializeField] private List<ShopItem> batterySI;

    List<ShopItem> shopItems = null;

    private int index = 0;

    public void UpdatePlacer(int itemType)
    {
        circuitPlacer.SetSelectedItemType((CircuitPlacer.ItemType)itemType);
        Debug.Log("Changed to: " + (CircuitPlacer.ItemType)itemType);

        mainMenu.SetActive(false);
        shopMenu.SetActive(true);

        switch ((CircuitPlacer.ItemType)itemType)
        {
            case CircuitPlacer.ItemType.TURRETS:
                shopItems = turretSI;
                break;
            case CircuitPlacer.ItemType.WIRES:
                shopItems = wireSI;
                break;
            case CircuitPlacer.ItemType.BATTERIES:
                shopItems = batterySI;
                break;
            default:
                break;
        }

        index = 0;
        sh.OnValueChange(shopItems, 0);
    }

    public void ScrollShop(int scrollBy)
    {
        index = index + scrollBy;

        if (index < 0)
        {
            index = 0;
        }

        if (index == shopItems.Count - 1)
        {
            index = shopItems.Count - 1;
        }

        sh.OnValueChange(shopItems, index);
    }

    public void BackToMain()
    {
        mainMenu.SetActive(true);
        shopMenu.SetActive(false);
    }
}