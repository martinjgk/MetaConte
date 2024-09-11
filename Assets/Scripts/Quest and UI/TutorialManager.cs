using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {
	[SerializeField]
	Animator dragonAnimator;
	[SerializeField]
	Animator teacherAnimator;
	[SerializeField]
	Animator eyelidAnimator;

	[SerializeField]
	InputSignLang inputs;

	[SerializeField]
	Text dialog;
	[SerializeField]
	GameObject dialogPanel;


	[SerializeField]
	[TextArea]
	List<string> npcText;


	[SerializeField]
	Text currentSkillText;

	[SerializeField]
	Image currentSkillImage;

	[SerializeField]
	Text nextSkillText;

	[SerializeField]
	VideoPlayer nextSkillVideo;

	[SerializeField]
	VideoClip waterVideo;
	[SerializeField]
	VideoClip downVideo;

	[SerializeField]
	Sprite waterImage;
	[SerializeField]
	Sprite downWaterImage;

	[SerializeField]
	GameObject ultimateSkill;

	[SerializeField]
	Image fadeOut;

	[SerializeField]
	GameObject waterEffect;

	[SerializeField]
	GameObject rainEffect;

	[SerializeField]
	BGMManager bGMManager;

	[SerializeField]
	AudioClip clip;

	int inputStep = -1;
	int dialogStep = -1;

	bool inputDone = true;

	int dragonAnimDir = 1;

	bool clickNext = false;
	bool clickFirst = false;
	// Start is called before the first frame update
	void Start() {
		waterEffect.SetActive(false);
		rainEffect.SetActive(false);
		fadeOut.gameObject.SetActive(false);
		ultimateSkill.SetActive(false);
		dialogPanel.SetActive(false);
		teacherAnimator.SetBool("Attack", false);
		// eyelidAnimator.SetTrigger("blink");
		dialog.gameObject.SetActive(false);
		StartCoroutine(DragonAttackAnimation());
		currentSkillImage.gameObject.transform.parent.parent.parent.parent.parent.gameObject.SetActive(false);
		currentSkillImage.gameObject.transform.parent.parent.parent.gameObject.SetActive(false);
		nextSkillText.gameObject.transform.parent.gameObject.SetActive(false);
	}

	public void OnNextClick() {
		clickNext = true;
	}

	public void OnFirstSkillClick() {
		clickNext = true;
	}

	// Update is called once per frame
	void Update() {
		if (dialogStep < 0) {
			dialogStep = 0;
			dialogPanel.SetActive(true);
			dialog.text = npcText[dialogStep];
			dialog.gameObject.SetActive(true);
		}
		if (inputDone && dialogStep >= 0 && (Input.GetKeyDown(KeyCode.N) || clickNext)) {
			clickNext = false;
			dialogStep++;
			if (dialogStep == 7) {
				inputDone = false;
				ultimateSkill.SetActive(true);
				currentSkillImage.gameObject.transform.parent.parent.parent.parent.parent.gameObject.SetActive(true);
				nextSkillText.gameObject.transform.parent.gameObject.SetActive(true);

				nextSkillText.text = "물";
				nextSkillVideo.clip = waterVideo;
			}
			else if (dialogStep == 11) {
				inputDone = false;
				nextSkillText.gameObject.transform.parent.gameObject.SetActive(true);

				nextSkillText.text = "내리다";
				nextSkillVideo.clip = downVideo;
			}
			else if (dialogStep == 13) {
				inputDone = false;
				StartCoroutine(EndTutorial());
			}
			dialog.text = npcText[dialogStep];
		}

		if (!inputDone) {
			if (dialogStep == 7) {
				if (inputs.inputSign == "water" || Input.GetKey(KeyCode.Alpha1) || clickFirst) {
					clickFirst = false;
					inputDone = true;
					dialogStep++;
					dialog.text = npcText[dialogStep];
					currentSkillImage.gameObject.transform.parent.parent.parent.gameObject.SetActive(true);
					currentSkillImage.sprite = waterImage;
					currentSkillText.text = "물";
					waterEffect.SetActive(true);
					nextSkillText.gameObject.transform.parent.gameObject.SetActive(false);
				}
			}
			else if (dialogStep == 11) {
				if (inputs.inputSign == "down" || Input.GetKey(KeyCode.E) || clickFirst) {
					clickFirst = false;
					inputDone = true;
					dialogStep++;
					dialog.text = npcText[dialogStep];
					currentSkillImage.gameObject.transform.parent.parent.parent.gameObject.SetActive(true);
					currentSkillImage.sprite = downWaterImage;
					currentSkillText.text = "비";
					waterEffect.SetActive(false);
					rainEffect.SetActive(true);
					nextSkillText.gameObject.transform.parent.gameObject.SetActive(false);
				}
			}
		}
	}


	IEnumerator EndTutorial() {
		StartCoroutine(FadeOut());
		yield return new WaitForSeconds(4);
		
		SceneManager.LoadScene("WaterTestField");
	}

	IEnumerator FadeOut() {
		fadeOut.gameObject.SetActive(true);
		while (true) {
			fadeOut.color = new Color(255, 255, 255, fadeOut.color.a + (1f / 255f));
			yield return new WaitForSeconds(2f/255f);
		}
	}

	IEnumerator DragonAttackAnimation() {
		while (true) {
			dragonAnimator.SetFloat("attackType", dragonAnimator.GetFloat("attackType") + dragonAnimDir * 0.02f);
			if (dragonAnimator.GetFloat("attackType") >= 1.8f) {
				dragonAnimDir = -1;
			}
			else if (dragonAnimator.GetFloat("attackType") <= 0.2f) {
				dragonAnimDir = 1;
			}
			yield return new WaitForSeconds(Time.deltaTime * 5f);
		}
	}
}
