using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public GameObject shopUI;
    public GameObject inventoryUI;
    public TMP_Text warningText;
    public TMP_Text goldText;
    public Inventory inventory;
    private bool isShopOpen = false;

    public Player player;

    public AudioClip purchaseSound; // 구매 소리 클립
    private AudioSource audioSource; // AudioSource 컴포넌트

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

    public void BuyItem(Item item)
    {
        if (player.SpendGold(item.itemPrice)) // Player의 골드를 차감
        {
            inventory.AddItem(item); // 아이템 인벤토리에 추가
            item.Use(player); // 아이템 사용
            UpdateGoldText(); // 금액 텍스트 업데이트
            HideWarning(); // 경고 메시지 숨기기
            PlayPurchaseSound(); // 구매 소리 재생
        }
        else
        {
            ShowWarning(); // 자금 부족 경고
        }
    }


    private void ShowWarning()
    {
        if (warningText != null)
        {
            warningText.text = "골드가 충분하지 않습니다.";
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
            goldText.text = "Gold: " + player.GetGold().ToString(); // Player의 골드를 가져와서 업데이트
        }
    }

    private void PlayPurchaseSound()
    {
        if (audioSource != null && purchaseSound != null)
        {
            audioSource.PlayOneShot(purchaseSound);
        }
        else
        {
            Debug.LogError("Purchase sound or AudioSource is not set.");
        }
    }
}
