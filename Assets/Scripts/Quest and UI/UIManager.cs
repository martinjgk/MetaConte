using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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
    private VideoPlayer FirstSkillIconVideoPlayer;
    [SerializeField]
    private Text FirstSkillIconText;

    [SerializeField]
    private GameObject SecondSkill;
    [SerializeField]
    private VideoPlayer SecondSkillIconVideoPlayer;
    [SerializeField]
    private Text SecondSkillIconText;

    [SerializeField]
    private GameObject ThirdSkill;
    [SerializeField]
    private VideoPlayer ThirdSkillIconVideoPlayer;
    [SerializeField]
    private Text ThirdSkillIconText;

    [SerializeField]
    private GameObject FourthSkill;
    [SerializeField]
    private VideoPlayer FourthSkillIconVideoPlayer;
    [SerializeField]
    private Text FourthSkillIconText;

    [SerializeField]
    private Slider NextSkillTimerSlider;

    [SerializeField]
    private Image NextSkillTimerFill;


    [SerializeField]
    private Sprite[] imageList; // List of images

    private List <string> magicName = new List<string>();

    [SerializeField]
    private float firstLevelMagicCoolTime = 7.0f;

    [SerializeField]
    private float secondLevelMagicCoolTime = 5.0f;

    [SerializeField]
    private List<VideoClip> videoClips;

    InputSignLang inputSignLang;
    private string FirstIconMagicName;
    private string SecondIconMagicName;
    private string ThirdIconMagicName;
    private string FourthIconMagicName;

    [SerializeField]
    private GameObject CurrentWorldInfoUI;

    [SerializeField]
    private Text CurrentWorldInfoUIText;

    [SerializeField]
    private string currentWorldNameText;
    // Start is called before the first frame update
    void Start()
    {
        CurrentSkill.SetActive(false);
        CurrentSkillIconText.text = "";
        InitializeMagicName();
        ShowInitState();
        inputSignLang = FindObjectOfType<InputSignLang>();
    }

    void InitializeMagicName(){
        magicName.Add("물");        //0
        magicName.Add("불");        //1
        magicName.Add("흙");        //2
        magicName.Add("바람");      //3
        magicName.Add("내리다");    //4
        magicName.Add("흐르다");    //5
        magicName.Add("주먹");      //6
        magicName.Add("지옥");      //7
        magicName.Add("뿌리다");
        magicName.Add("막다");      //9
        magicName.Add("돌아올라가다");
        magicName.Add("비");        //11
        magicName.Add("유성");
        magicName.Add("황사");
        magicName.Add("empty1");
        magicName.Add("강");        //15
        magicName.Add("화염");
        magicName.Add("산사태");
        magicName.Add("empty2");
        magicName.Add("물주먹");
        magicName.Add("불주먹");
        magicName.Add("흙주먹");    //19
        magicName.Add("empty3");
        magicName.Add("홍수");
        magicName.Add("불지옥");
        magicName.Add("지진");
        magicName.Add("empty4");
        magicName.Add("거품");
        magicName.Add("불꽃세례");  //24
        magicName.Add("흙뿌리기");
        magicName.Add("empty5");
        magicName.Add("물의 장벽");
        magicName.Add("불의 장벽");
        magicName.Add("흙의 장벽");
        magicName.Add("empty6");
        magicName.Add("회오리바람");//29
    }

    public void SetSkillDialog(string targetMagicName, List<string> usableSkills){
        if(targetMagicName == "") {
            prevMagicName = curMagicName;
            curMagicName = targetMagicName;
            return;
        }
        else if(targetMagicName != prevMagicName){
            if(isSkillDialog){
                int idx = GetIndexOfMagic(targetMagicName);
                if(targetMagicName == "None")
                {
                    //Debug.Log("SetSkillDialog None: prev: "+prevMagicName+" cur:" + targetMagicName);
                    ShowInitState();
                    currentState = 0;
                }
                else if(currentState == 0 && idx >= 0 && idx <= 3){
                    prevMagicName = curMagicName;
                    curMagicName = targetMagicName;
                    foreach (string skill in usableSkills)
                    {
                        //Debug.Log(skill);
                    }
                    ShowFirstState(idx, usableSkills);
                    currentState = 1;
                }
                else if(currentState == 1 && idx >= 4 && idx <= 10){
                    Debug.Log("SetSkillDialog Second: prev: "+prevMagicName+" cur:" + targetMagicName);
                    prevMagicName = curMagicName;
                    curMagicName = targetMagicName;
                    ShowSecondState(idx, usableSkills);
                    currentState = 2;
                }
                else if(currentState == 2 && targetMagicName == "강하다"){
                    //강하다 발동
                }
                else if(currentState == 2&& idx >= 0 && idx <= 3){
                    //None이 실행될 것.
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
    

    void SetCurrentIcon(string targetMagicName){
        if(targetMagicName == "None"){
            CurrentSkill.SetActive(false);
        }
        else if (GetIndexOfMagic(targetMagicName) != -1){
            CurrentSkill.SetActive(true);
            int idx = GetIndexOfMagic(targetMagicName);
            CurrentSkillIconImage.sprite = imageList[idx];
            CurrentSkillIconText.text = magicName[idx];
        }
    }

    void SetCurrentIconIdx(int targetMagicIdx){
        if(targetMagicIdx == -1){
            CurrentSkill.SetActive(false);
        }
        else{
            CurrentSkill.SetActive(true);
            CurrentSkillIconImage.sprite = imageList[targetMagicIdx];
            CurrentSkillIconText.text = magicName[targetMagicIdx];
        }
    }

    int GetIndexOfMagic(string targetMagicName){
        if(targetMagicName == "water")
        {
            return 0;
        }
        else if(targetMagicName == "fire")
        {
            return 1;
        } 
        else if(targetMagicName == "dirt")
        {
            return 2;
        } 
        else if(targetMagicName == "wind")
        {
            return 3;
        } 
        else if(targetMagicName == "down")
        {
            return 4;
        } 
        else if(targetMagicName == "flow")
        {
            return 5;
        } 
        else if(targetMagicName == "punch")
        {
            return 6;
        }
        else if(targetMagicName == "hell")
        {
            return 7;
        }
        else if(targetMagicName == "scatter")
        {
            return 8;
        }
        else if(targetMagicName == "block")
        {
            return 9;
        }
        else if(targetMagicName == "spin")
        {
            return 10;
        }
        else if(targetMagicName == "rain")
        {
            return 11;
        } 
        return -1;
    }

    void SetFirstIcon(string targetMagicName){
        FirstIconMagicName = targetMagicName;
        if(targetMagicName == "None"){
            FirstSkill.SetActive(false);
        }
        else{
            FirstSkill.SetActive(true);
            int index = GetIndexOfMagic(targetMagicName);
            if(index == -1)
            {
                Debug.Log("ERROR At getIndexOfMagic"+targetMagicName);
                return;
            }
            FirstSkillIconVideoPlayer.clip = videoClips[index];
            FirstSkillIconText.text = magicName[index];
        }
    }

    void SetSecondIcon(string targetMagicName){
        SecondIconMagicName = targetMagicName;
        if(targetMagicName == "None"){
            SecondSkill.SetActive(false);
        }
        else
        {
            SecondSkill.SetActive(true);
            int index = GetIndexOfMagic(targetMagicName);
            if(index == -1)
            {
                Debug.Log("ERROR At getIndexOfMagic"+targetMagicName);
                return;
            }
            SecondSkillIconVideoPlayer.clip = videoClips[index];
            SecondSkillIconText.text = magicName[index];
        } 
    }

    void SetThirdIcon(string targetMagicName){
        ThirdIconMagicName = targetMagicName;
        if(targetMagicName == "None"){
            ThirdSkill.SetActive(false);
        }
        else
        {
            ThirdSkill.SetActive(true);
            int index = GetIndexOfMagic(targetMagicName);
            if(index == -1)
            {
                Debug.Log("ERROR At getIndexOfMagic"+targetMagicName);
                return;
            }
            ThirdSkillIconVideoPlayer.clip = videoClips[index];
            ThirdSkillIconText.text = magicName[index];
        } 
    }
    void SetFourthIcon(string targetMagicName){
        FourthIconMagicName = targetMagicName;
        if(targetMagicName == "None"){
            FourthSkill.SetActive(false);
        }
        else
        {
            FourthSkill.SetActive(true);
            int index = GetIndexOfMagic(targetMagicName);
            if(index == -1)
            {
                Debug.Log("ERROR At getIndexOfMagic"+targetMagicName);
                return;
            }
            FourthSkillIconVideoPlayer.clip = videoClips[index];
            FourthSkillIconText.text = magicName[index];
        } 
    }

    void ShowInitState(){
            prevMagicName = curMagicName;
            curMagicName = "None";
            currentState = 0;
            SetCurrentIcon("None");
            SetFirstIcon("water");
            SetSecondIcon("fire");
            SetThirdIcon("dirt");
            SetFourthIcon("wind");
            NextSkillTimerSlider.gameObject.SetActive(false);
    }
    void ShowFirstState(int currentMagicIdx, List<string> usableSkills)
    {
        SetCurrentIconIdx(currentMagicIdx);
        
        if (usableSkills.Count > 0)
        {
            SetFirstIcon(usableSkills[0]);
        }
        else
        {
            SetFirstIcon("None");
        }

        if (usableSkills.Count > 1)
        {
            SetSecondIcon(usableSkills[1]);
        }
        else
        {
            SetSecondIcon("None");
        }

        if (usableSkills.Count > 2)
        {
            SetThirdIcon(usableSkills[2]);
        }
        else
        {
            SetThirdIcon("None");
        }

        if (usableSkills.Count > 3)
        {
            SetFourthIcon(usableSkills[3]);
        }
        else
        {
            SetFourthIcon("None");
        }

        UpdateCurrentSkillTime(firstLevelMagicCoolTime);
    }

    void ShowSecondState(int currentMagicIdx, List<string> usableSkills)
    {
        //Debug.Log("ShowSecondState "+currentMagicIdx.ToString()+" "+prevMagicName+" "+(11 + 4 * (currentMagicIdx - 4) + getIndexOfMagic(prevMagicName)).ToString());
        SetCurrentIconIdx(11 + 4 * (currentMagicIdx - 4) + GetIndexOfMagic(prevMagicName));
        
        if (usableSkills.Count > 0)
        {
            SetFirstIcon(usableSkills[0]);
        }
        else
        {
            SetFirstIcon("None");
        }

        if (usableSkills.Count > 1)
        {
            SetSecondIcon(usableSkills[1]);
        }
        else
        {
            SetSecondIcon("None");
        }

        if (usableSkills.Count > 2)
        {
            SetThirdIcon(usableSkills[2]);
        }
        else
        {
            SetThirdIcon("None");
        }

        if (usableSkills.Count > 3)
        {
            SetFourthIcon(usableSkills[3]);
        }
        else
        {
            SetFourthIcon("None");
        }

        UpdateCurrentSkillTime(secondLevelMagicCoolTime);
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
            UpdateSliderColor();
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
        Debug.Log("UpdateCurrentSkillTime");
        NextSkillTimerSlider.gameObject.SetActive(true);
        maxCurrentSkillTime = amount;
        curSkillTime = maxCurrentSkillTime;

        if (skillTimerCoroutine != null)
        {
            StopCoroutine(skillTimerCoroutine);
        }
        skillTimerCoroutine = StartCoroutine(SkillTimer());
    }
    private void UpdateSliderColor()
    {
        float percentage = curSkillTime / maxCurrentSkillTime;

        if (percentage > 0.6f)
        {
            NextSkillTimerFill.color = Color.green;
        }
        else if (percentage > 0.3f)
        {
            NextSkillTimerFill.color = Color.yellow;
        }
        else
        {
            NextSkillTimerFill.color = Color.red;
        }
    }

    public void StopSkillTimer()
    {
        if (skillTimerCoroutine != null)
        {
            StopCoroutine(skillTimerCoroutine);
            skillTimerCoroutine = null;
        }
    }
    public void OnFirstIconClick(){
    if (inputSignLang != null && !string.IsNullOrEmpty(FirstIconMagicName)) {
        inputSignLang.inputSign = FirstIconMagicName;
    } else {
        Debug.LogWarning("inputSignLang is null or FirstIconMagicName is null or empty");
    }
}


    public void OnSecondIconClick(){
        Debug.Log(SecondIconMagicName);
        inputSignLang.inputSign=SecondIconMagicName;
        Debug.Log(inputSignLang.inputSign);
    }
    public void OnThirdIconClick(){
        inputSignLang.inputSign=ThirdIconMagicName;
    }
    public void OnFourthIconClick(){
        inputSignLang.inputSign=FourthIconMagicName;
    }
}
