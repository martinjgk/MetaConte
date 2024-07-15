using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    private bool isDead = false;
    private float hp = 100;
	private float lastDamageT = -10;
	private float damageCool = 0.5f;


    public float HP {
        get {
            return hp;
        }
        set {
            hp = value;
            if (!isDead && hp <= 0)
            {
                hp = 0;
                die();
            }
        }
    }


    public void die()
    {
        if (!isDead)
        {
            isDead = true;

            Destroy(gameObject, 3.0f);
        }
    }

	public virtual void getDamage(float damage)
    {
		if (!isDead && Time.time - lastDamageT >= damageCool) {
			HP -= damage;
		}
    }
}
