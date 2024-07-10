using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentVisibilityManager : MonoBehaviour
{
    public Camera mainCamera; // 플레이어의 카메라
    public List<GameObject> agents; // Agent 리스트

    public Transform soundpos;

    public float soundInterval = 13.0f; // 사운드 재생 간격
    public float checkInterval = 1.0f; // 시야 체크 간격
    public float visibilityTimeout = 15.0f; // 시야에 보이지 않는 시간
    public CanvasGroup alertImage; // 알림 이미지를 포함한 CanvasGroup
    public SoundList crySound;

    private Dictionary<GameObject, float> agentLastSeenTime; // 에이전트 마지막으로 보인 시간을 저장하는 딕셔너리

    private Coroutine blinkCoroutine; // 깜빡이는 코루틴
    private Coroutine soundCoroutine; // 사운드 재생 코루틴

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // 메인 카메라 참조
        }

        agentLastSeenTime = new Dictionary<GameObject, float>();

        // 모든 에이전트를 초기화
        foreach (GameObject agent in agents)
        {
            agentLastSeenTime[agent] = Time.time;
        }

        alertImage.alpha = 0; // 초기에는 이미지가 보이지 않도록 설정

        StartCoroutine(CheckAgentsVisibility());
    }

    private IEnumerator CheckAgentsVisibility()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            bool anyAgentInvisible = false;
            foreach (GameObject agent in agents)
            {
                if (IsInView(mainCamera, agent))
                {
                    Debug.Log("Find!");
                    agentLastSeenTime[agent] = Time.time; // 에이전트가 시야에 보이면 시간 업데이트
                }
                else if (Time.time - agentLastSeenTime[agent] > visibilityTimeout)
                {
                    anyAgentInvisible = true;
                }
            }

            if (anyAgentInvisible)
            {
                if (blinkCoroutine == null)
                {
                    blinkCoroutine = StartCoroutine(BlinkImage());
                }

                if (soundCoroutine == null)
                {
                    soundCoroutine = StartCoroutine(PlaySound());
                }
            }
            else
            {
                if (blinkCoroutine != null)
                {
                    StopCoroutine(blinkCoroutine);
                    blinkCoroutine = null;
                    alertImage.alpha = 0;
                }

                if (soundCoroutine != null)
                {
                    StopCoroutine(soundCoroutine);
                    //SoundManager.Instance.Stop(true);
                    soundCoroutine = null;
                }
            }
        }
    }

    public void SetAlertImageAlpha(float alpha)
    {
        alertImage.alpha = alpha;

        if (alpha == 0)
        {
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;
            }

            if (soundCoroutine != null)
            {
                StopCoroutine(soundCoroutine);
                
                soundCoroutine = null;
            }
        }
    }

    private bool IsInView(Camera camera, GameObject target)
    {
        Vector3 screenPoint = camera.WorldToViewportPoint(target.transform.position);
        bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;

        if (onScreen)
        {
            if (Physics.Linecast(camera.transform.position, target.transform.position, out RaycastHit hit))
            {
                return hit.collider.gameObject == target;
            }
            else
            {
                return true; // If there's no obstruction
            }
        }

        return false;
    }

    private IEnumerator BlinkImage()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(0, 1, 0.5f));
            yield return StartCoroutine(Fade(1, 0, 0.5f));
        }
    }

    private IEnumerator PlaySound()
    {
        while (true)
        {
            SoundManager.Instance.PlayOneShotEffect((int)crySound, soundpos.position, 3f);
            yield return new WaitForSeconds(soundInterval);
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            alertImage.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        alertImage.alpha = endAlpha;
    }
}

