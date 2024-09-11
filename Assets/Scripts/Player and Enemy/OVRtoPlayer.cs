using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRtoPlayer : MonoBehaviour
{
    GameObject player;
    Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position;
    }
}
