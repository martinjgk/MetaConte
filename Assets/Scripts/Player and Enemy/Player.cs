using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
	private GamaManager.Element element_state;
	public GamaManager.Element ElementState { 
		get {
			return element_state;
		}
		set {
			if (element_state != value)
			{
				element_state = value;
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
