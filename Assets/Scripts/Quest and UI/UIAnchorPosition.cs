using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnchorPosition : MonoBehaviour
{
	[SerializeField]
	Transform anchor;

    [SerializeField]
    Transform y_anchor;

    public bool isRotateOn;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = new Vector3(y_anchor.position.x, y_anchor.position.y, y_anchor.position.z);
        if (isRotateOn)
        {
            dir.x = anchor.position.x;
            dir.z = anchor.position.z;
            transform.rotation = anchor.rotation;
        }
        this.transform.position = dir;
    }
}
