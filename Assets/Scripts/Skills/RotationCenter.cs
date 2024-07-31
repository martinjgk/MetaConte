using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCenter : MonoBehaviour
{
	[SerializeField]
	Transform center;
	[SerializeField]
	float circleR;
	[SerializeField]
	float deg;
	[SerializeField]
	float speed;

	private void FixedUpdate() {
		deg += Time.deltaTime * speed;
		if (deg < 360) {

			float radius = Mathf.Deg2Rad * deg;
			float x = circleR * Mathf.Sin(radius);
			float y = circleR * Mathf.Cos(radius);

			transform.position = center.position + new Vector3(x, -0.5f, y);
			transform.rotation = Quaternion.Euler(0, 0, deg * -1);

		}
		else {
			deg = 0;
		}
	}
}
