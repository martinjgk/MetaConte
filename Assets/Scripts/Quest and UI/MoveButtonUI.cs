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

	[SerializeField]
	PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        // playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

	public void ClickVertical(float dz) {
		Debug.Log("vertical" + dz.ToString());
		playerMovement.vrVertic = dz;
	}
	public void ClickHorizontal(float dx) {
        Debug.Log("horizontal");
		playerMovement.vrHorizon = dx;
	}
	public void ClickJump(bool jump) {
        Debug.Log("jump");
		playerMovement.vrJump = jump;
	}
}
