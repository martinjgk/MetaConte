using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
	PlayerMagic playerSkill;
	public int defense; // ����
	public int attack; // ���ݷ�
    public int health; // ü��
    public int mana; // ����

	float mp = 100;
	[SerializeField]
	float mpUpperBound = 100;

	public float mpRecoverT;
	public float mpRecoverAmount;
	public float mpReduceAmount;

	public float MP {
		get {
			return mp;
		}
		set {
			mp = value;
			if (mp <= 0) {
				mp = 0;
			}
			else if (mp >= mpUpperBound) {
				mp = mpUpperBound;
			}
		}
	}

	private static Player s_instance;
	// Start is called before the first frame update
	void Awake() {
		if (s_instance) {
			DestroyImmediate(gameObject);
			return;
		}

		s_instance = this;
		// DontDestroyOnLoad(gameObject);
		// StartCoroutine(MPUpdate());
	}

	// Start is called before the first frame update
	void Start()
	{
		playerSkill = GetComponent<PlayerMagic>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	IEnumerator MPUpdate() {
		while (!isDead) {
			MP -= mpReduceAmount;
			yield return new WaitForSeconds(mpRecoverT);
		}
	}
		

	// 방어력을 증가시키는 메소드
    public void IncreaseDefense(int amount)
    {
        defense += amount;
        Debug.Log("Defense increased. New defense: " + defense);
    }

    public void IncreaseAttack(int amount)
    {
        attack += amount;
        Debug.Log($"Attack increased by {amount}. New attack: {attack}");
    }

    public void IncreaseSpeed(int amount)
    {
        speed += amount;
        Debug.Log($"Speed increased by {amount}. New speed: {speed}");
    }

    public void RestoreHealth(int amount)
    {
        health += amount;
        Debug.Log($"Health restored by {amount}. New health: {health}");
    }

    public void RestoreMana(int amount)
    {
        mana += amount;
        Debug.Log($"Mana restored by {amount}. New mana: {mana}");
    }
}