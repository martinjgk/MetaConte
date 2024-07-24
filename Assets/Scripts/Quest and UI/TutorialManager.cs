using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
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
	[TextArea]
	List<string> npcText;

	int inputStep = 0;
	int dialogStep = -1;
    // Start is called before the first frame update
    void Start()
    {
        teacherAnimator.SetBool("Attack", false);
		eyelidAnimator.SetTrigger("blink");
		dialog.text = npcText[0];
		
    }

    // Update is called once per frame
    void Update()
    {
		if (eyelidAnimator.GetCurrentAnimatorStateInfo(0).IsName("animation_lids_idle")) {
			dialogStep = 0;
		}
		if (dialogStep >= 0 && Input.GetKeyDown(KeyCode.N)) {
			dialogStep++;
			if (dialogStep == npcText.Count) {
				
			}
			dialog.text = npcText[dialogStep];
		}
    }

	IEnumerator DragonAttackAnimation() {
		while (true) {

			yield return new WaitForSeconds(0.1f);
		}
	}
}
