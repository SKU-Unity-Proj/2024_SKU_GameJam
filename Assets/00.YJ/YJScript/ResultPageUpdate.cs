using UnityEngine;
using TMPro;
using System.Linq;

public class ResultPageUpdate : MonoBehaviour
{
    public TextMeshProUGUI failTextUI;

    private void OnEnable()
    {
        // MissionManager 인스턴스의 미션 리스트를 가져옴
        var missions = MissionManager.Instance.missions;

        // isCompleted가 false인 미션 중 가장 마지막 미션을 찾음
        var lastUncompletedMission = missions.LastOrDefault(m => !m.isCompleted);

        if (lastUncompletedMission != null)
        {
            // failTextUI에 missionFailText를 업데이트
            failTextUI.text = lastUncompletedMission.missionFailText;
        }
        else
        {
            // 완료되지 않은 미션이 없는 경우 기본 텍스트 설정
            failTextUI.text = "모든 미션이 완료되었습니다.";
        }
    }
}
