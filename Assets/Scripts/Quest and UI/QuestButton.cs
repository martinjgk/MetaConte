using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    public QuestManager questManager; // QuestManager 참조 추가

    [SerializeField]
    private GameObject QuestDialog;
    [SerializeField]
    private Text QuestText_Discription;
    [SerializeField]
    private Text QuestText_name;
    private Queue<string> sentences;
    private bool isDialogActive = false;
    [SerializeField]
    private Text NewQuestText;

    [SerializeField]
    private GameObject StatUI;
    void Start()
    {
        //QuestDialog = GameObject.Find("QuestDialog");
        //QuestText_Discription = GameObject.Find("QuestText_Discription").GetComponent<Text>();
        //QuestText_name = GameObject.Find("QuestText_name").GetComponent<Text>();
        QuestDialog.SetActive(false);
		questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();

        sentences = new Queue<string>();

    }

    public void OnButtonClick()
    {
        StatUI.SetActive(false);
        NewQuestText.text = "";
        if (QuestDialog != null)
        {
            QuestDialog.SetActive(true);
            sentences.Clear();
            
            var currentQuest = questManager.GetCurrentQuest(); // 현재 퀘스트 가져오기
            string[] lines = currentQuest.content.Split('\n'); // 퀘스트 내용 분리
            foreach (string line in lines)
            {
                sentences.Enqueue(line);
            }

            QuestText_name.text = currentQuest.questName; // 퀘스트 이름 표시

            DisplayNextSentence();
            isDialogActive = true;
            if(questManager.GetCurrentIndex() == 0)
            {
                questManager.NextQuest();
            }
        }
        else
        {
            Debug.LogError("QuestDialog is not assigned.");
        }
    }

    void Update()
    {
        if (isDialogActive && Input.GetKeyDown(KeyCode.N))
        {
            DisplayNextSentence();
        }
    }

    private void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        QuestText_Discription.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            QuestText_Discription.text += letter;
            yield return null;
        }
    }

    private void EndDialog()
    {
        QuestDialog.SetActive(false);
        StatUI.SetActive(true);
        isDialogActive = false;
    }
}
