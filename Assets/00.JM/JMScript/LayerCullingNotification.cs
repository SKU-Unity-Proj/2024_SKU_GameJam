using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class LayerCullingSignalReceiver : MonoBehaviour, INotificationReceiver
{
    public Camera targetCamera;
    public string layerToShow;
    private int originalCullingMask;

    private const int TransparentFXLayer = 1; // TransparentFX ���̾� �ε���

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
        if (notification is SignalAsset) // Ÿ�Ӷ��ο��� ���� �ñ׳��� üũ
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

        // ������ ���̾�� TransparentFX ���̾ �����Ͽ� cullingMask ����
        targetCamera.cullingMask = (1 << layer) | (1 << TransparentFXLayer);
    }

    public void ResetCullingMask()
    {
        targetCamera.cullingMask = originalCullingMask;
    }
}
