using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingController : MonoBehaviour
{
    public Transform merryGoRound; // ���̱ⱸ�� Transform
    public float rotationSpeed = 30f; // ȸ�� �ӵ�

    void Update()
    {
        // ���̱ⱸ ȸ��
        merryGoRound.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
