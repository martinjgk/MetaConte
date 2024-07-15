using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamaManager : MonoBehaviour
{
    static GamaManager instance;
    [SerializeField]
    new_Python_script pythonScript_Class;
    public static GamaManager Instance {  get { return instance; } }
	static NPCManager npcInstance;
	public static NPCManager NPCInstance { get { return npcInstance; } }
	public enum Element {
		None,
		Water,
		Fire,
		Dirt,
		Wind
	}
    private void Start()
    {
        // pythonScript_Class.Run();
        Init();
    }

    private void Update()
    {

    }

    void Init()
    {
        if(instance == null)
        {
            GameObject go = GameObject.Find("Game Manager");
            if (go == null)
            {
                go = new GameObject { name = "Game Manager" };
                go.AddComponent<GamaManager>();
            }
            DontDestroyOnLoad(go);
            instance = go.GetComponent<GamaManager>();
			npcInstance = go.gameObject.GetComponent<NPCManager>();
		}
    }
}
