using UnityEngine;
using UnityEngine.UI;

public class BookPanelManager : MonoBehaviour
{
    public GameObject bookPanel;
    public GameObject bookPanelCloseButton;
    public GameObject buttonObject;
    public GameObject QuestButtton;

    public Image displayImage; // Display image component
    public Text displayText; // Display text component

    public Sprite[] imageList; // List of images

    [TextArea(3, 10)]
    public string[] textList; // List of texts

    private int currentIndex = 0; // Current index of the list

    void Start()
    {
        UpdateContent();
    }

    public void OnButtonClick()
    {
		Debug.Log("Clicked");
        if (bookPanel != null)
        {
            bookPanel.SetActive(true);
            buttonObject.SetActive(false);
        }
        else
        {
            Debug.LogError("BookPanel is not assigned.");
        }
        if (bookPanelCloseButton != null)
        {
            bookPanelCloseButton.SetActive(true);
        }
        else
        {
            Debug.LogError("bookPanelCloseButton is not assigned.");
        }
        if (QuestButtton != null)
        {
            QuestButtton.SetActive(false);
            
        }
        else
        {
            Debug.LogError("QuestButtton is not assigned.");
        }
    }

    public void PreviousPage()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateContent();
        }
    }

    public void NextPage()
    {
        if (currentIndex < imageList.Length - 1) // Assuming imageList and textList are of the same length
        {
            currentIndex++;
            UpdateContent();
        }
    }

    void UpdateContent()
    {
        if (imageList.Length > 0 && textList.Length > 0)
        {
            displayImage.sprite = imageList[currentIndex];
            displayText.text = textList[currentIndex];
        }
    }
}
