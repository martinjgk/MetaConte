using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWater : Magic {
	[SerializeField]
	public float damage;
	[SerializeField]
	float duringTime;

	Player player;

	
	private void Awake() {
		
		player = FindObjectOfType<Player>();
		UseSkill();
	}

	public override void UseSkill() {
		skillOnTime = Time.time;
		player.CurrentSkill = this.name;
		
	}

	public void OffSkill() {
		
		gameObject.SetActive(false);
		player.CurrentSkill = "None";
	}

	private void Update() {
		if(Time.time - skillOnTime > duringTime) {
			OffSkill();
		}
		transform.position = player.transform.position;
	}
}
