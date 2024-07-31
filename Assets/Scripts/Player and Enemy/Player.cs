using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
	PlayerMagic playerSkill;

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
		DontDestroyOnLoad(gameObject);
		StartCoroutine(MPUpdate());
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
}
