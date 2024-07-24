using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIngameProfileManager : MonoBehaviour
{
    [SerializeField]
    private Text playerNameText;

    [SerializeField]
    private string playerName;

    [SerializeField]
    private Text playerLevelText;

    private int playerLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerNameText.text = " " +playerName;
        playerLevelText.text = playerLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdatePlayerNameUI(string newName){
        playerNameText.text = " " +newName;
    }

    public void UpdatePlayerLevelUI(int newLevel){
        playerLevel = newLevel;
        playerLevelText.text = newLevel.ToString();
    }

    [SerializeField]
    private Slider playerExpSlider;

    [SerializeField]
    private Image playerExpFill;
    protected float curExp; 
    public float maxExp;
    [SerializeField]
    private Text ExpText;
    public void SetExp(int cur, int max)
    {
        ExpText.text = "Exp "+cur+" / "+max;
        maxExp = max;
        curExp = cur;
        UpdateExpSlider();
    }

    private void UpdateExpSlider()
    {
        if (playerExpSlider != null)
        {
            playerExpSlider.value = curExp / maxExp;
            if (playerExpFill != null)
            {
                playerExpFill.fillAmount = playerExpSlider.value;
            }
        }
    }

    public void CheckCurrentSkillTime()
    {
        UpdateExpSlider();
    }
}
