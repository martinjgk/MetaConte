using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldNPC_Trigger : MonoBehaviour
{
    [SerializeField]
    private WorldNPCManager worldNpcManager;

    void Start() {
        worldNpcManager = FindObjectOfType<WorldNPCManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            worldNpcManager.GetComponent<WorldNPCManager>().NPCChatEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            worldNpcManager.GetComponent<WorldNPCManager>().NPCChatExit();
        }
    }
}
