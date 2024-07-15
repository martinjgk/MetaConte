using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDown : Magic
{
	[SerializeField]
	public float damage;
	[SerializeField]
	float duringTime;

	Player player;
	[SerializeField]
	List<GameObject> effectList;
	GameObject effect;

	public override void UseSkill() {
		effect.SetActive(false);
		player = FindObjectOfType<Player>();


		if (Time.time - lastSkillTime >= coolTime) {
			switch (player.ElementState) {
				case GamaManager.Element.Water:


					break;
				case GamaManager.Element.Fire:
					break;
				case GamaManager.Element.Dirt:
					break;
				case GamaManager.Element.Wind:
					break;
				default:
					break;
			}

			
		}
	}

	void SkillDown() {
		player.ElementState = GamaManager.Element.None;
		lastSkillTime = Time.time;
		effect.SetActive(true);
	}

	public void OffSkill() {
		effect.SetActive(false);
		gameObject.SetActive(false);
		player.ElementState = GamaManager.Element.None;
	}

	private void Update() {
		if (Time.time - lastSkillTime > duringTime) {
			OffSkill();
		}
		transform.position = player.transform.position;
	}
}
