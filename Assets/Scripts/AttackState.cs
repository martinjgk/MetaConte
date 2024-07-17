using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class AttackState : StateMachineBehaviour
{
    Transform player;

    public float longRangeAttackDistance = 3f;
    public float attackCooldown = 3.0f;
    private float lastAttackTime = 0f;
    public GameObject monsterSkillPrefab; // 스킬 프리팹

    public float heightOffset = 1.0f;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player);
        float distance = Vector3.Distance(player.position, animator.transform.position);

       if(distance > 6f ){
        animator.SetBool("isAttacking", false);
       }
       else{
        if(Time.time >= lastAttackTime + attackCooldown){
            setAttackType(animator, distance);
            lastAttackTime = Time.time;
        }
       }
    }

    void setAttackType(Animator animator, float distance){

        Dragon dragon = animator.GetComponent<Dragon>();

        if(distance >= longRangeAttackDistance){
            int longRangeAttackType = Random.Range(3,5);
            animator.SetFloat("attackType",longRangeAttackType);
            if(longRangeAttackType==3){
                FireFireball(animator);
            }
            if(longRangeAttackType==4){
                dragon.StartFireBreath();
            }
            
        }
        else{
            int closeRangeAttackType = Random.Range(0, 2); 
            animator.SetFloat("attackType", closeRangeAttackType);
        }
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

     void FireFireball(Animator animator)
    {
        if (monsterSkillPrefab != null && player != null)
        {
            Vector3 adjustedPosition = animator.transform.position + Vector3.up * heightOffset;
            GameObject monsterSkill = Instantiate(monsterSkillPrefab, adjustedPosition, animator.transform.rotation);
            Vector3 targetPosition = player.position + Vector3.up * 1.2f;
            monsterSkill.transform.LookAt(targetPosition);

            ParticleSystem particleSystem = monsterSkill.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
        }
        else
        {
            if(monsterSkillPrefab == null){
                Debug.LogError("MonsterSkill Prefab가 설정되지 않았습니다.");
            }
            if(player == null){
                Debug.LogError("Player가 설정되지 않았습니다.");
            }
        }
    }

}
