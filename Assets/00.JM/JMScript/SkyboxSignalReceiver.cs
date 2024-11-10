using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SkyboxSignalReceiver : MonoBehaviour, INotificationReceiver
{
    public SkyboxManager skyboxManager;

    public void OnNotify(Playable origin, INotification notification, object context)
    {
        if (notification is SignalEmitter signalEmitter)
        {
            if (signalEmitter.asset.name == "ChangeSkybox")
            {
                // ����: �ñ׳θ��� �ٸ� ��ī�̹ڽ� �ε����� �����ϵ��� ����
                int skyboxIndex = (int)context; // �ñ׳ο��� �ε��� ������ ������ �� ����
                skyboxManager.SetSkybox(skyboxIndex);
            }
        }
    }
}
