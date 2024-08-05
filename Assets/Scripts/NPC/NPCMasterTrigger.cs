using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCMasterTrigger : MonoBehaviour
{
    [SerializeField]
    private NPCManager nPCManager;
    void Start()
    {
        nPCManager = FindObjectOfType<NPCManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            nPCManager.NPCChatEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            nPCManager.NPCChatExit();
        }
    }
}
