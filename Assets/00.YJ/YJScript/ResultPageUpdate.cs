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

    public FirstPersonCam firstPersonCam;

    private void Awake()
    {
        firstPersonCam = FindAnyObjectByType<FirstPersonCam>();
    }

    private void OnEnable()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        firstPersonCam.enabled = false;
        
        var missions = MissionManager.Instance.missions;

        // isCompleted�� false�� �̼� �� ���� ������ �̼��� ã��
        var lastUncompletedMission = missions.LastOrDefault(m => !m.isCompleted);

        if (lastUncompletedMission != null)
        {
            Debug.Log($"{lastUncompletedMission.missionFailText}");
            failTextUI.text = lastUncompletedMission.missionFailText;
        }
        else
        {
            failTextUI.text = "���̰� ����� ������� �ڰ� �ֽ��ϴ�.";
        }

        // �Ϸ�� �̼� ����
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
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        firstPersonCam.enabled = true;
    }
}
