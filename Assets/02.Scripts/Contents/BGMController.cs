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
    private void Start()
    {
        // 씬 언로드 이벤트에 메서드 등록
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SoundManager.Instance.PlayBGM(SoundList.mainstageSound1);
        //SoundManager.Instance.PlayOneShotEffect((int)SoundList.mainstageSound1, transform.position, 0.5f);
    }

    private void Update()
    {
        // 메인 메뉴 BGM 재생
        //SoundManager.Instance.PlayOneShotEffect();
        
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
