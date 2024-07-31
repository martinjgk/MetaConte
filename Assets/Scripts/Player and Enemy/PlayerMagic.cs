using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMagic : MonoBehaviour
{
	Player player;
	InputSignLang inputSignLang;
	UIManager ui;

	public float damage;

	[SerializeField]
	private string current_skill;
	
	public string CurrentSkill {
		get {
			return current_skill;
		}
		set {
			if (current_skill != value) {
				current_skill = value;
			}
		}
	}

	private GameObject currentSkillObj;

	[SerializeField]
	SerializableDictionary<string, GameObject> skillDict;

	[SerializeField]
	SerializableDictionary<string, float> skillCoolDict;

	Dictionary<string, float> lastSkillTimeDict = new Dictionary<string, float>()
	{
		{"water", 0f},
		{"fire", 0f},
		{"dirt", 0f},
		{"wind", 0f},
		{"down", 0f},
		{"flow", 0f},
		{"punch", 0f},
		{"hell", 0f},
		{"scatter", 0f}
	};
	[SerializeField]
	List<string> learnedSkills;

	public List<string> usableSkills;
    void Start()
    {
		ui = FindObjectOfType<UIManager>();
		// learnedSkills = new List<string>();
		usableSkills = new List<string>();
		player = GetComponent<Player>();
		damage = player.atk;
		inputSignLang = FindObjectOfType<InputSignLang>();
		SetUsableSkillElement();
	}

    // Update is called once per frame
    void Update()
    {
        if (player.MP > 10.0f)
        {
			if (Input.GetKey(KeyCode.Alpha1) || inputSignLang.inputSign == "water") {
				CastSkill("water");
			}
			if (Input.GetKey(KeyCode.Alpha2) || inputSignLang.inputSign == "fire") {
				CastSkill("fire");
			}
		}

		if(Input.GetKey(KeyCode.E) || inputSignLang.inputSign == "down") {
			CastSkill("down");
		}
		if(Input.GetKey(KeyCode.Q) || inputSignLang.inputSign == "flow") {
			CastSkill("flow");
		}
		if (Input.GetKey(KeyCode.R) || inputSignLang.inputSign == "hell") {
			CastSkill("hell");
		}
		ui.SetSkillDialog(current_skill, usableSkills);
    }

	void CastSkill(string skillName) {
		GameObject skill = skillDict[skillName];
		if(skill != null && learnedSkills.Contains(skillName) && usableSkills.Contains(skillName)) {

			if(Time.time - lastSkillTimeDict[skillName] >= skillCoolDict[skillName]) {
				usableSkills.Clear();
				Magic skillMagic = skill.GetComponent<Magic>();

				foreach (string nextSkillName in skillMagic.nextMagic) {
					if (learnedSkills.Contains(nextSkillName)) {
						usableSkills.Add(nextSkillName);
					}
				}
				if (CurrentSkill != "None" && currentSkillObj != null) {
					Destroy(currentSkillObj);
				}
				currentSkillObj = Instantiate(skill, transform.position, transform.rotation);

			}
		}
	}

	public void AddSkill(string skillName) {
		GameObject newSkill = skillDict[skillName];

		if(newSkill != null && !learnedSkills.Contains(skillName)) {

			learnedSkills.Add(skillName);
		}
	}

	public void UpdateLastSkillTime(string skillName) {
		lastSkillTimeDict[skillName] = Time.time;
	}

	public void SetUsableSkillElement () {
		usableSkills.Clear();
		currentSkillObj = null;
		if(learnedSkills.Contains("water")) {
			usableSkills.Add("water");
		}
		if (learnedSkills.Contains("fire")) {
			usableSkills.Add("fire");
		}
		if (learnedSkills.Contains("dirt")) {
			usableSkills.Add("dirt");
		}
		if (learnedSkills.Contains("wind")) {
			usableSkills.Add("wind");
		}
	}

	IEnumerator MPRegenerator() {
		while(true) {
			if (currentSkillObj == null) {
				player.MP += player.mpRecoverAmount;
			}
			else if (current_skill == "water" || current_skill == "fire" || current_skill == "dirt" || current_skill == "wind") {
				player.MP -= player.mpReduceAmount;
			}
			yield return new WaitForSeconds(player.mpRecoverT);
		}
	}
}
