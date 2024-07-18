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
        // 카메라의 정면 방향으로 레이를 발사합니다.
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastDistance, layerMask))
        {
            // 레이가 어떤 오브젝트에 닿았는지 확인합니다.
            GameObject hitObject = hit.collider.gameObject;

            // 해당 오브젝트에 Outline 스크립트가 있다면 그것을 활성화합니다.
            currentOutline = hitObject.GetComponent<Outline>();
            if (currentOutline != null)
            {
                currentOutline.enabled = true;
            }

            // 오브젝트의 태그가 "Game"인 경우 gameObjectToActivate를 활성화합니다.
            if (hitObject.CompareTag("Game"))
            {
                gameObjectToActivate.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else
        {
            // 레이가 아무 오브젝트에도 닿지 않은 경우, 현재 Outline 스크립트를 비활성화합니다.
            if (currentOutline != null)
            {
                currentOutline.enabled = false;
                currentOutline = null;
            }
        }
    }
}
