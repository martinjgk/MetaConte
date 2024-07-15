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
    private GameObject NPCDialog;
    private Text NPCText_Discription;
    private Text NPCText_name;
    private Image sl_image;
        

    private Queue<string> sentences;
    private bool isDialogActive = false;
    
    public List<NPCInfo> infos = new List<NPCInfo>();
    public Sprite[] NPCImageList; // List of images
    public QuestManager questManager; 

    void Start()
    {
        InitializeInfos();
        NPCDialog = GameObject.Find("NPCDialog");
        NPCText_Discription = GameObject.Find("NPCText_Discription").GetComponent<Text>();
        NPCText_name = GameObject.Find("NPCText_name").GetComponent<Text>();
        sl_image = GameObject.Find("NPC_img").GetComponent<Image>();
        NPCDialog.SetActive(false);

        sentences = new Queue<string>();
        
    }

    void InitializeInfos()
    {
        infos.Add(new NPCInfo() { npcNo = 0, npcName = "story", npcType = "story", content = "한반도 깊은 산골 마을 '율전골'에 사는 주인공은 어린 시절부터 네 원소의 왕국에 대한 이야기를 듣고 자랐다.\n 네 원소의 전설은 마을 어른들의 이야기 속에서, 도서관의 낡은 책 속에서, 그리고 마을 광장에 새겨진 고대 비문 속에서 살아 숨 쉬고 있었다.\n천지가 처음 나뉘어지고 세상이 형성될 때, 네 가지 원소가 이 땅을 지배했다.\n 물\n 불\n 흙\n 공기\n 이 네 원소를 지배하는 자가 각각의 왕국을 이루었다.\n 각각의 왕국은 동, 서, 남, 북으로 뻗어가 독자적인 문명과 문화를 발전시켰다.\n각 왕국마다 원소를 사용하기 위한 독특한 마법이 있다. 손과 팔 동작으로 이루어진, 고대 주술과도 같은 성스러운 행동을 하면 원소가 반응을 한다고 알려져 있다..\n하지만\n갈라진 원소처럼,\n이 세상에는 큰 위기가 스며들었다!\n 물, 불, 흙, 공기 왕국에서는 모든 왕국의 모든 동작을 마스터해 세상을 구할 현자를 애타게 기다리고 있다.\n모든 왕국의 마법을 배워 율전골을 구해주세요!\n[신규 퀘스트]\nNPC에게 다가가 말을 걸어보세요! 이동은 키보드로 가능합니다.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 1, npcName = "지나가는 행인", npcType = "sign", content = "어서오시게!\n 나는 지나가는 행인이오.\n 자네 혹시 물의 왕국으로 가는 길인가?\n허허~ 맞구만,, 물의 왕국이라..아주 오래전에 가는 길이 막혔다고 들었다만..\n그래도 가는 방법은 잘 알고 있지. 할아버지의 할아버지로부터 전해 내려오던 전설일세.\n 물의 왕국으로 가기 위해서는 물 마법을 배워야하네.\n오직 선택받은 자만이 텔레포트에서 물 마법을 사용하면 물의 왕국을 갈 수 있을 것일세.\n물이라는 마법을 알려줄테니 잘 따라해보도록 할세. 자!\n가장 먼저, 오른 주먹의 1·5지를 펴보시게\n그 다음 구부려 입으로 약간 기울여 올리면 완성이야.\n컵을 들고 물을 마시는 동작을 떠올리면 좋다.\n텔레포트에 가서 물 마법을 사용해보시게.\n혹시 모르지, 자네가 선택받은 자일 수도 있지 않은가?", target = "" });
        infos.Add(new NPCInfo() { npcNo = 2, npcName = "지나가는 행인", npcType = "sign", content = "어서오시게!\n 나는 지나가는 행인이오.\n 자네 혹시 물의 왕국으로 가는 길인가?\n허허~ 맞구만,, 물의 왕국이라..아주 오래전에 가는 길이 막혔다고 들었다만..\n그래도 가는 방법은 잘 알고 있지. 할아버지의 할아버지로부터 전해 내려오던 전설일세.\n 물의 왕국으로 가기 위해서는 물 마법을 배워야하네.\n오직 선택받은 자만이 텔레포트에서 물 마법을 사용하면 물의 왕국을 갈 수 있을 것일세.\n물이라는 마법을 알려줄테니 잘 따라해보도록 할세. 자!\n가장 먼저, 오른 주먹의 1·5지를 펴보시게\n그 다음 구부려 입으로 약간 기울여 올리면 완성이야.\n컵을 들고 물을 마시는 동작을 떠올리면 좋다.\n텔레포트에 가서 물 마법을 사용해보시게.\n혹시 모르지, 자네가 선택받은 자일 수도 있지 않은가?\n[신규 퀘스트] 가자! 물의 왕국으로.\n방금 배운 물 마법을 텔레포트에서 사용해 물의 왕국으로 이동해보세요!", target = "" });
        infos.Add(new NPCInfo() { npcNo = 3, npcName = "지나가는 행인", npcType = "", content = "이런! 금새 물의 마법을 까먹은 것인가? 물의 왕국으로 가기 위해서는 물 마법을 배워야하네.\n오직 선택받은 자만이 텔레포트에서 물 마법을 사용하면 물의 왕국을 갈 수 있을 것일세.\n물이라는 마법을 다시 알려줄테니 잊지 말도록!\n가장 먼저, 오른 주먹의 1·5지를 펴보시게\n그 다음 구부려 입으로 약간 기울여 올리면 완성이야.\n컵을 들고 물을 마시는 동작을 떠올리면 좋다.\n텔레포트에 가서 다시 물 마법을 사용해보시게.\n혹시 모르지, 자네가 선택받은 자일 수도 있지 않은가?", target = "" });
        infos.Add(new NPCInfo() { npcNo = 4, npcName = "쿠아쿠아", npcType = "", content = "안녕? 나는 물의 왕국에서 귀요미를 담당하고 있는 쿠아쿠아야.\n듣기로는 우리 물의 왕국을 구하러 와준 예언된 존재라던데? 그럼 우리 물의 왕국 마법을 잘 알고 있어?\n뭐야! 잘 모르겠다구?\n이것 참 큰일이네.\n그러면 내려라!라는 마법을 알려줄게! 따라해봐.\n가장 먼저, 손끝이 아래로 손등이 밖으로 향하게 해줘.\n그 다음 두 손을 상하로 두 번 움직이면 마법 끝이야!\n이 마법은 아주 특별해서, 다른 마법과 같이 사용할 수 있어.\n이전에 배운 물 마법 기억나니? [Y / N]\n[Y] 물 마법을 사용하고 내려라! 마법을 이어서 사용하면 새로운 마법을 사용할 수 있어.\n바로 비 마법이지! 물이 내리면 비 이니까!\n비 마법을 한 번 사용해볼까?\n우리 물의 왕국은 화마라는 괴물이 나타나 위험에 빠졌어.\n물먹는 화마가 물의 왕국에 있는 모든 물을 흡수해버렸지 뭐람..\n[신규 퀘스트] 화마를 찾아서\n화마를 지키는 다섯 마리의 몬스터를 먼저 제압해줘! 부탁할게!", target = "" });
        infos.Add(new NPCInfo() { npcNo = 5, npcName = "쿠아쿠아", npcType = "", content = "우리 물의 왕국은 화마라는 괴물이 나타나 위험에 빠졌어.\n화마를 지키는 다섯 마리의 몬스터를 먼저 제압해줘! 부탁할게\n아? 내리다 마법이 잘 기억이 안난다고?\n내려라!라는 마법을 다시 알려줄게! 잘 따라해봐.\n가장 먼저, 손끝이 아래로 손등이 밖으로 향하게 해줘.\n그 다음 두 손을 상하로 두 번 움직이면 마법 끝이야!\n이 마법은 아주 특별해서, 다른 마법과 같이 사용할 수 있어.\n이전에 배운 물 마법 기억나니? [Y / N]\n[Y] 물 마법을 사용하고 내려라! 마법을 이어서 사용하면 새로운 마법을 사용할 수 있어.\n바로 비 마법이지! 물이 내리면 비 이니까!\n비 마법을 한 번 사용해볼까?\n이전에 배운 마법이 잘 기억나지 않는다면 마법 사전을 사용할 수도 있어!", target = "" });
        infos.Add(new NPCInfo() { npcNo = 6, npcName = "쿠아쿠아", npcType = "", content = "몬스터 다섯마리를 무찔렀구나! 정말 고마워:)\n이제는 화마를 무찔러줘! 부탁할게\n아? 내리다 마법이 잘 기억이 안난다고?\n내려라!라는 마법을 다시 알려줄게! 잘 따라해봐.\n가장 먼저, 손끝이 아래로 손등이 밖으로 향하게 해줘.\n그 다음 두 손을 상하로 두 번 움직이면 마법 끝이야!\n이 마법은 아주 특별해서, 다른 마법과 같이 사용할 수 있어.\n이전에 배운 물 마법 기억나니? [Y / N]\n[Y] 물 마법을 사용하고 내려라! 마법을 이어서 사용하면 새로운 마법을 사용할 수 있어.\n바로 비 마법이지! 물이 내리면 비 이니까!\n비 마법을 한 번 사용해볼까?", target = "" });
        infos.Add(new NPCInfo() { npcNo = 7, npcName = "쿠아쿠아", npcType = "", content = "전사님. 화마를 무찔러준 덕분에 우리 물의 왕국에 물이 다시 가득해졌어요!\n고마워요.\n답례로 코인을 드리겠어요.\n이제 물의 왕국을 떠날 시간이에요.\n마침 불의 왕국에서 전사님을 애타게 찾고 있는 것 같아요.\n불이라는 마법을 알려줄게요! 따라해봐요.\n가장 먼저, 두 손을 펴서 손등이 밖으로 보이게 해줘요.\n다음, 손끝이 위로 향하게 세워줘요.\n마지막으로 손가락을 가볍게 흔들면서 상하로 엇갈리게 움직여줘요!\n불길이 솟구쳐 오르는 것을 나타내는 동작을 떠올리면 좋아요.\n어렵지 않지요?\n[신규 퀘스트] 가자! 불의 왕국으로\n텔레포트에서 불의 마법을 사용해 불의 왕국으로 이동하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 8, npcName = "쿠아쿠아", npcType = "", content = "이제 물의 왕국을 떠날 시간이에요. 마침 불의 왕국에서 전사님을 애타게 찾고 있는 것 같아요.\n 앞으로의 여정을 응원할게요!\n텔레포트에서 불의 마법을 사용해 불의 왕국으로 이동할 수 있어요~", target = "" });
        infos.Add(new NPCInfo() { npcNo = 9, npcName = "마그마그", npcType = "", content = "안녕!!! 나는 불의 왕국에서 한 성격하는 마그마그야.\n화아아아아악\n듣기로는 물의 왕국을 가뭄에서 구한, 예언된 존재라던데? 그럼 우리 불의 왕국 마법도 잘 알고 있어?\n뭐야! 잘 모르겠다구?\n이것 참 큰일이네.\n그러면 내려라!라는 마법을 사용해 불의 마법을 알려줄게! 따라해봐.\n가장 먼저, 손끝이 아래로 손등이 밖으로 향하게 해줘.\n그 다음 두 손을 상하로 두 번 움직이면 내리다라는 마법 끝이야!\n이 마법은 물의 마법처럼, 불의 마법과도 같이 사용할 수 있어.\n이전에 배운 비 마법 기억나니? \n물 마법을 사용하고 내려라! 마법을 이어서 사용하면 비 마법이었지.\n마찬가지로 불 마법을 사용하고 내려라 마법을 사용하면 불꽃세례라는 마법을 사용할 수 있어!\n불꽃세례 마법을 한 번 사용해볼까?\n우리 불의 왕국은 땅두대지라는 괴물이 나타나 위험에 빠졌어.\n땅두대지가 우리 화산에 있는 마그마를 모두 삼켜서 우리 마을 주민들이 힘이 없걸랑..\n[새로운 퀘스트]\n땅두더지를 지키는 다섯 마리의 몬스터를 먼저 제압해줘! 부탁할게!", target = "" });
        infos.Add(new NPCInfo() { npcNo = 10, npcName = "마그마그", npcType = "", content = "화아아아아악\n땅두더지를 지키는 다섯 마리의 몬스터를 먼저 제압해줘! 부탁할게!\n뭐야! 불꽃세례를 잘 모르겠다구?\n내려라는 가장 먼저, 손끝이 아래로 손등이 밖으로 향하게 해줘.\n그 다음 두 손을 상하로 두 번 움직이면 마법 끝이야!\n불 마법을 사용하고 내려라 마법을 사용하면 불꽃세례라는 마법을 사용할 수 있어!\n불꽃세례 마법을 한 번 사용해볼까?\n우리 불의 왕국은 땅두더지라는 괴물이 나타나 위험에 빠졌어.\n땅두더지가 우리 화산에 있는 마그마를 모두 삼켜서 우리 마을 주민들이 힘이 없걸랑..\n[새로운 퀘스트]\n화마를 지키는 다섯 마리의 몬스터를 먼저 제압해줘! 부탁할게!", target = "" });
        infos.Add(new NPCInfo() { npcNo = 11, npcName = "마그마그", npcType = "", content = "화아아아아아악!\n땅두대지를 지키는 다섯 마리의 몬스터를 무찔러 주어서 고마워! 그럼 내가 새로운 마법을 알려줄려고 해!\n '펀치'라는 마법을 알려줄게! 따라해봐.\n가장 먼저, 오른손을 주먹지고 검지만 펴줘.\n손가락의 끝을 왼 주먹에 대어주면 끝이야!\n주먹을 가리킨다고 생각하면 좋아!\n이 마법은 아주 특별해서, 다른 마법과 결합해 사용할 수 있어.\n아까 배운 불 마법 기억나니? \n불 마법을 사용하고 펀치! 마법을 이어서 사용하면 새로운 마법을 사용할 수 있어.\n이름을 예상해보겠니?\n바로 불꽃펀치 마법이지~\n물하고 사용하면 아쿠아펀치 마법을 사용할 수도 있어.\n배운 마법을 한 번 사용해볼까?\n쿠오오오오오오오\n땅두대지를 지키는 다섯 마리의 몬스터가 다시 돌아와 불의 왕국을 어지럽히고 있어!\n몬스터를 다시 제압해줘!\n[신규 퀘스트] 땅두대지의 역습\n땅두대지를 지키는 몬스터를 5마리 사냥하기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 12, npcName = "마그마그", npcType = "", content = "쿠오오오오오오오\n땅두대지를 지키는 다섯 마리의 몬스터가 다시 돌아와 불의 왕국을 어지럽히고 있어!\n몬스터를 다시 제압해줘! 부탁할게!\n뭐야! 불꽃펀치를 잘 모르겠다구?\n마법 사전을 통해 배웠던 마법을 다시 익힐 수 있어.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 13, npcName = "마그마그", npcType = "", content = "화아아아아아악!\n땅두대지를 지키는 다섯 마리의 몬스터를 무찔러 주어서 고마워!\n덕분에 내 가족이 살 수 있었어.\n내가 더 강한 마법을 알려줄려고 해!\n 바로 '지옥'라는 마법을 알려줄게! 따라해봐.\n두 주먹의 검지를 펴서 바닥이 밖으로 향하게 세워줘.\n턱 양옆에 댔다가 머리 양쪽에 올려 대어 볼까?\n오른 주먹의 1지를 펴서 끝이 아래로 향하게 하여 내려줘. 끝이야!\n이 마법은 아주 특별해서, 다른 마법과 결합해 사용할 수 있어.\n불 마법을 사용하고 지옥 마법을 이어서 사용하면 새로운 마법을 사용할 수 있어.\n바로 불지옥 마법이지~\n물하고 사용하면 물지옥 마법을 사용할 수도 있어.\n이 마법은 지금까지 배웠던 어떤 마법들보다 매우 강력할 것이야.\n배운 마법을 한 번 사용해볼까?\n쿠오오오오오오오\n앗! 이번에는 땅두대지의 다섯 수호단이 불의 왕국을 어지럽히고 있어!\n다섯 수호단은 이전 몬스터보다도 더 강할 것이야\n땅두대지의 다섯 수호단을 제압해줘!\n[신규 퀘스트] 땅두대지의 역습\n땅두대지의 다섯 수호단 5마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 14, npcName = "마그마그", npcType = "", content = "앗! 이번에는 땅두대지의 다섯 수호단이 불의 왕국을 어지럽히고 있어!\n다섯 수호단은 이전 몬스터보다도 더 강할 것이야\n땅두대지의 다섯 수호단을 제압해줘!\n만약 이전까지 배운 마법이 기억나지 않는다면, 마법 사전을 참고해 다시 배울 수 있어!", target = "" });
        infos.Add(new NPCInfo() { npcNo = 15, npcName = "마그마그", npcType = "", content = "이제는 땅두대지를 무찌를 정도로 충분히 강해졌지?\n땅두대지를 무찔러 불의 왕국을 구해줘!\n만약 이전까지 배운 마법이 기억나지 않는다면, 마법 사전을 참고해 다시 배울 수 있어!", target = "" });
    }
    public void NPCChatEnter(string textDiscription, string textName, Sprite imageSprite)
    {
        NPCText_Discription.text = "";
        NPCText_name.text = textName;
        sl_image.sprite = imageSprite;
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
        if(questManager.GetCurrentIndex() == 1)
        {
            questManager.NextQuest();
        }
        NPCText_Discription.text = "";
        Debug.Log(questManager.GetCurrentIndex());
        Debug.Log(infos[0].npcName);
        NPCText_name.text = infos[questManager.GetCurrentIndex()].npcName;
        string textDiscription = infos[questManager.GetCurrentIndex()].content;
        sl_image.sprite = NPCImageList[questManager.GetCurrentIndex()];
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
        sl_image.sprite = null;
        NPCDialog.SetActive(false);
        isDialogActive = false;
    }

    private void EndDialog()
    {
        if(questManager.GetCurrentIndex() == 0 || questManager.GetCurrentIndex() == 2 || questManager.GetCurrentIndex() == 4 || questManager.GetCurrentIndex() == 7 || 
        questManager.GetCurrentIndex() == 8 || questManager.GetCurrentIndex() == 9 || questManager.GetCurrentIndex() == 11 || questManager.GetCurrentIndex() == 13)
        {
            questManager.NextQuest();
        }
        NPCChatExit();
    }
}
