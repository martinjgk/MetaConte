using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Magic : MonoBehaviour
{
	public enum WordClass {
		Subject,
		Verb,
		Adj,
		Obj
	}
	[SerializeField]
	protected string skillName;
	[SerializeField]
	protected GamaManager.Element element;
	[SerializeField]
	protected WordClass wordClass;

	protected PlayerMagic playerSkill;

	public float lastSkillTime = 0f;
	[SerializeField]
	public float coolTime;

	public void Init() {
		playerSkill = GameObject.Find("Player").GetComponent<PlayerMagic>();
	}

	public abstract void UseSkill();

}
