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
	

	List<GameObject> usableSkills;
    void Start()
    {
		usableSkills = new List<GameObject>();
		
	}


	void InitMagics() {
		
	}
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Alpha1)) {
			CastSkill("water");
		}
		if(Input.GetKey(KeyCode.E)) {
			CastSkill("down");
		}
    }

	void CastSkill(string skillName) {
		Magic skill = skillDict[skillName].GetComponent<Magic>();
		if(skill != null && usableSkills.Contains(skill.gameObject)) {
			if(Time.time - skill.lastSkillTime >= skill.coolTime) {
				skill.gameObject.SetActive(true);
				skill.UseSkill();
			}
		}
	}

	public void AddSkill(string skillName) {
		GameObject newSkill = skillDict[skillName];

		if(newSkill != null && !usableSkills.Contains(newSkill)) {
			
			usableSkills.Add(newSkill);
		}
	}
}
