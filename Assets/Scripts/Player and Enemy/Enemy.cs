using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
	[SerializeField]
	Animator animator;
	[SerializeField]
	Slider hpSlider;

	private void Start() {
		animator = gameObject.GetComponent<Animator>();
		hpSlider = gameObject.GetComponentInChildren<Slider>();
		hpSlider.value = HP;
	}


	private void Update() {
		
	}
	public override void getDamage(float damage) {
		base.getDamage(damage);
		hpSlider.value = HP;
		if (HP <= 0) {
			animator.SetTrigger("die");
		}
		else {
			animator.SetTrigger("damage");
		}
	}
}
