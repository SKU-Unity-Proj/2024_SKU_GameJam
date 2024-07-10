using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuManager : MonoBehaviour
{
    private void Start()
    {
        // 메인 메뉴 BGM 재생
        SoundManager.Instance.PlayBGM(SoundList.mainmenuSound);

        // 씬 언로드 이벤트에 메서드 등록
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDestroy()
    {
        // 씬 언로드 이벤트에서 메서드 해제
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
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
        LoadScene("Scene_01Ex"); // "GameScene"을 원하는 씬 이름으로 변경하세요.
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