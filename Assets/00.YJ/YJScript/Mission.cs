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

    // ������
    public Mission(int id, string name, bool completed)
    {
        this.id = id;
        missionName = name;
        isCompleted = completed;
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
        }
        else // ����
        {
            starUI.sprite= uncompleteStar;
            missionText.text = "???";
        }
    }
}
