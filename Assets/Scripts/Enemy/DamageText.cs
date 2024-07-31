using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    private TextMeshPro textMP;

    public float textUpSpeed = 1f; // Default value, adjust in inspector
    public float alphaSpeed = 1f; // Default value, adjust in inspector
    public float destroyTime = 1.5f; // Default value, adjust in inspector

    public int damage;

    void Start()
    {
        textMP = GetComponent<TextMeshPro>();
        textMP.text = damage.ToString();
        StartCoroutine(DestroyDamageText());
    }

    void Update()
    {
        // Move text up
        transform.Translate(new Vector3(0, textUpSpeed * Time.deltaTime, 0));

        // Fade out text
        float newAlpha = Mathf.Lerp(textMP.color.a, 0, Time.deltaTime * alphaSpeed);
        textMP.color = new Color(textMP.color.r, textMP.color.g, textMP.color.b, newAlpha);
    }

    private IEnumerator DestroyDamageText()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
