using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BGMController : MonoBehaviour
{
    public static BGMController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드될 때 BGM 재생
        PlayBGMForScene(scene.name);

    }

    private void PlayBGMForScene(string sceneName)
    {
        Debug.Log("PlayBGMForScene called for scene: " + sceneName); // 메서드 호출 디버그 출력
        switch (sceneName)
        {
            case "MainMenu":
                SoundManager.Instance.PlayBGM(SoundList.mainmenuSound);
                break;
            case "Scene_01Ex":
                SoundManager.Instance.PlayBGM(SoundList.mainstageSound1);
                break;

            default:
                Debug.LogWarning("No BGM defined for scene: " + sceneName);
                break;
        }
    }

}
