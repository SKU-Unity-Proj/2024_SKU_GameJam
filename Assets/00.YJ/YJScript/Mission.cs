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
            enabled = false; // �̼� �Ϸ� �� Update ȣ�� ����
        }
        else // ����
        {
            starUI.sprite = uncompleteStar;
            missionText.text = "???";
            checklistManager?.UpdateChecklist(id, false);
        }
    }
}
