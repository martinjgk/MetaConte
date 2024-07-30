using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowTrail : MonoBehaviour
{
	[SerializeField]
	float tick;
	[SerializeField]
	GameObject effect;

	List<GameObject> effects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine(EffectGenerator());
    }

	IEnumerator EffectGenerator() {
		while(true) {
			GameObject e = Instantiate(effect, transform.position, transform.rotation);
			e.transform.rotation = Quaternion.Euler(e.transform.rotation.x, e.transform.rotation.y, e.transform.position.z + 90f);
			effects.Add(e);
			StartCoroutine(EffectDestroyer(e));
			yield return new WaitForSeconds(tick);
		}
	}

	IEnumerator EffectDestroyer(GameObject e) {
		yield return new WaitForSeconds(3f);
		Destroy(e);
	}

	private void OnDestroy() {
		foreach (GameObject e in effects) {
			Destroy(e);
		}
	}
}
