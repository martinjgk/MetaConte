using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Transition1 : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    private float duration = 5f;

    void Start()
    {
        StartCoroutine(FillSlider());
    }

    IEnumerator FillSlider()
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            slider.value = Mathf.Clamp01(elapsedTime / duration) * 100;
            yield return null;
        }

        SceneManager.LoadScene("FireTestField");
    }
}
