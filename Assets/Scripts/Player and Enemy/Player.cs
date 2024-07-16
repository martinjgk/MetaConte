using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{

	private string current_skill;
	public string CurrentSkill { 
		get {
			return current_skill;
		}
		set {
			if (current_skill != value)
			{
				current_skill = value;
			}
		}
	}

	PlayerMagic playerSkill;

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
}
