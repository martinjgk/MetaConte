using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup group1;
    [SerializeField]
    private CanvasGroup group2;
        
    [SerializeField]
    private CanvasGroup image1CanvasGroup;
    [SerializeField]
    private RectTransform imageRectTransform; // Image3의 RectTransform
    [SerializeField]
    private CanvasGroup image3CanvasGroup; // Image3의 CanvasGroup

    public float transitionDuration = 1.0f;
    public float imageDropDuration = 0.5f; // Image3의 드롭다운 시간
    private float startY = 815f; // Image3의 시작 y 위치 (화면 위쪽)
    private float endY = 275f; // Image3의 끝 y 위치 (화면 아래쪽)

    [SerializeField] private BGMManager bGMManager;

    private void Start()
    {
        group2.gameObject.SetActive(false);
        StartCoroutine(TransitionGroups());
    }

    private IEnumerator TransitionGroups()
    {
        // Image1 등장
        image1CanvasGroup.alpha = 0;
        yield return new WaitForSeconds(1f);

        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            image1CanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / transitionDuration);
            yield return null;
        }

        //yield return new WaitForSeconds(2f);

        // Image3 등장
        image3CanvasGroup.alpha = 0;
        Vector2 startPos = new Vector2(imageRectTransform.anchoredPosition.x, startY);
        Vector2 endPos = new Vector2(imageRectTransform.anchoredPosition.x, endY);
        elapsedTime = 0f;

        while (elapsedTime < imageDropDuration)
        {
            elapsedTime += Time.deltaTime;
            imageRectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, elapsedTime / imageDropDuration);
            image3CanvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / imageDropDuration);
            yield return null;
        }
        Debug.Log("Final Position: " + imageRectTransform.anchoredPosition);
        // Image3가 완전히 등장한 후 잠시 대기
        //yield return new WaitForSeconds(2f);

        // group2로 전환
        group2.alpha = 0;
        group2.gameObject.SetActive(true);

        elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / transitionDuration);
            group1.alpha = alpha;
            group2.alpha = 1 - alpha;
            yield return null;
        }

        group1.gameObject.SetActive(false);
        group2.alpha = 1;
    }

    public void OnButtonClick(){
        SceneManager.LoadScene("Demo 2");
    }
}
