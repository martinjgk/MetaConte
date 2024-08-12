using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonUI : MonoBehaviour
{
	[SerializeField]
	Button forwardButton;
	[SerializeField]
	Button backButton;
	[SerializeField]
	Button rightButton;
	[SerializeField]
	Button leftButton;
	[SerializeField]
	Button jumpButton;

	PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

	public void ClickVertical(float dz) {
		playerMovement.SetVertical(dz);
	}
	public void ClickHorizontal(float dx) {
		playerMovement.SetHorizontal(dx);
	}
	public void ClickJump(bool jump) {
		playerMovement.SetJump(jump);
	}
}
