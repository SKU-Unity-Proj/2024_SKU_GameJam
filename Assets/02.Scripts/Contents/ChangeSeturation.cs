using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeSetuation : MonoBehaviour
{
    public Volume globalVolume; // 글로벌 볼륨 오브젝트의 Volume 컴포넌트
    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        // 글로벌 볼륨의 VolumeProfile에서 ColorAdjustments 컴포넌트를 찾습니다.
        if (globalVolume != null && globalVolume.profile != null)
        {
            globalVolume.profile.TryGet(out colorAdjustments);
        }
    }

    private void Update()
    {
        // 오른쪽 마우스 버튼 입력을 감지합니다.
        if (Input.GetMouseButtonDown(1))
        {
            IncreaseSaturation();
        }
    }

    private void IncreaseSaturation()
    {
        if (colorAdjustments != null)
        {
            // 현재 Saturation 값을 +10 증가시킵니다.
            colorAdjustments.saturation.value += 10f;
        }
    }
}
