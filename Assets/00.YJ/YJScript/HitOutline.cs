using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitOutline : MonoBehaviour
{
    private Camera mainCamera;
    public LayerMask layerMask;
    private float raycastDistance = 50f;
    private Outline currentOutline;

    private void Start()
    {
        mainCamera = Camera.main;
    }

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
