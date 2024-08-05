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
	protected List<string> effectNameList;

	[SerializeField]
	protected List<GameObject> effectList;


	[SerializeField]
	public List<string> nextMagic;

	protected float skillOnTime;
	protected PlayerMagic player=null;

	public virtual void UseSkill() {
		skillOnTime = Time.time;
		player.CurrentSkill = skillName;
	}

	protected virtual void OffSkill() {
		player.CurrentSkill = "None";
		player.SetUsableSkillElement();
		Destroy(gameObject);
	}


	public GamaManager.Element GetElement() {
		return element;
	}
	public void SetElement(GamaManager.Element element) {
		this.element = element;
	}

	private void Update() {
		if (player != null) {
			player.UpdateLastSkillTime(name);
		}
	}
}
