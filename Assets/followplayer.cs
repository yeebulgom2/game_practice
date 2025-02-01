using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform player; // �÷��̾��� Transform
    public Vector3 offset;   // ī�޶�� �÷��̾� ������ �Ÿ�
    public float smoothSpeed = 0.125f; // �ε巯�� �̵� �ӵ�

    void LateUpdate()
    {
        if (player != null)
        {
            // ��ǥ ��ġ ���
            Vector3 desiredPosition = player.position + offset;

            // ī�޶��� ��ġ�� �ε巴�� ����
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // ī�޶� ��ġ ����
            transform.position = smoothedPosition;
        }
    }
}
