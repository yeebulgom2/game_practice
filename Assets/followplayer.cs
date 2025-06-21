using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraFollow : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public Vector3 offset;   // ī�޶�� �÷��̾� ������ �Ÿ�
    public float smoothSpeed = 0.125f; // �ε巯�� �̵� �ӵ�

    public float minX;
    public float maxX   ;

    private float cameraHalfWidth;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        cameraHalfWidth = cam.orthographicSize * cam.aspect; // ī�޶� ���� �ʺ� ���
    }

    void LateUpdate()
    {
        if (player != null)
        {

            // ��ǥ ��ġ ���
            Vector3 desiredPosition = player.position + offset;

            // x�� ���� ���� (ī�޶� �߽� ����)
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX + cameraHalfWidth, maxX - cameraHalfWidth);

            // ī�޶��� ��ġ�� �ε巴�� ����
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // ī�޶� ��ġ ����
            transform.position = smoothedPosition;
        }
    }
}
