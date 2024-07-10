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

            // 여기에서 타임라인이 끝났을 때 실행할 코드를 작성하세요.
            // 예를 들어, 특정 오브젝트를 활성화/비활성화하거나 다른 씬으로 이동할 수 있습니다.
            // Example: Deactivate a UI element
            // someUIElement.SetActive(false);

            // Example: Load another scene
            SceneManager.LoadScene("MainMenu");
        }
    }
}
