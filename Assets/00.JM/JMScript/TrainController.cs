using UnityEngine;

public class TrainController : MonoBehaviour
{
    public Transform[] waypoints; // �̵��� ����Ʈ���� Transform �迭
    public float speed = 5f; // ������ �̵� �ӵ�
    private float t = 0f; // ���� ����� ��ġ�� ��Ÿ���� �Ű�����
    private int currentWaypointIndex = 0; // ���� ��ǥ�� �ϴ� ����Ʈ�� �ε���

    void Update()
    {
        if (waypoints.Length < 4) return;

        // ���� ���׸�Ʈ�� ����Ʈ�� ����
        Vector3 p0 = waypoints[GetWrappedIndex(currentWaypointIndex - 1)].position;
        Vector3 p1 = waypoints[currentWaypointIndex].position;
        Vector3 p2 = waypoints[GetWrappedIndex(currentWaypointIndex + 1)].position;
        Vector3 p3 = waypoints[GetWrappedIndex(currentWaypointIndex + 2)].position;

        // Catmull-Rom ���ö����� ����Ͽ� � ���
        Vector3 newPosition = CatmullRomSpline(p0, p1, p2, p3, t);

        // ���� �̵�
        transform.position = newPosition;

        // ���� ȸ��
        Vector3 targetDirection = CatmullRomSpline(p0, p1, p2, p3, t + 0.01f) - newPosition;
        if (targetDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * speed);
        }

        // t �� ������Ʈ
        t += speed * Time.deltaTime / Vector3.Distance(p1, p2);

        // t ���� 1 �̻��̸� ���� ���׸�Ʈ�� �̵�
        if (t >= 1f)
        {
            t = 0f;
            currentWaypointIndex = GetWrappedIndex(currentWaypointIndex + 1);
        }
    }

    int GetWrappedIndex(int index)
    {
        // �ε����� 0�� �迭 ���� ���̷� ����
        return (index + waypoints.Length) % waypoints.Length;
    }

    Vector3 CatmullRomSpline(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // Catmull-Rom ���ö����� ����Ͽ� � ���
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * ((2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t3);
    }
}
