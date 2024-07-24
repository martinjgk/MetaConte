using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject inventoryUI;
    public TMP_Text warningText;
    public TMP_Text goldText;
    private int gold = 500; //player에게 부여해야함
    public Inventory inventory;
    private bool isShopOpen = false;

     void Start()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory is not assigned in the ShopManager script!");
        }
        else
        {
            Debug.Log("Inventory is assigned correctly.");
        }

        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }

        shopUI.SetActive(false);
        inventoryUI.SetActive(false);

        UpdateGoldText();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleShopUI();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryUI();
        }
    }

    void ToggleShopUI()
    {
        isShopOpen = !isShopOpen;
        shopUI.SetActive(isShopOpen);

        if (isShopOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void ToggleInventoryUI()
    {
        inventoryUI.SetActive(!inventoryUI.activeSelf);
    }

   public void BuyArmor(Item armorItem)
    {
        if (gold >= 100)
        {
            gold -= 100;
            inventory.AddItem(armorItem);
            UpdateGoldText();
            HideWarning();
        }
        else
        {
            ShowWarning();
        }
    }

    public void BuyCloak(Item cloakItem)
    {
        if (gold >= 80)
        {
            gold -= 80;
            inventory.AddItem(cloakItem);
            UpdateGoldText();
            HideWarning();
        }
        else
        {
            ShowWarning();
        }
    }

    public void BuyBoots(Item bootsItem)
    {
        if (gold >= 50)
        {
            gold -= 50;
            inventory.AddItem(bootsItem);
            UpdateGoldText();
            HideWarning();
        }
        else
        {
            ShowWarning();
        }
    }

    private void ShowWarning()
    {
        if (warningText != null)
        {
            warningText.text = "Not Enough Gold";
            warningText.gameObject.SetActive(true);
        }
    }

    private void HideWarning()
    {
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false);
        }
    }

    private void UpdateGoldText()
    {
        if (goldText != null)
        {
            goldText.text = "Gold: " + gold.ToString();
        }
    }
}
