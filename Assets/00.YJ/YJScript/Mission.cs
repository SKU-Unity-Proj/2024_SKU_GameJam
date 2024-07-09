using UnityEngine;

[System.Serializable]
public class Mission : MonoBehaviour
{
    public int id;

    // �̼��� �̸�
    public string missionName;

    //�̼� ���� ���� ��� ���â�� ��Ÿ�� �޼���
    public string missionFailText;

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
        // isCompleted ���� ��ȯ
        return mission.isCompleted;
    }

    // ���÷� �̼� �Ϸ� ���¸� ����ϴ� �Լ�
    public void ToggleCompletion()
    {
        isCompleted = !isCompleted;
    }
}
