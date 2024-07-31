using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldNPCManager : MonoBehaviour
{
    [System.Serializable]
    public class NPCInfo
    {
        public int npcNo;
        public string npcName;
        public string npcType;
        public string content;
        public string target;
    }
    [SerializeField]
    private GameObject NPCDialog;
    [SerializeField]
    private Text NPCText_Discription;
    [SerializeField]
    private Text NPCText_name;
    private Queue<string> sentences;
    private bool isDialogActive = false;
    
    public List<NPCInfo> infos = new List<NPCInfo>();
    public QuestManager questManager; 

    [SerializeField]
    private GameObject StatUI;
    void Start()
    {
        InitializeInfos();
        NPCDialog.SetActive(false);

        sentences = new Queue<string>();
        
    }

    void InitializeInfos()
    {
        infos.Add(new NPCInfo() { npcNo = 0, npcName = "물의 왕국 쿠아쿠아", npcType = "story", content = "한반도 깊은 산골 마을 '율전골'에 사는 주인공은 어린 시절부터 네 원소의 왕국에 대한 이야기를 듣고 자랐다.\n 네 원소의 전설은 마을 어른들의 이야기 속에서, 도서관의 낡은 책 속에서, 그리고 마을 광장에 새겨진 고대 비문 속에서 살아 숨 쉬고 있었다.\n천지가 처음 나뉘어지고 세상이 형성될 때, 네 가지 원소가 이 땅을 지배했다.\n 물\n 불\n 흙\n 공기\n 이 네 원소를 지배하는 자가 각각의 왕국을 이루었다.\n 각각의 왕국은 동, 서, 남, 북으로 뻗어가 독자적인 문명과 문화를 발전시켰다.\n각 왕국마다 원소를 사용하기 위한 독특한 마법이 있다. 손과 팔 동작으로 이루어진, 고대 주술과도 같은 성스러운 행동을 하면 원소가 반응을 한다고 알려져 있다..\n하지만\n갈라진 원소처럼,\n이 세상에는 큰 위기가 스며들었다!\n 물, 불, 흙, 공기 왕국에서는 모든 왕국의 모든 동작을 마스터해 세상을 구할 현자를 애타게 기다리고 있다.\n모든 왕국의 마법을 배워 율전골을 구해주세요!\n[신규 퀘스트]\nNPC에게 다가가 말을 걸어보세요! 이동은 키보드로 가능합니다.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 1, npcName = "물의 왕국 ???", npcType = "sign", content = "앗! 아직 여기에 오면 위험해! 스승님의 조수 온에게 말을 걸어봐.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 2, npcName = "물의 왕국 쿠아쿠아", npcType = "sign", content = "어머! 무사했구나! 먼 바다에서 표류하고 있길래 얼릉 도와주었지~ 걱정 많이 했는데.. 앗! 내 소개가 늦었네. 안녕? 나는 물의 왕국 주민 쿠아쿠아라고 해.\n음.. 사실 네 스승님이었던 그랜드 수프림께서 몬스터와의 전투에서 지는 바람에 세상이 다시 어지럽혀지고 있어…유감이야..\n화마라는 괴물은 물의 왕국에 가뭄을 일으켰지. 물먹는 화마가 물의 왕국에 있는 모든 물을 흡수해버렸지 뭐람..혹시…우주 최고 마법사의 제자였던 네가 도와줄….\n어? 온 이라고 했던가? 저기서 네 동료가 부르는 것 같아. 어서 가봐야겠는데?\n[신규 퀘스트] 멀티콤보크래프트: 스승님의 조수 온에게 다가가 말을 걸기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 3, npcName = "물의 왕국 쿠아쿠아", npcType = "", content = "어? 온 이라고 했던가? 저기서 네 동료가 부르는 것 같아. 어서 가봐야겠는데?\n[현재 퀘스트] 멀티콤보크래프트: 스승님의 조수 온에게 다가가 말을 걸기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 4, npcName = "물의 왕국 쿠아쿠아", npcType = "", content = "물의 왕국을 어지럽힌 악당 화마와 몬스터를 무찔러 주겠다구?\n[현재 퀘스트] 화마를 찾아서: 화마를 지키는 몬스터 3마리를 사냥하기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 5, npcName = "물의 왕국 쿠아쿠아", npcType = "", content = "정말 잘 했어. 우리를 몬스터로부터 구해주다니~ 그렇다면 네가 물의 왕국을 구하러 와준 예언된 존재일 수도?\n물을 잘 다룰 수 있도록 물의 왕국에서 사용하는 마법을 하나 가르쳐줄게~ 첫 번째 전투를 승리한 네게 흐르다!라는 Level 2 마법을 알려줄게! 따라해봐.\n가장 먼저, 오른손을 펴서 손바닥이 위로 손끝이 왼쪽으로 향하게 해줘\n그러고선 상하로 약간 흔들며 오른쪽으로 이동시키면 끝이야.\n이 마법은 Level 2 마법이라 다른 마법 뒤에 이어서 사용해 새로운 마법을 구현할 수 있어. 물 마법과 결합해 사용하면 바로 강 마법을 사용할 수 있지! 물이 흐르면 강이니까!\n강 마법을 활용해 다시 전투를 나가볼까? 몬스터가 전투에서 패배했다는 소식을 듣고 화마가 이 쪽으로 선발대를 보내왔어\n[새로운 퀘스트]\n깨어난 화마: 화마의 선발대 몬스터 5마리를 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 6, npcName = "물의 왕국 쿠아쿠아", npcType = "", content = "화마의 선발대를 무찔러 물의 왕국 마을을 구해줘!\n[현재 퀘스트] 화마를 찾아서: 화마의 선발대 몬스터 5마리를 사냥하기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 7, npcName = "물의 왕국 쿠아쿠아", npcType = "", content = "우와아아, 네가 화마의 선발대를 이긴 덕분에 우리 모두가 무사할 수 있었어. 스승님의 조수가 너를 부르는데?", target = "" });
        infos.Add(new NPCInfo() { npcNo = 8, npcName = "물의 왕국 쿠아쿠아", npcType = "", content = "화마와 전투를 하러 간다구? 우리 왕국을 구해주는 것은 고맙지만, 꼭 살아 돌아와야해!", target = "" });
        infos.Add(new NPCInfo() { npcNo = 9, npcName = "물의 왕국 쿠아쿠아", npcType = "", content = "전사님. 화마를 무찔러준 덕분에 우리 물의 왕국에 물이 다시 가득해졌어요! 고마워요!", target = "" });
        infos.Add(new NPCInfo() { npcNo = 10, npcName = "불의 왕국 ???", npcType = "", content = "앗! 아직 여기에 오면 위험해! 스승님의 조수 온에게 말을 걸어봐.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 11, npcName = "불의 왕국 마그마그", npcType = "", content = "[쿠오오오오오오: 어디선가 괴성이 들려온다…] 안녕~~ 나는 불의 왕국 주민 마그마그라고 해. 불의 왕국은 땅두대지라는 괴물이 나타나 위험에 빠졌어.\n땅두대지가 화산에 있는 마그마를 모두 삼켜서 불의 왕국 마을 주민들이 힘을 못쓰고 있어. 땅두더지를 지키는 세 마리 몬스터를 방금 배운 불 마법을 이용해 먼저 제압해줘!\n[신규 퀘스트] 땅두대지의 습격: 땅두대지를 지키는 몬스터 3마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 12, npcName = "불의 왕국 마그마그", npcType = "", content = "[쿠오오오오오오: 어디선가 괴성이 들려온다…] 땅두대지를 지키는 몬스터를 무찔러 불의 왕국 마을을 구해줘!\n[현재 퀘스트] 땅두대지의 습격: 땅두대지를 지키는 몬스터 3마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 13, npcName = "불의 왕국 마그마그", npcType = "", content = "땅두대지를 지키는 다섯 마리의 몬스터를 무찔러 주어서 고마워! 전투서 승리했으니! 새로운 마법을 알려줄려고 해. 어떤 마법일지 기대가 되나본데?\nLevel 2 마법인 ‘펀치’라는 강력한 마법을 알려줄게! 따라해봐. 가장 먼저, 오른손을 주먹지고 검지만 펴줘. 손가락의 끝을 왼 주먹에 대어주면 끝이야!\n주먹을 가리킨다고 생각하면 좋아! 이 마법은 Level 2 마법이라 다른 마법과 결합해 사용할 수 있어. 예를 들어, 불 마법과 같이 사용하면 불주먹 마법을 사용할 수 있어.\n[효과음] 쿠오오오오오오오. 땅두대지를 지키는 다섯 마리의 몬스터가 다시 돌아와 불의 왕국을 어지럽히고 있어! 몬스터를 다시 제압해줘!\n[신규 퀘스트]\n땅두대지의 역습: 땅두대지를 지키는 몬스터를 5마리 사냥하기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 14, npcName = "불의 왕국 마그마그", npcType = "", content = "[쿠오오오오오오: 가까이서 괴성이 들려온다…] 땅두대지를 지키는 다섯 마리의 몬스터가 다시 돌아와 불의 왕국을 어지럽히고 있어! 몬스터를 다시 제압해줘! 부탁할게!\n[현재 퀘스트]\n땅두대지의 역습: 땅두대지를 지키는 몬스터를 5마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 15, npcName = "불의 왕국 마그마그", npcType = "", content = "땅두대지를 지키는 다섯 마리의 몬스터를 무찔러 주어서 고마워! 보상으로 내가 더 강한 마법을 알려줄려고 해! 바로 Level2 마법, '지옥'이라는 마법을 알려줄게! 따라해봐.\n두 주먹의 검지를 펴서 바닥이 밖으로 향하게 세워줘. 바로 이어서 턱 양옆에 댔다가 머리 양쪽에 올려 대어 볼까?\n오른 주먹의 1지를 펴서 끝이 아래로 향하게 하여 내려줘. 끝이야! 이 마법은 아주 특별해서, Level 1 마법과 결합해 사용할 수 있어. 이 마법은 지금까지 배웠던 어떤 마법들보다 매우 강력할 것이야. 배운 마법을 전투에서 한 번 사용해봐~\n[쿠오오오오오오: 아주 가까이서 괴성이 들려온다…] 앗! 이번에는 땅두대지의 일곱 수호단이 불의 왕국을 어지럽히고 있어! 일곱 수호단은 이전 몬스터보다도 더 강할 것이야. 땅두대지의 일곱 수호단을 제압해줘!\n[신규 퀘스트]\n 땅두대지의 분노: 땅두대지의 일곱 수호단 7마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 16, npcName = "불의 왕국 마그마그", npcType = "", content = "일곱 수호단은 이전 몬스터보다도 더 강할 것이야, 땅두대지의 일곱 수호단을 제압해줘!\n[현재 퀘스트] 땅두대지의 분노: 땅두대지의 일곱 수호단 7마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 17, npcName = "불의 왕국 마그마그", npcType = "", content = "이제는 땅두대지를 무찌를 정도로 충분히 강해졌지? 땅두대지를 무찔러 불의 왕국을 구해줘!\n[신규 퀘스트] 땅두대지의 최후: 땅두대지를 무찔러 불의 왕국을 구하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 18, npcName = "불의 왕국 마그마그", npcType = "", content = "전사님. 땅두대지를 무찔러준 덕분에 우리 불의 왕국에 불이 다시 가득해졌어요! 고마워요!", target = "" });
    }
    public void NPCChatEnter(string textDiscription, string textName, Sprite imageSprite)
    {
        NPCText_Discription.text = "";
        NPCText_name.text = textName;
        NPCDialog.SetActive(true);

        sentences.Clear();
        string[] lines = textDiscription.Split('\n');
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
        isDialogActive = true;
    }

    public void NPCChatEnter()
    {
        StatUI.SetActive(false);
        if(questManager.GetCurrentIndex() == 0)
        {
            questManager.NextQuest();
        }
        NPCText_Discription.text = "";
        Debug.Log(questManager.GetCurrentIndex());
        Debug.Log(infos[0].npcName);
        NPCText_name.text = infos[questManager.GetCurrentIndex()].npcName;
        string textDiscription = infos[questManager.GetCurrentIndex()].content;
        NPCDialog.SetActive(true);

        sentences.Clear();
        string[] lines = textDiscription.Split('\n');
        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }

        DisplayNextSentence();
        isDialogActive = true;
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
        NPCText_Discription.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            NPCText_Discription.text += letter;
            yield return null;
        }
    }

    public void NPCChatExit()
    {
        NPCText_Discription.text = "";
        NPCText_name.text = "";
        NPCDialog.SetActive(false);
        isDialogActive = false;
        StatUI.SetActive(true);
    }

    private void EndDialog()
    {
        if(questManager.GetCurrentIndex() == 2 || questManager.GetCurrentIndex() == 5 || questManager.GetCurrentIndex() == 11 || 
        questManager.GetCurrentIndex() == 13 ||  questManager.GetCurrentIndex() == 15 || questManager.GetCurrentIndex() == 11)
        {
            questManager.NextQuest();
        }
        NPCChatExit();
    }
}
