using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public GameObject shopPanel;      // 상점 창
    public GameObject inventoryPanel; // 인벤토리 창

    private bool isShopOpen = false;      // 상점 창 열림 여부
    private bool isInventoryOpen = false; // 인벤토리 창 열림 여부

    public void ToggleShop()
    {
        isShopOpen = !isShopOpen;
        shopPanel.SetActive(isShopOpen);
    }

    public void ToggleInventory()
    {
        isInventoryOpen = !isInventoryOpen;
        inventoryPanel.SetActive(isInventoryOpen);
    }
}
