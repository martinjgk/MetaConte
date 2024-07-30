using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPunch : Magic
{
	[SerializeField]
	public float damage;
	[SerializeField]
	float duringTime;

	[SerializeField]
	float sizeUp;

	[SerializeField]
	Transform rightHand;
	[SerializeField]
	Transform leftHand;

	GameObject effectRightHand;
	GameObject effectLeftHand;


	private void Awake() {
		player = FindObjectOfType<PlayerMagic>();

		effectRightHand = Instantiate(effectDict[player.CurrentSkill]);
		effectRightHand.transform.SetParent(transform, false);
		effectRightHand.GetComponent<SphereCollider>().radius += sizeUp;

		effectLeftHand = Instantiate(effectDict[player.CurrentSkill]);
		effectLeftHand.transform.SetParent(transform, false);
		effectLeftHand.GetComponent<SphereCollider>().radius += sizeUp;



		transform.position = player.transform.position;
		transform.rotation = player.transform.rotation;
		UseSkill();
	}

	public override void UseSkill() {
		base.UseSkill();
		skillOnTime = Time.time;
		player.CurrentSkill = skillName;
	}

	protected override void OffSkill() {
		base.OffSkill();
	}

	private void Update() {
		if (Time.time - skillOnTime > duringTime) {
			OffSkill();
		}
	}

}
