using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour
{
    [System.Serializable]
    public class Quest
    {
        public int questNo;
        public string questName;
        public string npcType;
        public string content;
        public string target;
    }

    public Text questNumberText;
    public List<Quest> quests = new List<Quest>();
    private int currentQuestIndex = 0;
    

    void Start()
    {
        InitializeQuests();
        UpdateQuestUI();
    }

    void InitializeQuests()
    {
        quests.Add(new Quest() { questNo = 0, questName = "새로운 시작", npcType = "story", content = "한반도 깊은 산골 마을 '율전골'에 사는 주인공은 어린 시절부터 네 원소의 왕국에 대한 이야기를 듣고 자랐다.\n 네 원소의 전설은 마을 어른들의 이야기 속에서, 도서관의 낡은 책 속에서, 그리고 마을 광장에 새겨진 고대 비문 속에서 살아 숨 쉬고 있었다.\n천지가 처음 나뉘어지고 세상이 형성될 때, 네 가지 원소가 이 땅을 지배했다.\n 물\n 불\n 흙\n 공기\n 이 네 원소를 지배하는 자가 각각의 왕국을 이루었다.\n 각각의 왕국은 동, 서, 남, 북으로 뻗어가 독자적인 문명과 문화를 발전시켰다.\n각 왕국마다 원소를 사용하기 위한 독특한 마법이 있다. 고대 주술과도 같은 성스러운 동작을 하면 원소가 반응을 한다고 알려져 있다..\n평화의 시대도 잠시,, 갈라진 원소처럼, 이 세상에는 큰 위기가 스며들었다!\n 물, 불, 흙, 공기 왕국에서는 모든 왕국의 모든 동작을 마스터해 세상을 구할 현자를 애타게 기다리고 있다.\n모든 왕국의 마법을 배워 율전골을 구해주세요!\n[신규 퀘스트]\nNPC 쿠아쿠아에게 다가가 말을 걸어보세요! 이동은 키보드로 가능합니다.", target = "" });
        quests.Add(new Quest() { questNo = 1, questName = "보이지 않는 위험", npcType = "sign", content = "지나가는 행인에게 말을 걸기", target = "" });
        quests.Add(new Quest() { questNo = 2, questName = "아쿠아 매직", npcType = "sign", content = "지나가는 행인에게 말을 걸어 물 마법을 배우기", target = "" });
        quests.Add(new Quest() { questNo = 3, questName = "가자! 물의 왕국으로.", npcType = "", content = "방금 배운 물 마법을 텔레포트에서 사용해 물의 왕국으로 이동하기", target = "" });
        quests.Add(new Quest() { questNo = 4, questName = "아쿠아쿠아", npcType = "", content = "깊은 바다 속에 자리한 물의 왕국은 생명의 요람, 때로는 용궁이라 불린다.\n 물의 왕국 주민들은 물을 다스리는 마법을 익혔다.\n 물의 언어를 사용하여 서로 소통하며, 물의 지혜 통해 세상의 진리를 깨닫곤 했다.\n 먼 과거에는 토끼와 거북이가 물의 왕국을 방문했다고 알려져 있다. 하지만 어떤 시점 이후부터 물의 왕국은 육지와의 교류를 완전히 끊고 자취를 감추었다.\n 물의 언어를 배우기 위해 NPC 아쿠아쿠아에게 말을 걸어 보세요!", target = "" });
        quests.Add(new Quest() { questNo = 5, questName = "화마를 찾아서", npcType = "", content = "화마를 지키는 몬스터 5마리를 사냥하기", target = "" });
        quests.Add(new Quest() { questNo = 6, questName = "비를 내려다오", npcType = "", content = "Boss 화마를 무찔러 가뭄을 끝내기", target = "" });
        quests.Add(new Quest() { questNo = 7, questName = "첫 번째 여정을 마치며", npcType = "", content = "npc에게 말을 걸어, 불 마법 배우기", target = "" });
        quests.Add(new Quest() { questNo = 8, questName = "가자! 불의 왕국으로", npcType = "", content = "텔레포트에서 불의 마법을 사용해 불의 왕국으로 이동하기", target = "" });
        quests.Add(new Quest() { questNo = 9, questName = "불이 내려", npcType = "", content = "마그마그에게 말을 걸어보기", target = "" });
        quests.Add(new Quest() { questNo = 10, questName = "땅두대지의 습격", npcType = "", content = "땅두대지를 지키는 몬스터 5마리 사냥하기", target = "" });
        quests.Add(new Quest() { questNo = 11, questName = "불꽃~ 펀치", npcType = "", content = "마그마그에게 말을 걸어 펀치 마법을 배우기", target = "" });
        quests.Add(new Quest() { questNo = 12, questName = "땅두대지의 역습", npcType = "", content = "땅두대지를 지키는 몬스터를 5마리 사냥하기", target = "" });
        quests.Add(new Quest() { questNo = 13, questName = "웰컴 투 헬", npcType = "", content = "마그마그에게 말을 걸어 지옥 마법을 배우기", target = "" });
        quests.Add(new Quest() { questNo = 14, questName = "땅두더지의 분노", npcType = "", content = "땅두대지의 다섯 수호단 5마리 사냥하기", target = "" });
        quests.Add(new Quest() { questNo = 15, questName = "예언된 자", npcType = "", content = "땅두대지를 무찔러 불의 왕국을 구하기", target = "" });
    }

    public void UpdateQuestUI()
    {
        questNumberText.text = quests[currentQuestIndex].questNo + " / " + quests.Count;
    }

    public void NextQuest()
    {
        if (currentQuestIndex < quests.Count - 1)
        {
            currentQuestIndex++;
            UpdateQuestUI();
        }
    }

    public void PreviousQuest()
    {
        if (currentQuestIndex > 0)
        {
            currentQuestIndex--;
            UpdateQuestUI();
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
        if(Input.GetKeyDown(KeyCode.O))
        {
            PreviousQuest();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            NextQuest();
        }
    }
}

