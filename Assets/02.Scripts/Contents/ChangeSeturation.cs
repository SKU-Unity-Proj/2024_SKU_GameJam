using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeSetuation : MonoBehaviour
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

    private void Update()
    {
        // ������ ���콺 ��ư �Է��� �����մϴ�.
        if (Input.GetMouseButtonDown(1))
        {
            IncreaseSaturation();
        }
    }

    private void IncreaseSaturation()
    {
        if (colorAdjustments != null)
        {
            // ���� Saturation ���� +10 ������ŵ�ϴ�.
            colorAdjustments.saturation.value += 10f;
        }
    }
}
