using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rideChangePos : MonoBehaviour
{
    public float interactDistance = 3f; // ��ȣ�ۿ� ������ �ִ� �Ÿ�
    private Transform originalParent; // ������ �θ� ������Ʈ ����
    private Quaternion originalRotation; // ������ ȸ�� ���� ����
    private Ride currentRide; // ���� ž�� ���� ���̱ⱸ

    public LayerMask interactLayer; // ��ȣ�ۿ� ������ ���̾� ����ũ

    private bool isSeated = false; // �÷��̾ ���̱ⱸ�� ž�� ������ ����

    void Start()
    {
        originalParent = transform.parent; // ���� �θ� ������Ʈ ����
        originalRotation = transform.rotation; // ���� ȸ�� ���� ����
    }

    void Update()
    {
        // F Ű �Է� üũ
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isSeated)
            {
                // ���̱ⱸ���� ������
                ExitRide();
            }
            else
            {
                // ���̱ⱸ�� ž�� �õ�
                TryInteractWithRide();
            }
        }

        // ž�� ���� ��� �÷��̾ seatPosition�� ����
        if (isSeated)
        {
            transform.localPosition = Vector3.zero;
            //transform.localRotation = Quaternion.identity;
        }
    }

    void TryInteractWithRide()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.forward * interactDistance, Color.red, 2f); // ����� ���� �׸���

        // Raycast�� ����Ͽ� ���̱ⱸ���� ��ȣ�ۿ� üũ
        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {     
            Ride ride = hit.collider.GetComponent<Ride>();
            if (ride != null)
            {
                ///Debug.Log("Interacting with Ride: " + ride.name); // ����� �޽��� ���

                // �÷��̾ ���� ��ġ�� �̵� (���� ������ ���)
                transform.position = ride.seatPosition.position;
                transform.rotation = ride.seatPosition.rotation;

                // �÷��̾ ���̱ⱸ�� �ڽ� ������Ʈ�� ����
                transform.SetParent(ride.seatPosition);

                isSeated = true; // ž�� ���·� ����
                currentRide = ride; // ���� ž�� ���� ���̱ⱸ ����
                //Debug.Log("Player moved to seat position."); // ����� �޽��� ���
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any objects."); // ����� �޽��� ���
        }
    }

    void ExitRide()
    {
        // �÷��̾ ���� �θ�� ����
        transform.SetParent(originalParent);

        // �÷��̾��� ��ġ�� ���̱ⱸ���� ������ ��ġ�� ���� (��: ���� ��ġ ��ó)
        transform.position += transform.forward * 2f; // ���� ��ġ���� �ణ ������ �̵�

        // ���� ȸ�� ������ �ǵ�����
        transform.rotation = originalRotation;

        isSeated = false; // ž�� ���� ����
        currentRide = null; // ���� ž�� ���� ���̱ⱸ �ʱ�ȭ
        //Debug.Log("Player exited the ride."); // ����� �޽��� ���
    }
}
