using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private string prevMagicName = "";
    private string curMagicName = "";
    private int currentState = 0;
    private bool isSkillDialog = true;
    [SerializeField]
    private GameObject CurrentSkill;
    [SerializeField]
    private Image CurrentSkillIconImage;
    [SerializeField]
    private Text CurrentSkillIconText;

    [SerializeField]
    private GameObject FirstSkill;
    [SerializeField]
    private Image FirstSkillIconImage;
    [SerializeField]
    private Text FirstSkillIconText;

    [SerializeField]
    private GameObject SecondSkill;
    [SerializeField]
    private Image SecondSkillIconImage;
    [SerializeField]
    private Text SecondSkillIconText;

    [SerializeField]
    private GameObject ThirdSkill;
    [SerializeField]
    private Image ThirdSkillIconImage;
    [SerializeField]
    private Text ThirdSkillIconText;

    [SerializeField]
    private Slider NextSkillTimerSlider;


    [SerializeField]
    private Sprite[] imageList; // List of images

    private List <string> magicName = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        CurrentSkill.SetActive(false);
        CurrentSkillIconText.text = "";
        InitializeMagicName();
        ShowInitState();
    }

    void InitializeMagicName(){
        magicName.Add("물");
        magicName.Add("내리다");
        magicName.Add("비");
        magicName.Add("불");
        magicName.Add("주먹");
        magicName.Add("지옥");
        magicName.Add("흙");
    }

    public void SetSkillDialog(string targetMagicName){
        if(isSkillDialog){
            if(targetMagicName == "None")
            {
                ShowInitState();
                currentState = 0;
            }
            else if(currentState == 0 && targetMagicName == "water"){
                ShowFirstState("water");
                currentState = 1;
            }

            else if(currentState == 1 && targetMagicName == "down"){
                ShowSecondState("down");
                currentState = 1;
            }
            Debug.Log("prev: " + prevMagicName + ", cur: "+curMagicName);
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(isSkillDialog){
            if(Input.GetKeyDown(KeyCode.G)){
                ShowInitState();
                currentState = 0;
            }
            else if(currentState == 0 && Input.GetKeyDown(KeyCode.H)){
                ShowFirstState("water");
                currentState = 1;
            }

            else if(currentState == 1 && Input.GetKeyDown(KeyCode.J)){
                ShowSecondState("down");
                currentState = 1;
            }
            
        }
    }
    

    void SetCurrentIcon(string targetMagicName){
        if(targetMagicName == "None"){
            CurrentSkill.SetActive(false);
        }
        if(targetMagicName == "water"){
            CurrentSkill.SetActive(true);
            CurrentSkillIconImage.sprite = imageList[0];
            CurrentSkillIconText.text = magicName[0];
        } 
        if(targetMagicName == "rain"){
            CurrentSkill.SetActive(true);
            CurrentSkillIconImage.sprite = imageList[2];
            CurrentSkillIconText.text = magicName[2];
        } 
    }

    int getIndexOfMagic(string targetMagicName){
        if(targetMagicName == "water")
        {
            return 0;
        } 
        else if(targetMagicName == "down")
        {
            return 1;
        } 
        else if(targetMagicName == "rain")
        {
            return 2;
        } 
        else if(targetMagicName == "fire")
        {
            return 3;
        }
        else if(targetMagicName == "punch")
        {
            return 4;
        }
        else if(targetMagicName == "hell")
        {
            return 5;
        }
        else if(targetMagicName == "dirt")
        {
            return 6;
        }
        return -1;
    }

    void SetFirstIcon(string targetMagicName){
        if(targetMagicName == "None"){
            FirstSkill.SetActive(false);
        }
        else{
            FirstSkill.SetActive(true);
            int index = getIndexOfMagic(targetMagicName);
            if(index == -1)
            {
                Debug.Log("ERROR At getIndexOfMagic"+targetMagicName);
                return;
            }
            FirstSkillIconImage.sprite = imageList[index];
            FirstSkillIconText.text = magicName[index];
        }
    }

    void SetSecondIcon(string targetMagicName){
        if(targetMagicName == "None"){
            SecondSkill.SetActive(false);
        }
        else
        {
            SecondSkill.SetActive(true);
            int index = getIndexOfMagic(targetMagicName);
            if(index == -1)
            {
                Debug.Log("ERROR At getIndexOfMagic"+targetMagicName);
                return;
            }
            SecondSkillIconImage.sprite = imageList[index];
            SecondSkillIconText.text = magicName[index];
        } 
    }

    void SetThirdIcon(string targetMagicName){
        if(targetMagicName == "None"){
            ThirdSkill.SetActive(false);
        }
        else
        {
            ThirdSkill.SetActive(true);
            int index = getIndexOfMagic(targetMagicName);
            if(index == -1)
            {
                Debug.Log("ERROR At getIndexOfMagic"+targetMagicName);
                return;
            }
            ThirdSkillIconImage.sprite = imageList[index];
            ThirdSkillIconText.text = magicName[index];
        } 
    }

    void ShowInitState(){
            prevMagicName = curMagicName;
            curMagicName = "";
            Debug.Log("prev: " + prevMagicName + ", cur: "+curMagicName);
            currentState = 0;
            SetCurrentIcon("None");
            SetFirstIcon("water");
            SetSecondIcon("fire");
            SetThirdIcon("dirt");
            NextSkillTimerSlider.gameObject.SetActive(false);
    }
    void ShowFirstState(string currentMagicName){
        if(currentMagicName == "water"){
            prevMagicName = curMagicName;
            curMagicName = currentMagicName;
            Debug.Log("prev: " + prevMagicName + ", cur: "+curMagicName);
            SetCurrentIcon(currentMagicName);
            SetFirstIcon("down");
            SetSecondIcon("None");
            SetThirdIcon("None");
            UpdateCurrentSkillTime(7.0f);
        }
    }

    void ShowSecondState(string currentMagicName){
        if(currentMagicName == "down"){
            prevMagicName = curMagicName;
            curMagicName = currentMagicName;
            Debug.Log("prev: " + prevMagicName + ", cur: "+curMagicName);
            SetCurrentIcon("rain");
            SetFirstIcon("water");
            SetSecondIcon("fire");
            SetThirdIcon("dirt");
            StopSkillTimer();
            NextSkillTimerSlider.gameObject.SetActive(false);
        }
    }


    // 원소 마법 사용했을 때, 결합 유효 시간 표시 타이머
    protected float curSkillTime; // 현재 시간
    public float maxCurrentSkillTime; // 최대 시간
    private Coroutine skillTimerCoroutine;
    public void SetCurrentSkillTime(float amount) //시간 설정
    {
        maxCurrentSkillTime = amount;
        curSkillTime = maxCurrentSkillTime;
    }

    public void CheckCurrentSkillTime()
    {
        if (NextSkillTimerSlider != null)
            NextSkillTimerSlider.value = curSkillTime / maxCurrentSkillTime;
    }
    private IEnumerator SkillTimer()
    {
        while (curSkillTime > 0)
        {
            curSkillTime -= 0.1f;
            CheckCurrentSkillTime();
            yield return new WaitForSeconds(0.1f);
        }
        curSkillTime = 0;
        ShowInitState();
        CheckCurrentSkillTime();
    }
    public void UpdateCurrentSkillTime(float amount)
    {
        NextSkillTimerSlider.gameObject.SetActive(true);
        maxCurrentSkillTime = amount;
        curSkillTime = maxCurrentSkillTime;

        if (skillTimerCoroutine != null)
        {
            StopCoroutine(skillTimerCoroutine);
        }
        skillTimerCoroutine = StartCoroutine(SkillTimer());
    }

    public void StopSkillTimer()
    {
        if (skillTimerCoroutine != null)
        {
            StopCoroutine(skillTimerCoroutine);
            skillTimerCoroutine = null;
        }
    }
}
