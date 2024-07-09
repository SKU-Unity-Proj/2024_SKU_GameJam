using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour
{
    public CanvasGroup canvasGroup; // CanvasGroup 컴포넌트 참조
    public float fadeDuration = 1.0f; // 페이드 인/아웃 시간
    public float blinkInterval = 0.5f; // 깜빡거리는 간격 (페이드 인/아웃 사이의 대기 시간)

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
            // 페이드 인
            yield return StartCoroutine(Fade(0, 1, fadeDuration));
            // 대기 시간
            yield return new WaitForSeconds(blinkInterval);
            // 페이드 아웃
            yield return StartCoroutine(Fade(1, 0, fadeDuration));
            // 대기 시간
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