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

        GoToShop();
    }

    public void ScrollShop(int scrollBy)
    {
        index = index + scrollBy;

        if (index < 0)
        {
            index = shopItems.Count - 1;
        }

        if (index == shopItems.Count)
        {
            index = 0;
        }

        sh.OnValueChange(shopItems, index);
    }

    private void GoToShop()
    {
        StartCoroutine(DelayShopChange());
    }

    public void GoToMain()
    {
        StartCoroutine(DelayMainChange());
    }

    private IEnumerator DelayShopChange()
    {
        yield return new WaitForSeconds(0.3f);
        mainMenu.SetActive(false);
        shopMenu.SetActive(true);
    }

    private IEnumerator DelayMainChange()
    {
        yield return new WaitForSeconds(0.3f);
        shopMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}