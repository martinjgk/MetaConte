using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCMasterTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject NPCManager;
    void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            NPCManager.GetComponent<NPCManager>().NPCChatEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            NPCManager.GetComponent<NPCManager>().NPCChatExit();
        }
    }
}
