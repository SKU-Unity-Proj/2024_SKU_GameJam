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

    public GameObject ending;

    private bool isEnding = false;

    private void Awake()
    {
        if(firstPersonCam == null)
            firstPersonCam = FindObjectOfType<FirstPersonCam>();
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

        if(uncompletedMissionCount > 1)
        {
            bronzeMedal.color = Color.white;
            babyUI.sprite = cryBaby;
            if (uncompletedMissionCount > 4)
            {
                sliverMedal.color = Color.white;
                babyUI.sprite = cryBaby;
                if (uncompletedMissionCount > 7)
                {
                    goldMedal.color = Color.white;
                    babyUI.sprite = happyBaby;
                    isEnding = true;
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

    public void Sleep()
    {
        ending.SetActive(true);
    }
}
