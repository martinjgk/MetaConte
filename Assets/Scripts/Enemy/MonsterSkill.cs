using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MonsterSkill : MonoBehaviour
{

    public float speed = 10f; // 투사체의 속도
    public float lifetime = 5f; // 투사체의 생존 시간
    private Transform target;

    private Rigidbody skillRigidBody;


    public ParticleSystem monsterSkillEffect;

    // Start is called before the first frame update
    void Start()
    {
        monsterSkillEffect = GetComponent<ParticleSystem>();
        monsterSkillEffect.Play();
        
        skillRigidBody = GetComponent<Rigidbody>();
        skillRigidBody.velocity = transform.forward * speed;

        Destroy(gameObject, lifetime);
    }

    public void Initialize(Transform target)
    {
        this.target = target;
        Destroy(gameObject, lifetime);
    }


    // Update is called once per frame
    void Update()
    {
        // if (target != null)
        // {
        //     //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //     Vector3 direction = (target.position - transform.position).normalized;
        //     transform.position += direction * speed * Time.deltaTime;
        // }
        // else
        // {
        //     Destroy(gameObject); // 타겟이 없어지면 fireball도 없어짐
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Fireball이 플레이어에게 도달했습니다.");
            // 여기서 플레이어에게 피해를 입히는 로직 추가 가능
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // FireballEffect를 비활성화
        if (monsterSkillEffect != null)
        {
            monsterSkillEffect.Stop();
        }
    }
}
