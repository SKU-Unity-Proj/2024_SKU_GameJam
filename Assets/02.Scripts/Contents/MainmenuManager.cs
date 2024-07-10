using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainmenuManager : MonoBehaviour
{
    public GameObject nameInputUI; // 이름 입력 UI 오브젝트
    public TMP_InputField nameInputField; // 이름 입력 필드

    private void Start()
    {
        // 메인 메뉴 BGM 재생
        SoundManager.Instance.PlayBGM(SoundList.mainmenuSound);

        // 씬 언로드 이벤트에 메서드 등록
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        // 이름 입력 UI 비활성화
        nameInputUI.SetActive(false);

        // InputField의 onEndEdit 이벤트에 메서드 등록
        nameInputField.onEndEdit.AddListener(OnNameInputEnd);
    }

    private void OnDestroy()
    {
        // 씬 언로드 이벤트에서 메서드 해제
        SceneManager.sceneUnloaded -= OnSceneUnloaded;

        // InputField의 onEndEdit 이벤트에서 메서드 해제
        nameInputField.onEndEdit.RemoveListener(OnNameInputEnd);
    }

    // 씬 언로드될 때 호출될 메서드
    private void OnSceneUnloaded(Scene current)
    {
        SoundManager.Instance.Stop(true);
    }

    // 씬 이동 함수
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 버튼 클릭 시 호출될 함수
    public void OnPlayButtonClicked()
    {
        SoundManager.Instance.PlayOneShotEffect((int)SoundList.click, this.transform.position, 3f);
        // 이름 입력 UI 활성화
        nameInputUI.SetActive(true);
        nameInputField.Select(); // 입력 필드를 선택하여 키보드를 활성화합니다.
    }

    // InputField에서 이름 입력이 끝났을 때 호출될 함수
    private void OnNameInputEnd(string playerName)
    {
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(playerName))
        {
            // 이름을 저장합니다.
            AgentData.SetPlayerName(playerName);

            Debug.Log(AgentData.GetPlayerName());
            // 이름이 입력되었으면 씬을 이동합니다.
            LoadScene("Scene_01Ex"); // "GameScene"을 원하는 씬 이름으로 변경하세요.
        }
    }

    // 다른 버튼 클릭 함수 예제
    public void OnSettingsButtonClicked()
    {
        LoadScene("SettingsScene"); // "SettingsScene"을 원하는 씬 이름으로 변경하세요.
    }

    // 종료 버튼 클릭 함수 예제
    public void OnExitButtonClicked()
    {
        Application.Quit();
    }
}
