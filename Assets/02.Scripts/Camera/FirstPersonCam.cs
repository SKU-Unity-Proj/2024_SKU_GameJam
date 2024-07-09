using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FirstPersonCam : MonoBehaviour
{
    public Transform player;
    public Vector3 eyeOffset = new Vector3(0.0f, 1.6f, 0.0f); // 플레이어 눈 위치

    public float smooth = 10f;
    public float horizontalAimingSpeed = 6.0f;
    public float verticalAimingSpeed = 6.0f;
    public float maxVerticalAngle = 30.0f;
    public float minVerticalAngle = -60.0f;
    public float recoilAngleBounce = 5.0f;
    private float angleH = 0.0f;
    private float angleV = 0.0f;
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

        cameraTransform.position = player.position + eyeOffset;
        cameraTransform.rotation = Quaternion.identity;

        defaultFOV = myCamera.fieldOfView;
        angleH = player.eulerAngles.y;

        ResetFOV();
        ResetMaxVerticalAngle();
    }

    public void ResetFOV()
    {
        this.targetFOV = defaultFOV;
    }

    public void ResetMaxVerticalAngle()
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

        // 카메라 위치는 고정
        cameraTransform.position = player.position + eyeOffset;

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
