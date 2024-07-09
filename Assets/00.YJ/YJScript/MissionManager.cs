using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    #region 싱글톤
    // 싱글톤 인스턴스를 저장하는 정적 변수
    private static MissionManager instance;

    // 인스턴스에 대한 공용 접근자를 제공합니다.
    public static MissionManager Instance
    {
        get
        {
            // 인스턴스가 null이면 새로 찾아 할당합니다.
            if (instance == null)
            {
                instance = FindObjectOfType<MissionManager>();

                if (instance == null)
                {
                    // GameManager가 없으면 새 GameManager를 만듭니다.
                    GameObject singleton = new GameObject(typeof(MissionManager).ToString());
                    instance = singleton.AddComponent<MissionManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    // Awake 메서드에서 싱글톤 인스턴스를 설정합니다.
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 이 게임 오브젝트를 씬 전환 시 파괴하지 않도록 설정합니다.
        }
        else if (instance != this)
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 자신을 파괴합니다.
        }
    }
    #endregion

    // 미션 리스트
    public List<Mission> missions = new List<Mission>();

    // 초기화 함수
    void Start()
    {
        // 첫 번째 미션의 완료 상태를 false에서 true로 변경
        ChangeMissionStatus(1, true);
    }

    // 특정 미션의 완료 상태를 변경하는 함수
    public void ChangeMissionStatus(int missionId, bool isCompleted)
    {
        Mission mission = missions.Find(m => m.id == missionId);
        if (mission != null)
        {
            mission.isCompleted = isCompleted;
        }
        else
        {
            Debug.LogWarning($"미션 ID {missionId}를 찾을 수 없습니다.");
        }
    }

    // 모든 미션의 완료 상태를 확인하는 함수
    public void CheckAllMissions()
    {
        foreach (Mission mission in missions)
        {
            Debug.Log($"미션 ID: {mission.id}, 이름: {mission.missionName}, 완료: {mission.CheckCompletion(mission)}");
        }
    }
}
