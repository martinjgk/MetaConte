using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    float speed = 7f;
    [SerializeField]
    float runSpeed = 15f;
    [SerializeField]
    float jumpPower = 10f;
    [SerializeField]
    float gravity = 15f;


    [SerializeField]
    Camera cam;
    [SerializeField]
    float lookSpeed = 2f;
    [SerializeField]
    float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotX = 0;
    CharacterController characterController;

	bool isCursurOn = false;

    public bool canMove = true;


    private void Start() {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = isCursurOn;
    }

	private void Update() {
		characterController.Move(moveDirection * Time.deltaTime);
		Move();
		Gravity();
		Rotate();
		
		if(Input.GetKeyUp(KeyCode.LeftAlt)) {
			isCursurOn = !isCursurOn;
			Cursor.lockState = 1 - Cursor.lockState;
		}
		Cursor.visible = isCursurOn;
	}

	void Move() {
		float dx = Input.GetAxis("Horizontal");
		float dz = Input.GetAxis("Vertical");
		bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float spdX = canMove ? (isRunning ? runSpeed : speed) * dx : 0;
        float spdZ = canMove ? (isRunning ? runSpeed : speed) * dz : 0;
		float moveDirectionY = moveDirection.y;

        moveDirection = (transform.TransformDirection(Vector3.right) * spdX) + (transform.TransformDirection(Vector3.forward) * spdZ);
		if (canMove && characterController.isGrounded && Input.GetKey(KeyCode.Space)) {
			moveDirection.y = jumpPower;
		}
		else {
			moveDirection.y = moveDirectionY;
		}
	}


    void Gravity() {
        if (!characterController.isGrounded) {
            moveDirection.y -= gravity * Time.deltaTime;
        }
    }

	void Rotate() {
		if(canMove) {
			rotX += -Input.GetAxis("Mouse Y") * lookSpeed;
			rotX = Mathf.Clamp(rotX, -lookXLimit, lookXLimit);
			cam.transform.localRotation = Quaternion.Euler(rotX, 0, 0);
			transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
		}
	}

}
