using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
	private static Manager s_instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (s_instance) {
			DestroyImmediate(gameObject);
			return;
		}

		s_instance = this;
		DontDestroyOnLoad(gameObject);
    }
}
