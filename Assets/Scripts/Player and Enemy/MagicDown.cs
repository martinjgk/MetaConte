using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MagicDown : Magic
{
	[SerializeField]
	public float damage;
	[SerializeField]
	float duringTime;
	[SerializeField]
	float moveSpd;
	[SerializeField]
	float maxDistance;
	[SerializeField]
	float damageTick;

	GameObject effect;

	Vector3 moveDir;
	float movedDistance = 0;
	float deltaPos;
	[SerializeField]
	bool isReached = false;

	private void Awake() {
		player = FindObjectOfType<PlayerMagic>();

		effect = Instantiate(effectDict[player.CurrentSkill]);
		effect.transform.SetParent(transform, false);
		moveDir = player.transform.forward;

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

		deltaPos = moveSpd * Time.deltaTime;
		if (!isReached) {
			if (movedDistance <= maxDistance) {
				transform.Translate(moveDir * deltaPos);
				// transform.position = transform.position + (moveDir * deltaPos);
				movedDistance += deltaPos;
			}
			else {
				isReached = true;
			}
		}
	}

	private void OnCollisionEnter(Collision collision) {
		IDamageable dmgable = collision.gameObject.GetComponent<IDamageable>();
		if (dmgable != null) {
			isReached = true;
			StartCoroutine(GiveDamage(dmgable));
		}
    }

	private void OnCollisionExit(Collision collision) {
		IDamageable dmgable = collision.gameObject.GetComponent<IDamageable>();
		if (dmgable != null) {
			isReached = false;
			StopCoroutine(GiveDamage(dmgable));
		}
	}

	IEnumerator GiveDamage(IDamageable damageable) {
		yield return new WaitForSeconds(damageTick);
		damageable.getDamage(damage);
	}
}
