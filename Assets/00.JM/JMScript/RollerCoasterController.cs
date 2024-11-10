using UnityEngine;
using System.Collections;

public class RollerCoasterController : MonoBehaviour
{
    public Transform[] waypoints; // ����� ����Ʈ��
    public float baseSpeed = 5f; // �ʱ� �ӵ�
    private float currentSpeed; // ���� �ӵ�
    private float t = 0f; // ����� ��ġ�� ��Ÿ���� �Ű�����
    private int currentWaypointIndex = 0; // ���� ��ǥ ����Ʈ�� �ε���
    private bool isStopped = false; // ������ ���� �������� ����
    private bool returningToStart = false; // ���������� ���ư��� �������� ����

    void Start()
    {
        // ���� ��ġ�� ���� ����
        if (waypoints.Length >= 2)
        {
            transform.position = waypoints[0].position;
            Vector3 initialDirection = waypoints[1].position - waypoints[0].position;
            if (initialDirection != Vector3.zero)
            {
                Quaternion initialRotation = Quaternion.LookRotation(initialDirection);
                transform.rotation = initialRotation * Quaternion.Euler(0, -90, 0); // Y���� �������� -90�� ȸ��
            }
        }

        currentSpeed = baseSpeed; // �ʱ� �ӵ��� ����
        StartCoroutine(RollerCoasterRoutine());
    }

    IEnumerator RollerCoasterRoutine()
    {
        while (true)
        {
            // �ӵ� ����
            SetSpeed();

            if (!isStopped)
            {
                if (returningToStart)
                {
                    // ������ ��������Ʈ���� ù ��° ��������Ʈ�� �̵� ó��
                    Vector3 newPosition = Vector3.MoveTowards(transform.position, waypoints[0].position, currentSpeed * Time.deltaTime);
                    transform.position = newPosition;

                    Vector3 targetDirection = waypoints[0].position - transform.position;
                    if (targetDirection != Vector3.zero)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                        transform.rotation = targetRotation * Quaternion.Euler(0, -90, 0); // Y���� �������� -90�� ȸ��
                    }

                    if (Vector3.Distance(transform.position, waypoints[0].position) < 0.1f)
                    {
                        returningToStart = false;
                        currentWaypointIndex = 0;
                        t = 0f;
                    }
                }
                else
                {
                    // ���� ���׸�Ʈ�� ����Ʈ�� ����
                    Vector3 p0 = waypoints[GetWrappedIndex(currentWaypointIndex - 1)].position;
                    Vector3 p1 = waypoints[currentWaypointIndex].position;
                    Vector3 p2 = waypoints[GetWrappedIndex(currentWaypointIndex + 1)].position;
                    Vector3 p3 = waypoints[GetWrappedIndex(currentWaypointIndex + 2)].position;

                    // Catmull-Rom Spline�� ����Ͽ� � ���
                    Vector3 newPosition = CatmullRomSpline(p0, p1, p2, p3, t);

                    // ���� �̵�
                    transform.position = newPosition;

                    // ���� ȸ��
                    Vector3 targetDirection = CatmullRomSpline(p0, p1, p2, p3, t + 0.01f) - newPosition;
                    if (targetDirection != Vector3.zero)
                    {
                        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                        transform.rotation = targetRotation * Quaternion.Euler(0, -90, 0); // Y���� �������� -90�� ȸ��
                    }

                    // t �� ������Ʈ
                    t += currentSpeed * Time.deltaTime / Vector3.Distance(p1, p2);

                    // t ���� 1 �̻��̸� ���� ���׸�Ʈ�� �̵�
                    if (t >= 1f)
                    {
                        t = 0f;
                        currentWaypointIndex = GetWrappedIndex(currentWaypointIndex + 1);
                    }

                    // ������ ��������Ʈ�� �����ϸ� ����
                    if (currentWaypointIndex == waypoints.Length - 1)
                    {
                        isStopped = true;
                        yield return new WaitForSeconds(5f); // 5�� ����
                        returningToStart = true;
                        isStopped = false;
                    }
                }
            }
            yield return null;
        }
    }

    //�����ϰ� �ӵ� ����
    void SetSpeed()
    {
        if (returningToStart)
        {
            currentSpeed = baseSpeed;
            return;
        }

        if (currentWaypointIndex <= 15)
        {
            currentSpeed = Mathf.Lerp(5f, 15f, currentWaypointIndex / 15f);
        }
        else if (currentWaypointIndex <= 27)
        {
            currentSpeed = Mathf.Lerp(15f, 30f, (currentWaypointIndex - 15) / 12f);
        }
        else if (currentWaypointIndex <= 34)
        {
            currentSpeed = Mathf.Lerp(30f, 5f, (currentWaypointIndex - 27) / 7f);
        }
        else if (currentWaypointIndex <= 38)
        {
            currentSpeed = Mathf.Lerp(5f, 25f, (currentWaypointIndex - 34) / 4f);
        }
        else if (currentWaypointIndex <= 48)
        {
            currentSpeed = Mathf.Lerp(25f, 45f, (currentWaypointIndex - 38) / 10f);
        }
        else if (currentWaypointIndex <= 56)
        {
            currentSpeed = Mathf.Lerp(45f, 0f, (currentWaypointIndex - 48) / 8f);
        }
    }

    int GetWrappedIndex(int index)
    {
        // �ε����� 0�� �迭 ���� ���̷� ����
        return (index + waypoints.Length) % waypoints.Length;
    }

    Vector3 CatmullRomSpline(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        // Catmull-Rom Spline�� ����Ͽ� � ���
        float t2 = t * t;
        float t3 = t2 * t;

        return 0.5f * ((2f * p1) +
            (-p0 + p2) * t +
            (2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
            (-p0 + 3f * p1 - 3f * p2 + p3) * t3);
    }
}
