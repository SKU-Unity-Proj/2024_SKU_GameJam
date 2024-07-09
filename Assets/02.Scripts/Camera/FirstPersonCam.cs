using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FirstPersonCam : MonoBehaviour
{
    public Transform player;

    public float smooth = 10f;
    public float horizontalAimingSpeed = 6.0f;
    public float verticalAimingSpeed = 6.0f;

    // 각도 제한
    public float maxVerticalAngle = 30.0f;
    public float minVerticalAngle = -60.0f;

    public float recoilAngleBounce = 5.0f;
    private float angleH = 0.0f;
    private float angleV = 0.0f;


    public Transform CamPos;
    private Transform cameraTransform;
    private Camera myCamera;


    private float defaultFOV;
    private float targetFOV;
    private float targetMaxVerticalAngle;
    private float recoilAngle = 0f;

    public float GetH
    {
        get
        {
            return angleH;
        }
    }

    private void Awake()
    {
        cameraTransform = transform;
        myCamera = cameraTransform.GetComponent<Camera>();

        cameraTransform.position = CamPos.position;
        cameraTransform.rotation = Quaternion.identity;

        defaultFOV = myCamera.fieldOfView;
        angleH = player.eulerAngles.y;

        ResetFOV();
        ResetMaxAngle();
    }

    public void ResetFOV()
    {
        this.targetFOV = defaultFOV;
    }

    public void ResetMaxAngle()
    {
        targetMaxVerticalAngle = maxVerticalAngle;
    }

    public void BounceVertical(float degree)
    {
        recoilAngle = degree;
    }

    public void SetFOV(float customFOV)
    {
        this.targetFOV = customFOV;
    }

    private void Update()
    {
        angleH += Mathf.Clamp(Input.GetAxis("Mouse X"), -1f, 1f) * horizontalAimingSpeed;
        angleV += Mathf.Clamp(Input.GetAxis("Mouse Y"), -1f, 1f) * verticalAimingSpeed;
        angleV = Mathf.Clamp(angleV, minVerticalAngle, targetMaxVerticalAngle);
        angleV = Mathf.LerpAngle(angleV, angleV + recoilAngle, 10f * Time.deltaTime);

        Quaternion aimRotation = Quaternion.Euler(-angleV, angleH, 0.0f);
        cameraTransform.rotation = aimRotation;

        myCamera.fieldOfView = Mathf.Lerp(myCamera.fieldOfView, targetFOV, Time.deltaTime);

        // 플레이어의 이동 방향과 카메라의 바라보는 방향이 다르면 카메라가 우선적으로 이동하는 부분
        Vector3 cameraDirection = cameraTransform.forward;
        Vector3 playerDirection = player.forward;

        if (Vector3.Dot(cameraDirection, playerDirection) < 0.9f)
        {
            Vector3 newPosition = CamPos.position;
            cameraTransform.position = Vector3.Lerp(cameraTransform.position, newPosition, smooth * Time.deltaTime);
        }
        else
        {
            cameraTransform.position = CamPos.position;
        }

        if (recoilAngle > 0.0f)
        {
            recoilAngle -= recoilAngleBounce * Time.deltaTime;
        }

        else if (recoilAngle < 0.0f)
        {
            recoilAngle += recoilAngleBounce * Time.deltaTime;
        }
    }
}
