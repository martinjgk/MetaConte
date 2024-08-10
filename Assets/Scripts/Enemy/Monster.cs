using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Monster : LivingEntity
{
    public Slider healthBar;
    public Animator animator;
    public float attackInterval = 2.0f; // 공격 간격 (초)
    private int currentAttackType = 0; // 현재 공격 타입 - used in blend tree

    public event Action<GameObject> OnDeath; // 사망 이벤트
    [SerializeField] private GameObject damageTextPrefab;

    public AudioClip skillSound; // 스킬 사운드 클립
    private AudioSource audioSource; // AudioSource 컴포넌트



	private QuestManager questManager;

	void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(AttackRoutine());
		questManager = FindAnyObjectByType<QuestManager>();
	}

    void Update(){
        healthBar.value = HP;

        if (Input.GetKeyDown(KeyCode.U))
        {
            getDamage(10); // 예시로 10의 데미지를 입힘
        }
		
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
            ShowDamageText(damage);
		}
	}
    private void ShowDamageText(float damage)
    {
        if (damageTextPrefab)  // Check if the prefab is assigned
        {
            Vector3 textPosition = transform.position + new Vector3(9.0f, 0, 0);
            GameObject textObj = Instantiate(damageTextPrefab, textPosition, Quaternion.identity);
            DamageText dmgText = textObj.GetComponent<DamageText>();
            if (dmgText)
            {
                dmgText.damage = (int)damage;  // Set the damage amount to display
            }
            else
            {
                Debug.LogError("DamageText component not found on the prefab!");
            }
        }
        else
        {
            Debug.LogError("Damage text prefab is not assigned!");
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

    public void PlaySkillSound()
    {
        if (audioSource != null && skillSound != null)
        {
            audioSource.PlayOneShot(skillSound);
        }
    }
}
