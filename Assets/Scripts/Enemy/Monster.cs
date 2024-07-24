using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;
using System;

public class Monster : LivingEntity
{
    public Slider healthBar;
    public Animator animator;
    public float attackInterval = 2.0f; // 공격 간격 (초)
    private int currentAttackType = 0; // 현재 공격 타입 - used in blend tree

    public event Action<GameObject> OnDeath; // 사망 이벤트


	private QuestManager questManager;

	void Start()
    {
        StartCoroutine(AttackRoutine());
		questManager = FindAnyObjectByType<QuestManager>();
	}

    void Update(){
        healthBar.value = HP;
		
    }


	public override void getDamage(float damage) {
		base.getDamage(damage);
		if (HP <= 0) {
			Die();
			isDead = true;
			animator.SetTrigger("die");
			GetComponent<Collider>().enabled = false;
			StartCoroutine(DestroyAfterDelay(5f)); // 5초 후에 사라지게 함
		}
		else {
			animator.SetTrigger("damage");
		}
	}

    void Die()
    {
		questManager.AddNumMonsterKill();
		OnDeath?.Invoke(gameObject); // 사망 이벤트 발생
        
    }


    IEnumerator AttackRoutine()
    {
        while (true)
        {
            // 현재 공격 유형을 설정
            animator.SetFloat("attackType", currentAttackType);
            animator.SetTrigger("attackTrigger");

            // 다음 공격 유형을 설정
            currentAttackType = (currentAttackType + 1) % 4;

            // 일정 시간 대기
            yield return new WaitForSeconds(attackInterval);
        }
    }


    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        // 다른 몬스터와 충돌할 경우 위치 조정 등의 처리를 수행
        if (other.CompareTag("Dragon"))
        {
            // 충돌을 피하기 위해 위치를 조정하거나 다른 처리를 수행할 수 있음

        }
    }
}
