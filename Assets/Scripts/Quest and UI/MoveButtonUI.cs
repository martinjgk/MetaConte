using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveButtonUI : MonoBehaviour
{
	[SerializeField]
	PlayerMovement playerMovement;

	Transform player;
    // Start is called before the first frame update
    void Start()
    {
		// playerMovement = FindAnyObjectByType<PlayerMovement>();
		player = playerMovement.transform;
    }

	public void ClickVertical(float dz) {
		Debug.Log("vertical" + dz.ToString());
		player.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
		playerMovement.vrVertic = dz;
	}
	public void ClickHorizontal(float dx) {
        Debug.Log("horizontal");
        player.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
        playerMovement.vrHorizon = dx;
	}
	public void ClickJump(bool jump) {
        Debug.Log("jump");
		playerMovement.vrJump = jump;
	}
}
