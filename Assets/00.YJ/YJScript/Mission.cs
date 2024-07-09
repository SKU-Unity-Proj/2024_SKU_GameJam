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

    // 생성자
    public Mission(int id, string name, bool completed)
    {
        this.id = id;
        missionName = name;
        isCompleted = completed;
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
        }
        else // 실패
        {
            starUI.sprite= uncompleteStar;
            missionText.text = "???";
        }
    }
}
