using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;
using UnityEngine.AI;

public class MagicHell : Magic
{
	[SerializeField]
	float damageCoef;
	[SerializeField]
	[Range(0f, 1f)]
	float speedReduceAmount;
	[SerializeField]
	float duringTime;

	[Header("Circle")]
	[Range(0, 30)]
	[SerializeField] private float viewRange;               // 시야 범위

	[Header("Target")]
	[SerializeField] private LayerMask targetMask;          // 탐색 대상
	[SerializeField] private List<LivingEntity> targetList;    // 탐색 결과 리스트


	float damage;

	private void Awake() {
		player = FindAnyObjectByType<PlayerMagic>();
		GameObject effect = Instantiate(effectList[effectNameList.IndexOf(player.CurrentSkill)], transform);
		damage = player.damage * damageCoef;
		skillOnTime = Time.time;
		UseSkill();
	}

	private void Update() {
		if (Time.time - skillOnTime >= duringTime) {
			OffSkill();
		}
	}

	public override void UseSkill() {
		base.UseSkill();
		StartCoroutine(DoSkill());
	}

	void CheckTarget() {
		targetList.Clear();
		Collider[] cols = Physics.OverlapSphere(transform.position, viewRange, targetMask);
		foreach (var e in cols) {
			LivingEntity entity = e.GetComponent<LivingEntity>();
			if (entity != null && entity.gameObject.tag == "Enemy" && !targetList.Contains(entity)) {
				targetList.Add(entity);
			}
		}
	}

	protected override void OffSkill() {
		base.OffSkill();
	}

	IEnumerator DoSkill() {
		CheckTarget();
		foreach (var target in targetList) {
			NavMeshAgent agent = target.GetComponent<NavMeshAgent>();
			agent.speed = target.runSpeed * speedReduceAmount;
			target.getDamage(damage);
		}
		yield return new WaitForSeconds(1f);
		while (true) {
			CheckTarget();
			foreach (var target in targetList) {
				NavMeshAgent agent = target.GetComponent<NavMeshAgent>();
				agent.speed = target.runSpeed * speedReduceAmount;
			}
			yield return new WaitForSeconds(1f);
		}
	}
}
