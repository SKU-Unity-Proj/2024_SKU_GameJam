using UnityEngine;

public class FerrisWheelController : MonoBehaviour
{
    public Transform ferrisWheel; // �������� Transform
    public Transform centerPivot; // �ν� ȸ���� ���� �߽� �� Transform
    public Transform[] cabins;    // �ν����� Transform �迭
    public float rotationSpeed = 10f; // ȸ�� �ӵ�

    private Vector3[] initialCabinPositions;

    void Start()
    {
        // �� �ν��� �ʱ� ��ġ�� �����մϴ�.
        initialCabinPositions = new Vector3[cabins.Length];
        for (int i = 0; i < cabins.Length; i++)
        {
            // �ν��� �ʱ� ��ġ�� �߽� �����κ��� ������� ��ġ�� ����
            initialCabinPositions[i] = centerPivot.InverseTransformPoint(cabins[i].position);
        }
    }

    void Update()
    {
        // ������ ȸ��
        ferrisWheel.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // �� �ν��� ȸ����Ű�� ����
        for (int i = 0; i < cabins.Length; i++)
        {
            Transform cabin = cabins[i];

            // �ν��� �ʱ� ��ġ���� �߽� ���� �������� ȸ��
            Vector3 rotatedPosition = Quaternion.Euler(0, 0, rotationSpeed * Time.deltaTime) * initialCabinPositions[i];

            // ���ο� ��ġ ����
            cabin.position = centerPivot.TransformPoint(rotatedPosition);

            // �ν��� �׻� ������ �����ϵ��� ȸ��
            Vector3 directionToCenter = centerPivot.position - cabin.position;
            cabin.rotation = Quaternion.LookRotation(Vector3.forward, directionToCenter);
        }
    }
}
