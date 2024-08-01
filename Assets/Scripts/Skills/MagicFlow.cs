using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MagicFlow : Magic
{
	[SerializeField]
	float damageCoef;
	float damage;
	[Header("Circle")]
	[Range(0, 30)]
	[SerializeField] private float viewRange;               // �þ� ����
	[Range(0, 360)]
	[SerializeField] private float viewAngle;               // �þ� ����

	[Header("Target")]
	[SerializeField] private LayerMask targetMask;          // Ž�� ���
	[SerializeField] private List<Transform> targetList;    // Ž�� ��� ����Ʈ


	[SerializeField]
	float speed;

	GameObject effect;
	bool isTargetOn = false;
	Transform target;
	Transform startTransform;
	NavMeshAgent agent;

	private void Awake() {
		player = FindAnyObjectByType<PlayerMagic>();
		agent = GetComponent<NavMeshAgent>();
		damage = player.damage * damageCoef;
		startTransform = player.transform;
		transform.position = startTransform.position;
		transform.rotation = startTransform.rotation;

		targetList = new List<Transform>();

		effect = Instantiate(effectDict[player.CurrentSkill], transform.position + Vector3.up, transform.rotation, transform);
		CheckTarget();
		
		agent.speed = speed;
		UseSkill();
	}


	private void FixedUpdate() {
		if (isTargetOn && target != null) {
			agent.SetDestination(target.position);
			float origin_distance = Vector3.Distance(transform.position, startTransform.position);
			float target_distance = Vector3.Distance(transform.position, target.position);
			if (target_distance <= 2) {
				target.GetComponent<LivingEntity>().getDamage(damage);
				Debug.Log("Flow Target Enter!!!!!");
				OffSkill();
			}
			if (origin_distance >= viewRange)
			{
				OffSkill();
			}
		}
		else {
			OffSkill();
		}
    }

	public override void UseSkill() {
		base.UseSkill();
	}

	protected override void OffSkill() {
		isTargetOn = false;
		effect.transform.SetParent(null);
		Destroy(effect, 3f);
		base.OffSkill();
	}

	void CheckTarget() {
		targetList.Clear();
		Collider[] cols = Physics.OverlapSphere(transform.position, viewRange, targetMask);
		foreach (var e in cols) {
			// ������ ����� ������ ���Ѵ�.
			Vector3 direction = (e.transform.position - transform.position).normalized;

			print("target in range");

			// ������ ������ ������ ���� �̳��� �ִ��� Ȯ���Ѵ�.
			// viewAngle �� ��ä�� ��ü �����̱� ������, 0.5�� �����ش�.
			if (Vector3.Angle(transform.forward, direction) < (viewAngle * 0.5f) && e.gameObject.tag == "Enemy") {
				print("target in angle");
				isTargetOn = true;
				targetList.Add(e.transform);
			}
		}
		if (isTargetOn) {
			target = targetList[0];
		}
		else {
			target = null;
			OffSkill();
		}
	}

	private void OnTriggerEnter(Collider other) {
		LivingEntity dmgable = other.gameObject.GetComponent<LivingEntity>();
		if (dmgable != null && other.gameObject.tag == "Enemy") {
			StartCoroutine(GiveDamage(dmgable));
			Debug.Log("FLow Trigger!!!");

			if (dmgable.transform.Equals(target.transform)) {
				Debug.Log("Flow End!!!!!");
				OffSkill();
			}
		}
	}

	IEnumerator GiveDamage(LivingEntity dmgable) {
		yield return new WaitForSeconds(0.5f);
		dmgable.getDamage(damage / 2f);
	}
}
