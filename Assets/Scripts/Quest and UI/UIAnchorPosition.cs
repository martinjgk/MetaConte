using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnchorPosition : MonoBehaviour
{
	[SerializeField]
	Transform anchor;


    // Update is called once per frame
    void Update()
    {
        this.transform.position = anchor.position;
		this.transform.rotation = anchor.rotation;
    }
}
