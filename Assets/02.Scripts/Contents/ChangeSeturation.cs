using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeSaturation : MonoBehaviour
{
    public Volume globalVolume; // �۷ι� ���� ������Ʈ�� Volume ������Ʈ
    private ColorAdjustments colorAdjustments;

    private void Start()
    {
        // �۷ι� ������ VolumeProfile���� ColorAdjustments ������Ʈ�� ã���ϴ�.
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
        float duration = 2f; // Saturation�� ������ �ð� (��)
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            colorAdjustments.saturation.value = Mathf.Lerp(colorAdjustments.saturation.value, targetSaturation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        colorAdjustments.saturation.value = targetSaturation; // ���� ������ ����
    }
}
