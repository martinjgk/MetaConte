using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMagic : MonoBehaviour
{
	Player player;

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
	List<string> learnedSkills;
	public List<string> usableSkills;
    void Start()
    {
		learnedSkills = new List<string>();
		usableSkills = new List<string>();
	}

    // Update is called once per frame
    void Update()
    {
		if(player.CurrentSkill == "None") {
			
		}
        if(Input.GetKey(KeyCode.Alpha1)) {
			CastSkill("water");
		}
		if(Input.GetKey(KeyCode.E)) {
			CastSkill("down");
		}
		if(Input.GetKey(KeyCode.Alpha2)) {
			CastSkill("fire");
		}
    }

	void CastSkill(string skillName) {
		GameObject skill = skillDict[skillName];
		if(skill != null && learnedSkills.Contains(skillName)) {
			if(Time.time - lastSkillTimeDict[skillName] >= skillCoolDict[skillName]) {
				usableSkills.Clear();
				Magic skillMagic = skill.GetComponent<Magic>();

				foreach (string nextSkillName in skillMagic.nextMagic) {
					if (learnedSkills.Contains(nextSkillName)) {
						usableSkills.Add(nextSkillName);
					}
				}
				Instantiate(skill);
				lastSkillTimeDict[skillName] = Time.time;
			}
		}
	}

	public void AddSkill(string skillName) {
		GameObject newSkill = skillDict[skillName];

		if(newSkill != null && !learnedSkills.Contains(skillName)) {

			learnedSkills.Add(skillName);
		}
	}
}
