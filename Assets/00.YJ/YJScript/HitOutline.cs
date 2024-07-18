using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOutline : MonoBehaviour
{
    public Camera mainCamera;
    public LayerMask layerMask;
    private float raycastDistance = 50f;
    private Outline currentOutline;
    public GameObject gameObjectToActivate;

    void Update()
    {
        // ī�޶��� ���� �������� ���̸� �߻��մϴ�.
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, layerMask))
        {
            // ���̰� � ������Ʈ�� ��Ҵ��� Ȯ���մϴ�.
            GameObject hitObject = hit.collider.gameObject;

            // �ش� ������Ʈ�� Outline ��ũ��Ʈ�� �ִٸ� �װ��� Ȱ��ȭ�մϴ�.
            currentOutline = hitObject.GetComponent<Outline>();
            if (currentOutline != null)
            {
                currentOutline.enabled = true;
            }

            // ������Ʈ�� �±װ� "Game"�� ��� gameObjectToActivate�� Ȱ��ȭ�մϴ�.
            if (hitObject.CompareTag("Game"))
            {
                gameObjectToActivate.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {
            // ���̰� �ƹ� ������Ʈ���� ���� ���� ���, ���� Outline ��ũ��Ʈ�� ��Ȱ��ȭ�մϴ�.
            if (currentOutline != null)
            {
                currentOutline.enabled = false;
                currentOutline = null;
            }
        }
    }
}
