using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCMasterTrigger : MonoBehaviour
{
    private GameObject Main;
    void Start()
    {
        Main = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "myplayer")
        {
            Main.GetComponent<NPCManager>().NPCChatEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "myplayer")
        {
            Main.GetComponent<NPCManager>().NPCChatExit();
        }
    }
}
