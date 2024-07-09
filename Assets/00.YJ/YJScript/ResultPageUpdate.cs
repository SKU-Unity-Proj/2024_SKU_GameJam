using UnityEngine;
using TMPro;
using System.Linq;

public class ResultPageUpdate : MonoBehaviour
{
    public TextMeshProUGUI failTextUI;

    private void OnEnable()
    {
        // MissionManager �ν��Ͻ��� �̼� ����Ʈ�� ������
        var missions = MissionManager.Instance.missions;

        // isCompleted�� false�� �̼� �� ���� ������ �̼��� ã��
        var lastUncompletedMission = missions.LastOrDefault(m => !m.isCompleted);

        if (lastUncompletedMission != null)
        {
            // failTextUI�� missionFailText�� ������Ʈ
            failTextUI.text = lastUncompletedMission.missionFailText;
        }
        else
        {
            // �Ϸ���� ���� �̼��� ���� ��� �⺻ �ؽ�Ʈ ����
            failTextUI.text = "��� �̼��� �Ϸ�Ǿ����ϴ�.";
        }
    }
}
