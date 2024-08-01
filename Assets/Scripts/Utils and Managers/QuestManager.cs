using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class QuestManager : MonoBehaviour
{
    [SerializeField]
    private int playerLevel = 1;
    private int playerExp = 0;
    Dictionary<int, int> playerExpToLevelUp = new Dictionary<int, int>()
	{
		{0, 0},
        {1, 0}, {2, 50}, {3, 50}, {4, 50}, {5, 50},
        {6, 100}, {7, 100}, {8, 100}, {9, 100}, {10, 100},
        {11, 250}, {12, 250}, {13, 250}, {14, 250}, {15, 250},
        {16, 500}, {17, 500}, {18, 500}, {19, 500}, {20, 500},
	};

    [System.Serializable]
    public class Quest
    {
        public int questNo;
        public string questName;
        public string npcType;
        public string content;
        public string target;
        public int clearExp;
    }

    public List<Quest> quests = new List<Quest>();
    private int currentQuestIndex = 0;
    
    PlayerIngameProfileManager playerIngameProfileManager;

    [SerializeField]
    private Text NewQuestText;

    [SerializeField]
    private int numMonsterKill = 0;

    [SerializeField]
    GameObject boss;

    [SerializeField]
    private GameObject QuestDoneUI;

    [SerializeField]
    private GameObject MonsterKillUI;
    [SerializeField]
    private Text MonsterKillText;
    void Start()
    {
        playerIngameProfileManager = FindObjectOfType<PlayerIngameProfileManager>();
        playerIngameProfileManager.UpdatePlayerLevelUI(playerLevel);
        Debug.Log((playerExp - playerExpToLevelUp[playerLevel - 1]).ToString()+" "+(playerExpToLevelUp[playerLevel]).ToString());
        playerIngameProfileManager.SetExp(playerExp - playerExpToLevelUp[playerLevel], playerExpToLevelUp[playerLevel + 1]);
        InitializeQuests();
        NewQuestText.text = "";
        NextQuest();
        boss.SetActive(false);
        QuestDoneUI.SetActive(false);
        MonsterKillUI.SetActive(false);
    }

    void InitializeQuests()
    {
        quests.Add(new Quest() { questNo = 0, questName = "Init", npcType = "Init", content = "Init", target = "", clearExp =0 });
        quests.Add(new Quest() { questNo = 1, questName = "새로운 희망", npcType = "story", content = "스승님의 조수 주변으로 이동해 말을 걸어보기", target = "", clearExp =60 });
        quests.Add(new Quest() { questNo = 2, questName = "물의 길", npcType = "sign", content = "물의 왕국 주민 쿠아쿠아에게 감사 인사 전해주기. WASD를 사용하여 이동할 수 있습니다.", target = "", clearExp =60 });
        quests.Add(new Quest() { questNo = 3, questName = "매직 앤 머지", npcType = "", content = "스승님의 조수 온에게 다가가 말을 걸기", target = "", clearExp =50 });
        quests.Add(new Quest() { questNo = 4, questName = "화마를 찾아서", npcType = "", content = "화마를 지키는 몬스터 3마리를 사냥하기", target = "", clearExp =50 });
        quests.Add(new Quest() { questNo = 5, questName = "물 흐르듯", npcType = "", content = "물의 왕국 주민 쿠아쿠아 주변으로 이동해 말을 걸어보기", target = "", clearExp =50 });
        quests.Add(new Quest() { questNo = 6, questName = "깨어난 화마", npcType = "", content = "화마의 선발대 몬스터 5마리를 사냥하기", target = "", clearExp =100 });
        quests.Add(new Quest() { questNo = 7, questName = "비를 내려다오", npcType = "", content = "스승님의 조수 온 주변으로 이동해 말을 걸어보기", target = "", clearExp =50 });
        quests.Add(new Quest() { questNo = 8, questName = "화마의 최후", npcType = "", content = "Boss 화마를 무찔러 가뭄을 끝내기", target = "", clearExp =100 });
        quests.Add(new Quest() { questNo = 9, questName = "첫 번째 여정을 마치며", npcType = "", content = "스승님의 조수 온 주변으로 이동해 말을 걸어보기", target = "", clearExp =100 });
        quests.Add(new Quest() { questNo = 10, questName = "가자! 불의 왕국으로", npcType = "", content = "불의 왕국 도착, 스승님의 조수 온 주변으로 이동해 말을 걸어보기", target = "", clearExp =100 });
        quests.Add(new Quest() { questNo = 11, questName = "공포의 땅두대지", npcType = "", content = "불의 왕국 주민 주변으로 이동해 말을 걸어보기", target = "", clearExp =100 });
        quests.Add(new Quest() { questNo = 12, questName = "땅두대지의 습격", npcType = "", content = "땅두대지를 지키는 몬스터 3마리 사냥하기", target = "", clearExp =100 });
        quests.Add(new Quest() { questNo = 13, questName = "불꽃~ 펀치", npcType = "", content = "불의 왕국 마그마그 주변으로 이동해 말을 걸어보기", target = "", clearExp =100 });
        quests.Add(new Quest() { questNo = 14, questName = "땅두대지의 역습", npcType = "", content = "땅두대지를 지키는 몬스터를 5마리 사냥하기", target = "", clearExp =100 });
        quests.Add(new Quest() { questNo = 15, questName = "웰 컴 투 헬", npcType = "", content = "불의 왕국 마그마그 주변으로 이동해 말을 걸어보기", target = "", clearExp =100 });
        quests.Add(new Quest() { questNo = 16, questName = "땅두더지의 분노", npcType = "", content = "땅두대지의 일곱 수호단 7마리 사냥하기", target = "", clearExp =100 });
        quests.Add(new Quest() { questNo = 17, questName = "땅두대지의 최후", npcType = "", content = "땅두대지를 무찔러 불의 왕국을 구하기", target = "", clearExp =100 });
        quests.Add(new Quest() { questNo = 18, questName = "예언된 자", npcType = "", content = "스승님의 조수 온 주변으로 이동해 말을 걸어보기", target = "", clearExp =100 });
    }


    public void NextQuest()
    {
        if (currentQuestIndex < quests.Count - 1)
        {
            IncreasePlayerExp(quests[currentQuestIndex].clearExp);
            NewQuestText.text = "NEW";
            currentQuestIndex++;
            numMonsterKill = 0;
            
            QuestDoneUI.SetActive(true);
            StartCoroutine(DeactivateQuestDoneUIAfterDelay(5.0f));
            MonsterKillUI.SetActive(false);
            if (currentQuestIndex == 8) 
            {
                boss.SetActive(true);
            }
            if (currentQuestIndex == 4 || currentQuestIndex == 6){
                MonsterKillUI.SetActive(true);
                MonsterKillText.text = "Kill: "+numMonsterKill.ToString();
            }
        }
    }

    private IEnumerator DeactivateQuestDoneUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        QuestDoneUI.SetActive(false);
    }

    public void PrevQuest()
    {
        if (currentQuestIndex > 0)
        {
            currentQuestIndex--;
        }
    }

    public int GetCurrentIndex()
    {
        return currentQuestIndex;
    }

    public Quest GetCurrentQuest()
    {
        return quests[currentQuestIndex];
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.K))
        {
            PrevQuest();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            NextQuest();
        }
    }

    public int GetPlayerExp(){
        return playerExp;
    }

    public int GetPlayerLevel(){
        return playerLevel;
    }

    public void IncreasePlayerExp(int addExp){
        playerExp += addExp;
        if (playerExp > playerExpToLevelUp[playerLevel + 1])
        {
            playerExp -= playerExpToLevelUp[playerLevel+ 1];
            IncreasePlayerLevel();
        }
        playerIngameProfileManager.SetExp(playerExp, playerExpToLevelUp[playerLevel + 1]);
    }

    private void IncreasePlayerLevel(){
        playerLevel++;
        playerIngameProfileManager.UpdatePlayerLevelUI(playerLevel);
    }

    public void AddNumMonsterKill(){
        numMonsterKill++;
        MonsterKillText.text = "Kill: "+numMonsterKill.ToString();
        if(currentQuestIndex == 4 && numMonsterKill >= 3){
            NextQuest();
        }
        else if (currentQuestIndex == 6 && numMonsterKill >= 5){
            NextQuest();
        }
        else if (currentQuestIndex == 12 && numMonsterKill >= 3){
            NextQuest();
        }
        else if (currentQuestIndex == 14 && numMonsterKill >= 5){
            NextQuest();
        }
        else if (currentQuestIndex == 16 && numMonsterKill >= 7){
            NextQuest();
        }
    }

    public void AddNumBossMonsterKill(){
        if(currentQuestIndex == 8 || currentQuestIndex == 17){
            NextQuest();
        }
    }
}

