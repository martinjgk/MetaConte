using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWater : Magic {
	[SerializeField]
	public float damage;
	[SerializeField]
	float duringTime;

	Player player;
	[SerializeField]
	GameObject effect;

	public override void UseSkill() {
		effect.SetActive(false);
		player = FindObjectOfType<Player>();
		

		if (Time.time - lastSkillTime >= coolTime) {
			player.ElementState = GamaManager.Element.Water;
			lastSkillTime = Time.time;
			effect.SetActive(true);
		}
	}

	public void OffSkill() {
		effect.SetActive(false);
		gameObject.SetActive(false);
		player.ElementState = GamaManager.Element.None;
	}

	private void Update() {
		if(Time.time - lastSkillTime > duringTime) {
			OffSkill();
		}
		transform.position = player.transform.position;
	}
}
