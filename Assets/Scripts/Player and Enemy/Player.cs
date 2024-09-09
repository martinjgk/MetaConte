using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : LivingEntity
{
	PlayerMagic playerSkill;
	public int defense = 50; 
	public int attack = 50; 
	public int gold = 0;

	float mp = 100;
	[SerializeField]
	float mpUpperBound = 100;

	public float mpRecoverT;
	public float mpRecoverAmount = 5f;
	public float mpReduceAmount = 7f;

	public float hpRecoverAmount = 10f;  
	public float hpRecoverInterval = 3f;
	[SerializeField]
	private float hpUpperBound = 100;

	// UI Elements
	public Text hpText;
	public Text mpText;
	public Text defenseText;
	public Text attackText;
	public Text speedText;
	public Text goldText;
	public Slider manaSlider;
	public Slider hpSlider;

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
		UpdateUI();
		StartCoroutine(HPRegenerator());
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold added: " + amount + ". Current Gold: " + gold);
		UpdateUI();
    }

	public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            Debug.Log("Gold spent: " + amount + ". Current Gold: " + gold);
			UpdateUI();
            return true;
        }
        else
        {
            Debug.Log("Not enough gold!");
            return false;
        }
    }

    public int GetGold()
    {
        return gold;
    }

	IEnumerator HPRegenerator() {
		while (!isDead) {
			RestoreHealth((int)hpRecoverAmount);
			UpdateUI();  // HP가 회복될 때마다 UI 업데이트
			yield return new WaitForSeconds(hpRecoverInterval);
		}
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
		UpdateUI();
    }

    public void IncreaseAttack(int amount)
    {
        attack += amount;
        Debug.Log($"Attack increased by {amount}. New attack: {attack}");
		UpdateUI();
    }

    public void IncreaseSpeed(int amount)
    {
        speed += amount;
        Debug.Log($"Speed increased by {amount}. New speed: {speed}");
		UpdateUI();
    }

    public void RestoreHealth(int amount)
    {
        HP += amount;
		if (HP > hpUpperBound) { // HP가 최대값을 넘지 않도록 제한
            HP = hpUpperBound;
        }
        Debug.Log($"Health restored by {amount}. New health: {HP}");
		UpdateUI();
    }

    public void RestoreMana(int amount)
    {
        mp += amount;
        Debug.Log($"Mana restored by {amount}. New mana: {mp}");
		UpdateUI();
    }
	

    public override void getDamage(float damage)
    {
        float finalDamage = damage - (defense * 0.1f);
        if (finalDamage < 0) finalDamage = 0;

        HP -= finalDamage;
		UpdateUI();
    }

	public void AttackEnemy(LivingEntity enemy)
    {
        if (enemy != null)
        {
            float damage = attack; 
            enemy.getDamage(damage);
            Debug.Log("Enemy attacked for " + damage + " damage.");
        }
    }

	public void UpdateUI()
	{
		if (hpText != null)
			hpText.text = "HP: " + HP;

		if (mpText != null)
			mpText.text = "MP: " + MP;

		if (defenseText != null)
			defenseText.text = "DEF: " + defense;

		if (attackText != null)
			attackText.text = "ATK: " + attack;

		if (speedText != null)
			speedText.text = "SPD: " + speed;

		if (goldText != null)
			goldText.text = " " + gold;

		if (manaSlider != null)
			manaSlider.value = MP / mpUpperBound;

		if (hpSlider != null)
            hpSlider.value = HP / hpUpperBound;;
	}
}