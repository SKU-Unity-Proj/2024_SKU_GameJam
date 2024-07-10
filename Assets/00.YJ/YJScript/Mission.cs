using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Mission : MonoBehaviour
{
    public int id;

    // 미션의 이름
    public string missionName;

    //미션 실패 했을 경우 결과창에 나타날 메세지
    public string missionFailText;

    public Image starUI;
    public TextMeshProUGUI missionText;
    public Sprite completeStar;
    public Sprite uncompleteStar;

    // 미션의 완료 상태
    public bool isCompleted = false;

    private ChecklistManager checklistManager;
    private ChangeSaturation changeSaturation;
    private int soundIndex;  // 현재 재생할 소리의 인덱스

    public SoundList[] sounds;  // 재생할 소리 목록

    // 생성자
    public Mission(int id, string name, bool completed)
    {
        this.id = id;
        missionName = name;
        isCompleted = completed;
    }

    private void Start()
    {
        checklistManager = FindObjectOfType<ChecklistManager>();
        if (checklistManager == null)
        {
            Debug.LogError("ChecklistManager not found in the scene.");
        }

        changeSaturation = FindObjectOfType<ChangeSaturation>();
        if (changeSaturation == null)
        {
            Debug.LogError("ChangeSaturation not found in the scene.");
        }
    }

    // 미션 완료 여부를 판단하는 함수
    public bool CheckCompletion(Mission mission)
    {
        return mission.isCompleted;
    }

    private void Update()
    {
        if (isCompleted) // 성공
        {
            starUI.sprite = completeStar;
            missionText.text = missionName;
            checklistManager?.UpdateChecklist(id, true);
            changeSaturation?.OnMissionCompleted();
            PlayRandomSound();  // 미션 완료 시 랜덤 보이스 재생
            enabled = false; // 미션 완료 후 Update 호출 중지
        }
        else // 실패
        {
            starUI.sprite = uncompleteStar;
            missionText.text = "???";
            checklistManager?.UpdateChecklist(id, false);
        }
    }

    private void PlayRandomSound()
    {
        if (sounds.Length == 0) return;  // sounds 배열이 비어있을 경우 리턴

        int oldIndex = soundIndex;
        while (oldIndex == soundIndex && sounds.Length > 1)
        {
            soundIndex = Random.Range(0, sounds.Length);
        }

        SoundManager.Instance.PlayOneShotEffect((int)sounds[soundIndex], this.transform.position, 1.5f);
    }
}
