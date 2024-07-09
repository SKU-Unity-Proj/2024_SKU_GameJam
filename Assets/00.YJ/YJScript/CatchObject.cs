using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using FC;

public class CatchObject : MonoBehaviour
{
    public Camera mainCamera; // 카메라
    public string targetTag = "Target"; // 대상 오브젝트의 태그
    public string agentTag = "Agent"; // 대상 오브젝트의 태그
    public float fixedDistance = 5.0f; // 카메라 정면에서의 거리
    public float rayDistance = 100.0f; // 레이캐스트 거리
    public float scrollSpeed = 2.0f; // 마우스 휠로 조절할 때의 속도

    public LayerMask outlineMask = TagAndLayer.LayerMasking.Outline;

    public bool isGround = true; // 땅에 있는지 여부를 나타내는 변수

    private GameObject fixedObject = null; // 현재 고정된 오브젝트

    void Update()
    {
        // 마우스 휠 입력으로 거리 조절
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0)
        {
            fixedDistance += scroll * scrollSpeed;
            fixedDistance = Mathf.Clamp(fixedDistance, 1.0f, rayDistance); // 최소, 최대 거리 제한
        }

        if (Input.GetButtonDown(ButtonName.InteractObj))
        {
            if (fixedObject != null)
            {
                // 속도 초기화
                Rigidbody rb = fixedObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
                // 고정 해제
                fixedObject = null;
                isGround = true; // 고정 해제 시 isGround를 true로 설정
                Debug.Log("오브젝트 고정 해제");
            }
            else
            {
                // 레이캐스트 발사
                Vector3 rayOrigin = mainCamera.transform.position;
                Vector3 rayDirection = mainCamera.transform.forward;

                Ray ray = new Ray(rayOrigin, rayDirection);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, rayDistance, outlineMask))
                {
                    Debug.Log($"레이캐스트 히트! 히트 오브젝트 이름: {hit.collider.gameObject.name}");

                    if (hit.collider.CompareTag(targetTag))
                    {
                        fixedObject = hit.collider.gameObject;
                        //isGround = false; // 오브젝트 고정 시 isGround를 false로 설정
                        Debug.Log("대상 오브젝트 고정됨");
                    }
                    if (hit.collider.CompareTag(agentTag))
                    {
                        fixedObject = hit.collider.gameObject;
                        isGround = false; // 오브젝트 고정 시 isGround를 false로 설정
                        Debug.Log("대상 오브젝트 고정됨");
                    }
                    else
                    {
                        Debug.Log("히트된 오브젝트는 대상 태그가 아닙니다.");
                    }
                }
                else
                {
                    Debug.Log("레이캐스트 미스!");
                }
            }
        }

        // 오브젝트를 카메라 정면의 고정 위치에 고정
        if (fixedObject != null)
        {
            Vector3 fixedPosition = mainCamera.transform.position + mainCamera.transform.forward * fixedDistance;
            fixedObject.transform.position = fixedPosition;
            fixedObject.transform.rotation = mainCamera.transform.rotation;
        }

        Debug.DrawRay(mainCamera.transform.position, mainCamera.transform.forward * rayDistance, Color.red, 2f);
    }
}
