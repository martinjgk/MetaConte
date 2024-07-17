using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFire : Magic {
	[SerializeField]
	public float damage;
	[SerializeField]
	float duringTime;

	private void Awake() {
		player = FindObjectOfType<PlayerMagic>();
		UseSkill();
	}

	public override void UseSkill() {
		base.UseSkill();
		skillOnTime = Time.time;
	}

	protected override void OffSkill() {
		base.OffSkill();
		gameObject.SetActive(false);
		Destroy(gameObject);
	}

	private void Update() {
		if(Time.time - skillOnTime > duringTime) {
			OffSkill();
		}
		transform.position = player.transform.position;
	}
}
