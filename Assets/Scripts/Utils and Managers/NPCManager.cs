using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

    [SerializeField]
    private GameObject StatUI;

    [SerializeField]
    private GameObject UIInfoDialog;
    void Start()
    {
        InitializeInfos();
        NPCDialog.SetActive(false);

        sentences = new Queue<string>();
        UIInfoDialog.SetActive(false);
    }

    void InitializeInfos()
    {
        infos.Add(new NPCInfo() { npcNo = 0, npcName = "story", npcType = "story", content = "한반도 깊은 산골 마을 '율전골'에 사는 주인공은 어린 시절부터 네 원소의 왕국에 대한 이야기를 듣고 자랐다.\n 네 원소의 전설은 마을 어른들의 이야기 속에서, 도서관의 낡은 책 속에서, 그리고 마을 광장에 새겨진 고대 비문 속에서 살아 숨 쉬고 있었다.\n천지가 처음 나뉘어지고 세상이 형성될 때, 네 가지 원소가 이 땅을 지배했다.\n 물\n 불\n 흙\n 공기\n 이 네 원소를 지배하는 자가 각각의 왕국을 이루었다.\n 각각의 왕국은 동, 서, 남, 북으로 뻗어가 독자적인 문명과 문화를 발전시켰다.\n각 왕국마다 원소를 사용하기 위한 독특한 마법이 있다. 손과 팔 동작으로 이루어진, 고대 주술과도 같은 성스러운 행동을 하면 원소가 반응을 한다고 알려져 있다..\n하지만\n갈라진 원소처럼,\n이 세상에는 큰 위기가 스며들었다!\n 물, 불, 흙, 공기 왕국에서는 모든 왕국의 모든 동작을 마스터해 세상을 구할 현자를 애타게 기다리고 있다.\n모든 왕국의 마법을 배워 율전골을 구해주세요!\n[신규 퀘스트]\nNPC에게 다가가 말을 걸어보세요! 이동은 키보드로 가능합니다.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 1, npcName = "온 [스승님의 조수]", npcType = "sign", content = "안녕? 이제야 정신이 들었나보구나. 나는 스승님의 조 온 이라고 해! 비록 스승님은.. 흑흑ㅠㅠ 스승님은 지키지 못했다만, 기절한 너를 끌고 생명의 요람, 물의 왕국으로 겨우 도망쳤어! 물의 왕국은 먼 과거 토끼와 거북이가 방문했다고 알려져 있지엉~\n물의 왕국 주민들 도움이 없었다면, 우리는 분명 괴물에게 잡아먹히고 말았을 것이야! 우리를 대표해서 물의 왕국 주민에게 감사 인사를 전해주고 와볼래?\n [신규 퀘스트] 물의 길: 물의 왕국 주민 쿠아쿠아에게 감사 인사를 전해주기, 이동은 WASD[앞뒤오왼]를 사용해 할 수 있습니다.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 2, npcName = "온 [스승님의 조수]", npcType = "sign", content = "우리를 대표해서 물의 왕국 주민에게 감사 인사를 전해주고 와볼래?\n [현재 퀘스트] 물의 길: 물의 왕국 주민 쿠아쿠아에게 감사 인사를 전해주기, 이동은 WASD[앞뒤오왼]를 사용해 할 수 있습니다.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 3, npcName = "온 [스승님의 조수]", npcType = "", content = "감사 인사는 잘 전해주고 왔수엉? 헉!!!!!!!!!!! 그런 일이 있었수엉? 물의 왕국을 어지럽힌 악당 화마와 몬스터를 무찌르기 위해서 전투에 나서보자! 스승님에게 배웠던 물, 내리다 마법을 꼭 사용해줘!\n[신규 퀘스트] 화마를 찾아서: 화마를 지키는 몬스터 3마리를 사냥하기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 4, npcName = "온 [스승님의 조수]", npcType = "", content = "물의 왕국을 어지럽힌 악당 화마와 몬스터를 무찌르기 위해서 전투에 나서보자!\n[현재 퀘스트] 화마를 찾아서: 화마를 지키는 몬스터 3마리를 사냥하기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 5, npcName = "온 [스승님의 조수]", npcType = "", content = "정말 잘 했어. 이렇게 전투를 훌륭하게 마칠 줄 알고 있었다니까~ 물의 왕국 주민 쿠아쿠아가 해줄 말이 있다고 하더라구\n[현재 퀘스트] 물 흐르듯: 물의 왕국 주민 쿠아쿠아 주변으로 이동해 말을 걸어보기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 6, npcName = "온 [스승님의 조수]", npcType = "", content = "화마의 선발대를 무찔러 물의 왕국 마을을 구하자!\n[현재 퀘스트] 화마를 찾아서: 화마의 선발대 몬스터 5마리를 사냥하기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 7, npcName = "온 [스승님의 조수]", npcType = "", content = "우와아아, 네가 화마의 선발대를 이긴 덕분에 마을 주민들이 모두 다치지 않았어!\n이젠 화마와 싸울 때가 된 것 같아. 화마를 무찌르고 물의 왕국을 구해줘!\n[신규 퀘스트] 화마의 최후: Boss인 화마를 무찔러 가뭄을 끝내기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 8, npcName = "온 [스승님의 조수]", npcType = "", content = "이젠 화마와 싸울 때가 된 것 같아. 화마를 무찌르고 물의 왕국을 구해줘!\n[현재 퀘스트] 화마의 최후: Boss인 화마를 무찔러 가뭄을 끝내기.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 9, npcName = "온 [스승님의 조수]", npcType = "", content = "이제 물의 왕국을 떠날 시간이야. 물의 왕국 바다 너머에 있는 불의 왕국에서도 큰 위기가 왔다고 하네.\n[신규 퀘스트] 가자! 불의 왕국으로: 퀘스트 창을 닫으면 불의 왕국으로 이동합니다.", target = "" });
        infos.Add(new NPCInfo() { npcNo = 10, npcName = "온 [스승님의 조수]", npcType = "", content = "물의 왕국을 구했다고 방심해서는 안될거야! 여기 불의 왕국은 더더욱 위험하다구. 불의 왕국에 왔으니 마을 주민들과 대화를 하기 위해선 불 마법을 배워야겠지?\n나도 스승님의 조수였으니 이정도는 알고 있수엉~ 불 마법은 Level 1 마법으로 기본 원소 마법에 해당해. 동작을 차근차근 알려줄테니, 천천히 따라해부엉~\n가장 먼저, 두 손을 펴서 손등이 밖으로 보이게 해줘. 이어서, 손끝이 위로 향하게 세워줘.\n마지막으로 손가락을 가볍게 흔들면서 상하로 엇갈리게 움직여줘! 불길이 솟구쳐 오르는 것을 나타내는 동작을 떠올리면 좋아! 어렵지 않지?\n이 마법은 이전에 배웠던 Level 2 마법과 함께 사용할 수 있어. Level 2 마법을 잘 모르겠다면 수어 사전을 찾아보렴~\n[신규 퀘스트] 불의 왕국 주민 주변으로 이동해 말을 걸어보기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 11, npcName = "온 [스승님의 조수]", npcType = "", content = "물의 왕국을 구했다고 방심해서는 안될거야! 여기 불의 왕국은 더더욱 위험하다구. [현재 퀘스트] 불의 왕국 주민 주변으로 이동해 말을 걸어보기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 12, npcName = "온 [스승님의 조수]", npcType = "", content = "땅두대지를 지키는 몬스터를 무찔러 불의 왕국 마을을 구하자!\n[현재 퀘스트] 땅두대지의 습격: 땅두대지를 지키는 몬스터 3마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 13, npcName = "온 [스승님의 조수]", npcType = "", content = "정말 잘 했어. 이렇게 전투를 훌륭하게 마칠 줄 알고 있었다니까~ 불의 왕국 주민 마그마그가 해줄 말이 있다고 하더라구\n[현재 퀘스트] 불꽃~ 펀치: 불의 왕국 마그마그 주변으로 이동해 말을 걸어보기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 14, npcName = "온 [스승님의 조수]", npcType = "", content = "[쿠오오오오오오: 가까이서 괴성이 들려온다…] 땅두대지를 지키는 다섯 마리의 몬스터를 제압해보자!\n[현재 퀘스트]\n땅두대지의 역습: 땅두대지를 지키는 몬스터를 5마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 15, npcName = "온 [스승님의 조수]", npcType = "", content = "정말 잘 했어. 이렇게 전투를 훌륭하게 마칠 줄 알고 있었다니까~ 불의 왕국 주민 마그마그가 해줄 말이 있다고 하더라구\n[현재 퀘스트] 웰 컴 투 헬: 불의 왕국 마그마그 주변으로 이동해 말을 걸어보기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 16, npcName = "온 [스승님의 조수]", npcType = "", content = "일곱 수호단은 이전 몬스터보다도 더 강할 것이야, 땅두대지의 일곱 수호단을 제압해줘!\n[현재 퀘스트] 땅두대지의 분노: 땅두대지의 일곱 수호단 7마리 사냥하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 17, npcName = "온 [스승님의 조수]", npcType = "", content = "이제는 땅두대지를 무찌를 정도로 충분히 강해졌지? 땅두대지를 무찔러 불의 왕국을 구해줘!\n[신규 퀘스트] 땅두대지의 최후: 땅두대지를 무찔러 불의 왕국을 구하기", target = "" });
        infos.Add(new NPCInfo() { npcNo = 18, npcName = "온 [스승님의 조수]", npcType = "", content = "빠바바바바바밤\n땅두대지를 무찔러 불의 왕국을 구했구나! 정말 대단한걸?\nTo..Be..Continue", target = "" });
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
        if(questManager.GetCurrentIndex() == 1 || questManager.GetCurrentIndex() == 3 || 
        questManager.GetCurrentIndex() == 7 ||  questManager.GetCurrentIndex() == 10 || questManager.GetCurrentIndex() == 12)
        {
            questManager.NextQuest();
            if(questManager.GetCurrentIndex() == 4)
            {
                StartCoroutine(ShowUIInfoDialog());
            }
        }
        NPCChatExit();
        if(questManager.GetCurrentIndex() == 9){
            SceneManager.LoadScene("Transition1Scene");
        }
    }

    private IEnumerator ShowUIInfoDialog()
    {
        UIInfoDialog.SetActive(true);
        yield return new WaitForSeconds(10);
        UIInfoDialog.SetActive(false);
    }
}
