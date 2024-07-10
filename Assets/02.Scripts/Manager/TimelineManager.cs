using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector playableDirector;

    private void Start()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped += OnPlayableDirectorStopped;
        }
    }

    private void OnDestroy()
    {
        if (playableDirector != null)
        {
            playableDirector.stopped -= OnPlayableDirectorStopped;
        }
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        if (director == playableDirector)
        {
            Debug.Log("Timeline has ended.");

            // ���⿡�� Ÿ�Ӷ����� ������ �� ������ �ڵ带 �ۼ��ϼ���.
            // ���� ���, Ư�� ������Ʈ�� Ȱ��ȭ/��Ȱ��ȭ�ϰų� �ٸ� ������ �̵��� �� �ֽ��ϴ�.
            // Example: Deactivate a UI element
            // someUIElement.SetActive(false);

            // Example: Load another scene
            SceneManager.LoadScene("MainMenu");
        }
    }
}
