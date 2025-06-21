using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraFollow : MonoBehaviour
{
    public Transform player; // 플레이어의 Transform
    public Vector3 offset;   // 카메라와 플레이어 사이의 거리
    public float smoothSpeed = 0.125f; // 부드러운 이동 속도

    public float minX;
    public float maxX   ;

    private float cameraHalfWidth;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cameraHalfWidth = cam.orthographicSize * cam.aspect; // 카메라 절반 너비 계산
    }

    void LateUpdate()
    {
        if (player != null)
        {

            // 목표 위치 계산
            Vector3 desiredPosition = player.position + offset;

            // x축 제한 적용 (카메라 중심 기준)
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX + cameraHalfWidth, maxX - cameraHalfWidth);

            // 카메라의 위치를 부드럽게 보간
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // 카메라 위치 설정
            transform.position = smoothedPosition;
        }
    }
}
