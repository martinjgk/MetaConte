using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.UI;
public class Dragon : MonoBehaviour
{
    private int HP = 100;
    public Slider healthBar;
    public Animator animator;
    public float attackInterval = 2.0f; // 공격 간격 (초)
    private int currentAttackType = 0; // 현재 공격 타입 - used in blend tree

    private bool isDead = false; // 죽음 여부

    void Start()
    {
        StartCoroutine(AttackRoutine());
    }

    void Update(){
        healthBar.value = HP;

        // 임의로 키 입력을 감지하여 데미지 입히기
        // 테스트용
        if (Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(10); // 예시로 10의 데미지를 입힘
        }
       
    }
    
    public void TakeDamage(int damageAmount){
        if (isDead){
            return;
        }

        HP -= damageAmount;
        if (HP <= 0)
        {
            isDead = true;
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
            StartCoroutine(DestroyAfterDelay(5f)); // 5초 후에 사라지게 함
        }
        else
        {
            animator.SetTrigger("damage");
        }
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
