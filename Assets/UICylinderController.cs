using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICylinderController : MonoBehaviour
{
    public Transform playerCamera; // 플레이어의 카메라
    public float distanceFromCamera = 2.0f; // 카메라와의 거리

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main.transform; // 메인 카메라를 기본값으로 설정
        }
    }

    void Update()
    {
        // UICylinder의 위치를 카메라 앞에 고정
        Vector3 newPosition = playerCamera.position + playerCamera.forward * distanceFromCamera;
        transform.position = newPosition;

        // UICylinder가 항상 카메라를 바라보도록 설정
        transform.LookAt(playerCamera);
        transform.Rotate(0, 180f, 0); // UICylinder가 뒤집히지 않도록 회전
    }
}
