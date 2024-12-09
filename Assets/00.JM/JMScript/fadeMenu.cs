using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class fadeMenu : MonoBehaviour
{

    public CanvasGroup canvasGroup; // CanvasGroup�� ���� ���� ����
    public float fadeDuration = 1.0f; // ���̵� ���� �ð�

    /// <summary>
    /// ���̵� �� ȿ��: ���� 0 �� 1
    /// </summary>
    public void FadeIn()
    {
        StartCoroutine(Fade(0f, 1f));
    }

    /// <summary>
    /// ���̵� �ƿ� ȿ��: ���� 1 �� 0
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    /// <summary>
    /// ���� ���� �ڷ�ƾ
    /// </summary>
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;

        // ���� �� ���� ���� �� ����
        canvasGroup.alpha = startAlpha;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            yield return null;
        }

        // ���� ���� �� ����
        canvasGroup.alpha = endAlpha;
    }
}
