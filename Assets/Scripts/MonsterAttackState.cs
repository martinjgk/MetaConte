using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MonsterAttackState : StateMachineBehaviour
{
    Transform player;

    public float longRangeAttackDistance = 3f;
    public int longRangeAttackType = 3;
    public float attackCooldown = 2.0f;
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

       if(distance > 8f ){
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
        if(distance >= longRangeAttackDistance){
            animator.SetFloat("attackType",longRangeAttackType);
            FireFireball(animator);
        }
        else{
            int closeRangeAttackType = Random.Range(0, longRangeAttackType); 
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

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
