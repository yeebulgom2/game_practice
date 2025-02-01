using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Vector3 offset;   // 카메라와 플레이어 사이의 거리
    public float smoothSpeed = 0.125f; // 부드러운 이동 속도

    void LateUpdate()
    {
        if (player != null)
        {
            // 목표 위치 계산
            Vector3 desiredPosition = player.position + offset;

            // 카메라의 위치를 부드럽게 보간
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 카메라 위치 설정
            transform.position = smoothedPosition;
        }
    }
}
