using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour
{
    public CanvasGroup canvasGroup; // CanvasGroup ������Ʈ ����
    public float fadeDuration = 1.0f; // ���̵� ��/�ƿ� �ð�
    public float blinkInterval = 0.5f; // �����Ÿ��� ���� (���̵� ��/�ƿ� ������ ��� �ð�)

    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            // ���̵� ��
            yield return StartCoroutine(Fade(0, 1, fadeDuration));
            // ��� �ð�
            yield return new WaitForSeconds(blinkInterval);
            // ���̵� �ƿ�
            yield return StartCoroutine(Fade(1, 0, fadeDuration));
            // ��� �ð�
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }
}