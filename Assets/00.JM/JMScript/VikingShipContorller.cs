using UnityEngine;

public class VikingShipController : MonoBehaviour
{
    public Transform pivot; // ����ŷ �߽��� Transform
    public float amplitude = 30f; // �ִ� ��鸲 ����
    public float speed = 1f; // ��鸲 �ӵ�

    void Update()
    {
        // ���� �Լ��� ����Ͽ� ������ ���
        float angle = amplitude * Mathf.Sin(Time.time * speed);

        // �߽����� �������� ȸ��
        pivot.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
