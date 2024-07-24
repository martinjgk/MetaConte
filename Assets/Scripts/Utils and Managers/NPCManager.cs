using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
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
    public Sprite[] NPCImageList; // List of images
    public QuestManager questManager; 

    void Start()
    {
        InitializeInfos();
        NPCDialog.SetActive(false);

        sentences = new Queue<string>();
        
    }

    void InitializeInfos()
    {
        infos.Add(new NPCInfo() { npcNo = 0, npcName = "story", npcType = "story", content = "한반도 깊은 산골 마을 '율전골'에 사는 주인공은 어린 시절부터 네 원소의 왕국에 대한 이야기를 듣고 자랐다.\n 네 원소의 전설은 마을 어른들의 이야기 속에서, 도서관의 낡은 책 속에서, 그리고 마을 광장에 새겨진 고대 비문 속에서 살아 숨 쉬고 있었다.\n천지가 처음 나뉘어지고 세상이 형성될 때, 네 가지 원소가 이 땅을 지배했다.\n 물\n 불\n 흙\n 공기\n 이 네 원소를 지배하는 자가 각각의 왕국을 이루었다.\n 각각의 왕국은 동, 서, 남, 북으로 뻗어가 독자적인 문명과 문화를 발전시켰다.\n각 왕국마다 원소를 사용하기 위한 독특한 마법이 있다. 손과 팔 동작으로 이루어진, 고대 주술과도 같은 성스러운 행동을 하면 원소가 반응을 한다고 알려져 있다..\n하지만\n갈라진 원소처럼,\n이 세상에는 큰 위기가 스며들었다!\n 물, 불, 흙, 공기 왕국에서는 모든 왕국의 모든 동작을 마스터해 세상을 구할 현자를 애타게 기다리고 있다.\n모든 왕국의 마법을 배워 율전골을 구해주세요!\n[신규 퀘스트]\nNPC에게 다가가 말을 걸어보세요! 이동은 키보드로 가능합니다.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 1, npcName = "온 [스승님의 조수]", npcType = "sign", content = "안녕? 이제야 정신이 들었나보구나. 나는 스승님의 도우미 온 이라고 해. 비록 스승님은.. 흑흑\n스승님은 지키지 못했다만, 기절한 너를 끌고 물의 왕국으로 겨우 도망쳤어!\n물의 왕국은 생명의 요람, 때로는 용궁이라고 불리는 곳이야. 먼 과거에는 토끼와 거북이가 물의 왕국을 방문했다고 알려져 있지\n그치만 우리가.. 몬스터와의 전투에서 지는 바람에 세상이 다시 어지럽혀지고 있어. 화마라는 괴물은 물의 왕국에 가뭄을 일으켰지. 물먹는 화마가 물의 왕국에 있는 모든 물을 흡수해버렸지 뭐람..\n물의 왕국을 잠시 둘러보고 와볼래? 이동은 WASD[앞뒤오왼]를 사용해 할 수 있어.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 2, npcName = "온 [스승님의 조수]", npcType = "sign", content = "물의 왕국을 둘러본 소감이 어때?\n그렇구나! 물의 왕국은 물의 언어를 사용해 서로 소통하며, 물의 지혜 통해 세상의 진리를 깨닫곤 했다고 해.\n이제 물의 왕국을 어지럽힌 악당 화마와 몬스터를 무찌르기 위해서 전투에 나설 것이야.\n전투에 나서기 전에 마법을 더 잘 사용할 수 있는 팁을 몇가지 주려고 해!\n가장 먼저 오른쪽에 보이는 창은 스킬창이야. 현재 사용한 마법과 사용 가능한 마법을 볼 수 있어.\n두 번째로 가운데 보이는 것은 마법 지속 시간이야! 마법을 결합하고 싶다면 제한 시간 내에 빠르게 해야해!\n왼쪽 상단에 보이는 것은 플레이어 프로필이야. 현재 레벨과 경험치를 알 수 있지\n마지막으로 왼쪽에 마법 사전과 현재 퀘스트를 볼 수 있어.\n이전에 배웠던 물 마법과 내리다 마법 기억나지? 잘 기억이 안난다면 스킬창과 마법사전을 사용해보도록 해! 그럼 이따 보자!\n[신규 퀘스트] 화마를 찾아서: 화마를 지키는 몬스터 5마리를 사냥하기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 3, npcName = "온 [스승님의 조수]", npcType = "", content = "물의 왕국을 어지럽힌 악당 화마와 몬스터를 무찌르기 위해서 전투에 나서 보자.\n[현재 퀘스트] 화마를 찾아서: 화마를 지키는 몬스터 5마리를 사냥하기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 4, npcName = "온 [스승님의 조수]", npcType = "", content = "정말 잘 했어. 이렇게 전투를 훌륭하게 마칠 줄 알고 있었다니까~\n아까 물의 왕국 주민에게 듣기로는 네가 물의 왕국을 구하러 와준 예언된 존재라던데? 그러면서 물의 왕국에서 사용하는 마법을 하나 가르쳐주더라구.\n그래서 내가 후딱 배워왔찌. 첫 번째 전투를 승리한 네게 흐르다!라는 Level 2 마법을 알려줄게! 따라해봐.\n가장 먼저, 오른손을 펴서 손바닥이 위로 손끝이 왼쪽으로 향하게 해줘\n그러고선 상하로 약간 흔들며 오른쪽으로 이동시키면 끝이야.\n이 마법은 Level 2 마법이라 다른 마법 뒤에 이어서 사용해 새로운 마법을 구현할 수 있어.\n물 마법과 결합해 사용하면 바로 강 마법을 사용할 수 있지! 물이 흐르면 강이니까!\n강 마법을 활용해 다시 전투를 나가볼까? 몬스터가 전투에서 패배했다는 소식을 듣고 화마가 이 쪽으로 선발대를 보내왔어\n[새로운 퀘스트]\n깨어난 화마: 화마의 선발대 몬스터 5마리를 사냥하기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 5, npcName = "온 [스승님의 조수]", npcType = "", content = "화마의 선발대를 무찔러 물의 왕국 마을을 구하자!\n[현재 퀘스트] 화마를 찾아서: 화마의 선발대 몬스터 5마리를 사냥하기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 6, npcName = "온 [스승님의 조수]", npcType = "", content = "우와아아, 네가 화마의 선발대를 이긴 덕분에 마을 주민들이 모두 다치지 않았어!\n이젠 화마와 싸울 때가 된 것 같아. 화마를 무찌르고 물의 왕국을 구해줘!\n[신규 퀘스트]\n비를 내려다오: Boss인 화마를 무찔러 가뭄을 끝내기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 7, npcName = "온 [스승님의 조수]", npcType = "", content = "물의 왕국 주민: 전사님. 화마를 무찔러준 덕분에 우리 물의 왕국에 물이 다시 가득해졌어요!\n고마워요.\n답례로 코인을 드리겠어요.\n온: 이제 물의 왕국을 떠날 시간이야. 물의 왕국 바다 너머에 있는 불의 왕국에서도 큰 위기가 왔다고 하네.\n[신규 퀘스트] 가자! 불의 왕국으로 [물의 왕국 시나리오 종료]", target = "" });
        infos.Add(new NPCInfo() { npcNo = 8, npcName = "온 [스승님의 조수]", npcType = "", content = "어이, 물의 왕국을 구했다고 방심해서는 안될거야! 여기 불의 왕국은 더더욱 위험하다구.\n불의 왕국에 왔으니 불 마법을 배워야겠지? 불 마법은 Level 1 마법으로 기본 원소 마법에 해당해.\n동작을 차근차근 알려줄테니, 천천히 따라해봐~\n가장 먼저, 두 손을 펴서 손등이 밖으로 보이게 해줘.\n다음, 손끝이 위로 향하게 세워줘.\n마지막으로 손가락을 가볍게 흔들면서 상하로 엇갈리게 움직여줘!\n불길이 솟구쳐 오르는 것을 나타내는 동작을 떠올리면 좋아! 어렵지 않지?\n이 마법은 물의 마법처럼, 불의 마법과도 같이 사용할 수 있어. 이전에 배운 비 마법 기억나니? \n물 마법을 사용하고 내려라! 마법을 이어서 사용하면 비 마법이었지.\n마찬가지로 불 마법을 사용하고 내려라 마법을 사용하면 불꽃세례라는 마법을 사용할 수 있어!\n불의 왕국은 땅두대지라는 괴물이 나타나 위험에 빠졌어.\n땅두대지가 화산에 있는 마그마를 모두 삼켜서 불의 왕국 마을 주민들이 힘을 못쓰고 있어.\n땅두더지를 지키는 세 마리 몬스터를 방금 배운 불 마법을 이용해 먼저 제압해줘!\n[신규 퀘스트]\n땅두대지의 습격: 땅두대지를 지키는 몬스터 3마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 9, npcName = "온 [스승님의 조수]", npcType = "", content = "땅두대지를 지키는 몬스터를 무찔러 불의 왕국 마을을 구하자!\n[현재 퀘스트] 땅두대지의 습격: 땅두대지를 지키는 몬스터 3마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 10, npcName = "온 [스승님의 조수]", npcType = "", content = "땅두대지를 지키는 다섯 마리의 몬스터를 무찔러 주어서 고마워! 전투서 승리했으니! 새로운 마법을 알려줄려고 해.\n어떤 마법일지 기대가 되나본데?\n이번에는 Level 2 마법인 ‘펀치’라는 강력한 마법을 알려줄게! 따라해봐.\n가장 먼저, 오른손을 주먹지고 검지만 펴줘.\n손가락의 끝을 왼 주먹에 대어주면 끝이야!\n주먹을 가리킨다고 생각하면 좋아!\n이 마법은 Level 2 마법이라 다른 마법과 결합해 사용할 수 있어.\n아까 배운 불 마법을 사용하고 펀치! 마법을 이어서 사용하면 새로운 마법을 사용할 수 있어. 이름을 예상해보겠니?\n바로 불주먹 마법이지~ 물하고 사용하면 아쿠아펀치 마법을 사용할 수도 있어.\n배운 마법을 한 번 사용해볼까?\n[효과음] 쿠오오오오오오오\n땅두대지를 지키는 다섯 마리의 몬스터가 다시 돌아와 불의 왕국을 어지럽히고 있어!\n몬스터를 다시 제압해줘!\n[신규 퀘스트]\n땅두대지의 역습: 땅두대지를 지키는 몬스터를 5마리 사냥하기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 11, npcName = "온 [스승님의 조수]", npcType = "", content = "쿠오오오오오오오\n땅두대지를 지키는 다섯 마리의 몬스터가 다시 돌아와 불의 왕국을 어지럽히고 있어!\n몬스터를 다시 제압해줘! 부탁할게!\n[현재 퀘스트]\n땅두대지의 역습: 땅두대지를 지키는 몬스터를 5마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 12, npcName = "온 [스승님의 조수]", npcType = "", content = "[효과음] 화아아아아아악!\n땅두대지를 지키는 다섯 마리의 몬스터를 무찔러 주어서 고마워! 불의 왕국 마을 주민들이 보상으로 (아이템)을 주었어.\n내가 더 강한 마법을 알려줄려고 해!\n 바로 Level2 마법, 지옥 이라는 마법을 알려줄게! 따라해봐.\n두 주먹의 검지를 펴서 바닥이 밖으로 향하게 세워줘.\n턱 양옆에 댔다가 머리 양쪽에 올려 대어 볼까?\n오른 주먹의 1지를 펴서 끝이 아래로 향하게 하여 내려줘. 끝이야!\n이 마법은 아주 특별해서, 다른 마법과 결합해 사용할 수 있어. 불 마법을 사용하고 지옥 마법을 이어서 사용하면 새로운 마법을 사용할 수 있어.\n바로 불지옥 마법이지~ 물하고 사용하면 물지옥 마법을 사용할 수도 있어.\n이 마법은 지금까지 배웠던 어떤 마법들보다 매우 강력할 것이야. 배운 마법을 전투에서 한 번 사용해봐~\n[효과음]쿠오오오오오오오\n앗! 이번에는 땅두대지의 일곱 수호단이 불의 왕국을 어지럽히고 있어! 일곱 수호단은 이전 몬스터보다도 더 강할 것이야\n땅두대지의 일곱 수호단을 제압해줘!\n[신규 퀘스트]\n 땅두대지의 역습: 땅두대지의 일곱 수호단 7마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 13, npcName = "온 [스승님의 조수]", npcType = "", content = "일곱 수호단은 이전 몬스터보다도 더 강할 것이야, 땅두대지의 일곱 수호단을 제압해줘!\n[현재 퀘스트]\n 땅두대지의 역습: 땅두대지의 일곱 수호단 7마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 14, npcName = "온 [스승님의 조수]", npcType = "", content = "이제는 땅두대지를 무찌를 정도로 충분히 강해졌지?\n땅두대지를 무찔러 불의 왕국을 구해줘!\n만약 이전까지 배운 마법이 기억나지 않는다면, 마법 사전을 참고해 다시 배울 수 있어!\n[신규 퀘스트]\n땅두대지의 최후: 땅두대지를 무찔러 불의 왕국을 구하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 15, npcName = "온 [스승님의 조수]", npcType = "", content = "[효과음]: 빠바바바바바밤\n땅두대지를 무찔러 불의 왕국을 구했구나! 정말 대단한걸?\n끝.", target = "" });
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
    }

    private void EndDialog()
    {
        if(questManager.GetCurrentIndex() == 1 || questManager.GetCurrentIndex() == 2 || questManager.GetCurrentIndex() == 4 || questManager.GetCurrentIndex() == 7 || 
        questManager.GetCurrentIndex() == 8 ||  questManager.GetCurrentIndex() == 10 || questManager.GetCurrentIndex() == 12)
        {
            questManager.NextQuest();
        }
        NPCChatExit();
    }
}
