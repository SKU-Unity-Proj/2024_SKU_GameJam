using UnityEngine;

public class MerryGoRoundController : MonoBehaviour
{
    public Transform merryGoRound; // ���̱ⱸ�� Transform
    public float rotationSpeed = 10f; // ȸ�� �ӵ�

    void Update()
    {
        // ���̱ⱸ ȸ��
        merryGoRound.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }
}
