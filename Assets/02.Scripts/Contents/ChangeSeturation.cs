using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeSaturation : MonoBehaviour
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

    public void OnMissionCompleted()
    {
        if (colorAdjustments != null)
        {
            StartCoroutine(IncreaseSaturationGradually());
        }
    }

    private IEnumerator IncreaseSaturationGradually()
    {
        float targetSaturation = colorAdjustments.saturation.value + 10f;
        float duration = 2f; // Saturation이 증가할 시간 (초)
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            colorAdjustments.saturation.value = Mathf.Lerp(colorAdjustments.saturation.value, targetSaturation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        colorAdjustments.saturation.value = targetSaturation; // 최종 값으로 설정
    }
}
