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
                // 예시: 시그널마다 다른 스카이박스 인덱스로 변경하도록 설정
                int skyboxIndex = (int)context; // 시그널에서 인덱스 정보를 가져올 수 있음
                skyboxManager.SetSkybox(skyboxIndex);
            }
        }
    }
}
