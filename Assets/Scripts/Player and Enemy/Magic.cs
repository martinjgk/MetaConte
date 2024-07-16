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

	[SerializeField]
	protected SerializableDictionary<string, GameObject> effectDict;

	[SerializeField]
	public List<string> nextMagic;

	protected float skillOnTime;
	protected PlayerMagic playerSkill;

	public void Init() {
		playerSkill = GameObject.Find("Player").GetComponent<PlayerMagic>();
	}

	public abstract void UseSkill();

	public GamaManager.Element GetElement() {
		return element;
	}
	public void SetElement(GamaManager.Element element) {
		this.element = element;
	}
}
