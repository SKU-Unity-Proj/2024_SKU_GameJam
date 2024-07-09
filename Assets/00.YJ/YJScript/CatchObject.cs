using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using FC;

public class CatchObject : MonoBehaviour
{
    public Camera mainCamera; // ī�޶�
    public string targetTag = "Target"; // ��� ������Ʈ�� �±�
    public string agentTag = "Agent"; // ��� ������Ʈ�� �±�
    public float fixedDistance = 5.0f; // ī�޶� ���鿡���� �Ÿ�
    public float rayDistance = 100.0f; // ����ĳ��Ʈ �Ÿ�
    public float scrollSpeed = 2.0f; // ���콺 �ٷ� ������ ���� �ӵ�

    public LayerMask outlineMask = TagAndLayer.LayerMasking.Outline;

    public bool isGround = true; // ���� �ִ��� ���θ� ��Ÿ���� ����

    private GameObject fixedObject = null; // ���� ������ ������Ʈ

    void Update()
    {
        // ���콺 �� �Է����� �Ÿ� ����
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            fixedDistance += scroll * scrollSpeed;
            fixedDistance = Mathf.Clamp(fixedDistance, 1.0f, rayDistance); // �ּ�, �ִ� �Ÿ� ����
        }

        if (Input.GetButtonDown(ButtonName.InteractObj))
        {
            if (fixedObject != null)
            {
                // �ӵ� �ʱ�ȭ
                Rigidbody rb = fixedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
                // ���� ����
                fixedObject = null;
                isGround = true; // ���� ���� �� isGround�� true�� ����
                Debug.Log("������Ʈ ���� ����");
            }
            else
            {
                // ����ĳ��Ʈ �߻�
                Vector3 rayOrigin = mainCamera.transform.position;
                Vector3 rayDirection = mainCamera.transform.forward;

                Ray ray = new Ray(rayOrigin, rayDirection);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, rayDistance, outlineMask))
                {
                    Debug.Log($"����ĳ��Ʈ ��Ʈ! ��Ʈ ������Ʈ �̸�: {hit.collider.gameObject.name}");

                    if (hit.collider.CompareTag(targetTag))
                    {
                        fixedObject = hit.collider.gameObject;
                        //isGround = false; // ������Ʈ ���� �� isGround�� false�� ����
                        Debug.Log("��� ������Ʈ ������");
                    }
                    if (hit.collider.CompareTag(agentTag))
                    {
                        fixedObject = hit.collider.gameObject;
                        isGround = false; // ������Ʈ ���� �� isGround�� false�� ����
                        Debug.Log("��� ������Ʈ ������");
                    }
                    else
                    {
                        Debug.Log("��Ʈ�� ������Ʈ�� ��� �±װ� �ƴմϴ�.");
                    }
                }
                else
                {
                    Debug.Log("����ĳ��Ʈ �̽�!");
                }
            }
        }

        // ������Ʈ�� ī�޶� ������ ���� ��ġ�� ����
        if (fixedObject != null)
        {
            Vector3 fixedPosition = mainCamera.transform.position + mainCamera.transform.forward * fixedDistance;
            fixedObject.transform.position = fixedPosition;
            fixedObject.transform.rotation = mainCamera.transform.rotation;
        }

        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * rayDistance, Color.red, 2f);
    }
}
