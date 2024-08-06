using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireEffect : MonoBehaviour {
	MagicFire magicElement;
	Transform hand;
	SphereCollider coll;
	[SerializeField]
	float circleR;
	[SerializeField]
	float deg;
	[SerializeField]
	float speed;

	// Start is called before the first frame update
	void OnEnable() {
		hand = GameObject.Find("RightHandAnchor").transform;
		magicElement = GetComponentInParent<MagicFire>();
		coll = gameObject.GetComponent<SphereCollider>();
	}

	private void FixedUpdate() {
		float spd;
		if (Input.GetMouseButton(0)) {
			spd = speed * 2;
		}
		else {
			spd = speed;
		}
		deg += Time.deltaTime * spd;
		if (deg < 360) {

			float radius = Mathf.Deg2Rad * deg;
			float x = circleR * Mathf.Sin(radius);
			float y = circleR * Mathf.Cos(radius);

			transform.position = hand.position + new Vector3(x, 0, y);
			transform.rotation = Quaternion.Euler(0, 0, deg * -1);

		}
		else {
			deg = 0;
		}
	}


	private void OnTriggerEnter(Collider other) {
		Debug.Log("Enter!" + other.gameObject.tag);
		if (other.gameObject.tag == "Enemy") {
			Debug.Log("Dragon!!");
			LivingEntity lv = other.gameObject.GetComponent<LivingEntity>();
			if (lv != null) {
				Debug.Log("!!!!!!!Enter!!!!!!");
				lv.getDamage(magicElement.damage);
			}
		}

	}
}