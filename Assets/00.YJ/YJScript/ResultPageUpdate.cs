using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class ResultPageUpdate : MonoBehaviour
{
    public Image babyUI;
    public Sprite happyBaby;
    public Sprite cryBaby;

    public TextMeshProUGUI failTextUI;
    public Image bronzeMedal;
    public Image sliverMedal;
    public Image goldMedal;

    private FirstPersonCam firstPersonCam;

    private void Awake()
    {
        firstPersonCam = FindAnyObjectByType<FirstPersonCam>();
    }

    private void OnEnable()
    {
        firstPersonCam.enabled = false;
        
        var missions = MissionManager.Instance.missions;

        // isCompleted가 false인 미션 중 가장 마지막 미션을 찾음
        var lastUncompletedMission = missions.LastOrDefault(m => !m.isCompleted);

        if (lastUncompletedMission != null)
        {
            Debug.Log($"{lastUncompletedMission.missionFailText}");
            failTextUI.text = lastUncompletedMission.missionFailText;
        }
        else
        {
            failTextUI.text = "아이가 편안한 모습으로 자고 있습니다.";
        }

        // 완료된 미션 개수
        int uncompletedMissionCount = missions.Count(m => m.isCompleted);
        Debug.Log($"{uncompletedMissionCount}");

        if(uncompletedMissionCount > 2)
        {
            bronzeMedal.color = Color.white;
            babyUI.sprite = cryBaby;
            if (uncompletedMissionCount > 5)
            {
                sliverMedal.color = Color.white;
                babyUI.sprite = cryBaby;
                if (uncompletedMissionCount > 8)
                {
                    goldMedal.color = Color.white;
                    babyUI.sprite = happyBaby;
                }  
            }
        }
        else
        {
            bronzeMedal.color = Color.black;
            sliverMedal.color = Color.black;
            goldMedal.color = Color.black;
            babyUI.sprite = cryBaby;
        }
    }

    private void OnDisable()
    {
        firstPersonCam.enabled = true;
    }
}
