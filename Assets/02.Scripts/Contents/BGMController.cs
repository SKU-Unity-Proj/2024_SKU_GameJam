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
        // �� ��ε� �̺�Ʈ�� �޼��� ���
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        SoundManager.Instance.PlayBGM(SoundList.mainstageSound1);
        //SoundManager.Instance.PlayOneShotEffect((int)SoundList.mainstageSound1, transform.position, 0.5f);
    }

    private void Update()
    {
        // ���� �޴� BGM ���
        //SoundManager.Instance.PlayOneShotEffect();
        
    }

    private void OnDestroy()
    {
        // �� ��ε� �̺�Ʈ���� �޼��� ����
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    // �� ��ε�� �� ȣ��� �޼���
    private void OnSceneUnloaded(Scene current)
    {
        SoundManager.Instance.Stop(true);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // ���� �ε�� �� BGM ���
        PlayBGMForScene(scene.name);

    }

    private void PlayBGMForScene(string sceneName)
    {
        Debug.Log("PlayBGMForScene called for scene: " + sceneName); // �޼��� ȣ�� ����� ���
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
