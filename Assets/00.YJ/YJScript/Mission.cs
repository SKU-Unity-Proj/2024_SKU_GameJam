using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Mission : MonoBehaviour
{
    public int id;

    // �̼��� �̸�
    public string missionName;

    //�̼� ���� ���� ��� ���â�� ��Ÿ�� �޼���
    public string missionFailText;

    public Image starUI;
    public TextMeshProUGUI missionText;
    public Sprite completeStar;
    public Sprite uncompleteStar;

    // �̼��� �Ϸ� ����
    public bool isCompleted = false;

    private ChecklistManager checklistManager;
    private ChangeSaturation changeSaturation;
    private int soundIndex;  // ���� ����� �Ҹ��� �ε���

    public SoundList[] sounds;  // ����� �Ҹ� ���

    // ������
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

    // �̼� �Ϸ� ���θ� �Ǵ��ϴ� �Լ�
    public bool CheckCompletion(Mission mission)
    {
        return mission.isCompleted;
    }

    private void Update()
    {
        if (isCompleted) // ����
        {
            starUI.sprite = completeStar;
            missionText.text = missionName;
            checklistManager?.UpdateChecklist(id, true);
            changeSaturation?.OnMissionCompleted();
            PlayRandomSound();  // �̼� �Ϸ� �� ���� ���̽� ���
            enabled = false; // �̼� �Ϸ� �� Update ȣ�� ����
        }
        else // ����
        {
            starUI.sprite = uncompleteStar;
            missionText.text = "???";
            checklistManager?.UpdateChecklist(id, false);
        }
    }

    private void PlayRandomSound()
    {
        if (sounds.Length == 0) return;  // sounds �迭�� ������� ��� ����

        int oldIndex = soundIndex;
        while (oldIndex == soundIndex && sounds.Length > 1)
        {
            soundIndex = Random.Range(0, sounds.Length);
        }

        SoundManager.Instance.PlayOneShotEffect((int)sounds[soundIndex], this.transform.position, 1.5f);
    }
}
