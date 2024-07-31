using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGMManager : MonoBehaviour
{
    public static BGMManager instance;
    private AudioSource audioSource;

    [SerializeField]
    List<AudioClip> clips;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            SceneManager.sceneLoaded += OnSceneLoaded;
            audioSource.clip = clips[0];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (audioSource.clip == clip)
        {
            return;
        }
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopBGM()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // 씬 로드 이벤트에서 메서드 제거
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log(scene.name);
        if (scene.name == "Demo 2") {

            audioSource.clip = clips[1];
        }
        else if ( scene.name == "WaterTestField"
        ){
            audioSource.clip = clips[2];
        }
        else if ( scene.name == "Transition1Scene"){
            audioSource.clip = clips[3];
        }
        else if ( scene.name == "FireTestField"){
            audioSource.clip = clips[4];
        }

        audioSource.Play();
    }
}
