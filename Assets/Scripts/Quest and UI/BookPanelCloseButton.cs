using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookPanelCloseButton : MonoBehaviour
{
    public GameObject bookPanel;
    public GameObject bookPanelCloseButton;
    public GameObject buttonObject;

    public GameObject QuestButtton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnButtonClick()
    {
        if (bookPanel != null)
        {
            bookPanel.SetActive(false);
            
        }
        else
        {
            Debug.LogError("BookPanel is not assigned.");
        }
        if (bookPanelCloseButton != null)
        {
            bookPanelCloseButton.SetActive(false);
            
        }
        else
        {
            Debug.LogError("bookPanelCloseButton is not assigned.");
        }
        if (buttonObject != null)
        {
            //buttonObject.SetActive(true);
            
        }
        else
        {
            Debug.LogError("buttonObject is not assigned.");
        }
        if (QuestButtton != null)
        {
            //QuestButtton.SetActive(true);
            
        }
        else
        {
            Debug.LogError("QuestButtton is not assigned.");
        }
    }
}
