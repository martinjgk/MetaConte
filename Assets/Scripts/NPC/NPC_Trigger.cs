using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Trigger : MonoBehaviour
{
	[SerializeField]
	string giveSkill;
	[TextArea]
    public string ChatText = "";
    [TextArea]
    public string NameText = "";
    public Sprite ImageSprite = null;
    private NPCManager npcManager;
    void Start()
    {
		npcManager = GameObject.Find("Game Manager").GetComponent<NPCManager>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
			npcManager.NPCChatEnter(ChatText, NameText, ImageSprite);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
			npcManager.NPCChatExit();
			PlayerMagic player = other.GetComponent<PlayerMagic>();
			if (player != null) {
				player.AddSkill(giveSkill);
			}
		}
    }
}