using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    #region �̱���
    // �̱��� �ν��Ͻ��� �����ϴ� ���� ����
    private static MissionManager instance;

    // �ν��Ͻ��� ���� ���� �����ڸ� �����մϴ�.
    public static MissionManager Instance
    {
        get
        {
            // �ν��Ͻ��� null�̸� ���� ã�� �Ҵ��մϴ�.
            if (instance == null)
            {
                instance = FindObjectOfType<MissionManager>();

                if (instance == null)
                {
                    // GameManager�� ������ �� GameManager�� ����ϴ�.
                    GameObject singleton = new GameObject(typeof(MissionManager).ToString());
                    instance = singleton.AddComponent<MissionManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    // Awake �޼��忡�� �̱��� �ν��Ͻ��� �����մϴ�.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // �� ���� ������Ʈ�� �� ��ȯ �� �ı����� �ʵ��� �����մϴ�.
        }
        else if (instance != this)
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� �ڽ��� �ı��մϴ�.
        }
    }
    #endregion

    // �̼� ����Ʈ
    public List<Mission> missions = new List<Mission>();

    // �ʱ�ȭ �Լ�
    void Start()
    {
        // ù ��° �̼��� �Ϸ� ���¸� false���� true�� ����
        ChangeMissionStatus(1, true);
    }

    // Ư�� �̼��� �Ϸ� ���¸� �����ϴ� �Լ�
    public void ChangeMissionStatus(int missionId, bool isCompleted)
    {
        Mission mission = missions.Find(m => m.id == missionId);
        if (mission != null)
        {
            mission.isCompleted = isCompleted;
        }
        else
        {
            Debug.LogWarning($"�̼� ID {missionId}�� ã�� �� �����ϴ�.");
        }
    }

    // ��� �̼��� �Ϸ� ���¸� Ȯ���ϴ� �Լ�
    public void CheckAllMissions()
    {
        foreach (Mission mission in missions)
        {
            Debug.Log($"�̼� ID: {mission.id}, �̸�: {mission.missionName}, �Ϸ�: {mission.CheckCompletion(mission)}");
        }
    }
}
