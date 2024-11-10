using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LayerCullingSignalReceiver : MonoBehaviour, INotificationReceiver
{
    public Camera targetCamera;
    public string layerToShow;
    private int originalCullingMask;

    private const int TransparentFXLayer = 1; // TransparentFX 레이어 인덱스

    private void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        originalCullingMask = targetCamera.cullingMask;
    }

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is SignalAsset) // 타임라인에서 오는 시그널을 체크
        {
            SetLayerCullingMask(layerToShow);
        }
    }

    private void SetLayerCullingMask(string layerName)
    {
        if (string.IsNullOrEmpty(layerName)) return;

        int layer = LayerMask.NameToLayer(layerName);
        if (layer == -1)
        {
            Debug.LogError($"Layer '{layerName}' does not exist. Please check the layer name.");
            return;
        }

        // 지정된 레이어와 TransparentFX 레이어를 포함하여 cullingMask 설정
        targetCamera.cullingMask = (1 << layer) | (1 << TransparentFXLayer);
    }

    public void ResetCullingMask()
    {
        targetCamera.cullingMask = originalCullingMask;
    }
}
