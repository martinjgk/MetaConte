using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDown : Magic
{
	[SerializeField]
	public float damage;
	[SerializeField]
	float duringTime;

	Player player;
	[SerializeField]
	List<GameObject> effectList;
	GameObject effect;

	private void Awake() {
		player = GetComponent<Player>();
		effect.SetActive(false);
		
		if (player.CurrentSkill == "None") {

		}
	}

	public override void UseSkill() {


		if (Time.time - skillOnTime >= duringTime) {

		}
	}

	void SkillDown() {
		player.CurrentSkill = this.name;
		skillOnTime = Time.time;
		effect.SetActive(true);
	}

	public void OffSkill() {
		effect.SetActive(false);
		gameObject.SetActive(false);
		player.CurrentSkill = "None";
	}

	private void Update() {
		if (Time.time - skillOnTime > duringTime) {
			OffSkill();
		}
		transform.position = player.transform.position;
	}
}
