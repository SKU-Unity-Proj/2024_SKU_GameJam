using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgentVisibilityManager : MonoBehaviour
{
    public Camera mainCamera; // �÷��̾��� ī�޶�
    public string agentTag = "Agent"; // Agent �±�

    public Transform soundpos;

    public float soundInterval = 13.0f; // ���� ��� ����
    public float checkInterval = 1.0f; // �þ� üũ ����
    public float visibilityTimeout = 15.0f; // �þ߿� ������ �ʴ� �ð�
    public CanvasGroup alertImage; // �˸� �̹����� ������ CanvasGroup

    public SoundList crySound;
    private Dictionary<GameObject, float> agentLastSeenTime; // ������Ʈ ���������� ���� �ð��� �����ϴ� ��ųʸ�

    private Coroutine blinkCoroutine; // �����̴� �ڷ�ƾ
    private Coroutine soundCoroutine; // ���� ��� �ڷ�ƾ

    private void Start()
    {
        agentLastSeenTime = new Dictionary<GameObject, float>();

        // ��� ������Ʈ�� ã�� �ʱ�ȭ
        GameObject[] agents = GameObject.FindGameObjectsWithTag(agentTag);
        foreach (GameObject agent in agents)
        {
            agentLastSeenTime[agent] = Time.time;
        }

        alertImage.alpha = 0; // �ʱ⿡�� �̹����� ������ �ʵ��� ����

        StartCoroutine(CheckAgentsVisibility());
    }

    private IEnumerator CheckAgentsVisibility()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            bool anyAgentInvisible = false;
            foreach (GameObject agent in agentLastSeenTime.Keys)
            {
                if (IsInView(mainCamera, agent))
                {
                    agentLastSeenTime[agent] = Time.time; // ������Ʈ�� �þ߿� ���̸� �ð� ������Ʈ
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
                    soundCoroutine = null;
                }
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
