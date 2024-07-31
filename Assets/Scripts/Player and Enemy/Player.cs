using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
	PlayerMagic playerSkill;
	public int defense; // 방어력
	public int attack; // 공격력
    public float speed; //이동 속도
    public int health; // 체력
    public int mana; // 마나

	private static Player s_instance;
	// Start is called before the first frame update
	void Awake() {
		if (s_instance) {
			DestroyImmediate(gameObject);
			return;
		}

		s_instance = this;
		DontDestroyOnLoad(gameObject);
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
