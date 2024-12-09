using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class fadeMenu : MonoBehaviour
{

    public CanvasGroup canvasGroup; // CanvasGroup을 통한 투명도 제어
    public float fadeDuration = 1.0f; // 페이드 지속 시간

    /// <summary>
    /// 페이드 인 효과: 투명도 0 → 1
    /// </summary>
    public void FadeIn()
    {
        StartCoroutine(Fade(0f, 1f));
    }

    /// <summary>
    /// 페이드 아웃 효과: 투명도 1 → 0
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(Fade(1f, 0f));
    }

    /// <summary>
    /// 투명도 조절 코루틴
    /// </summary>
    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;

        // 시작 및 종료 알파 값 설정
        canvasGroup.alpha = startAlpha;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            yield return null;
        }

        // 최종 알파 값 고정
        canvasGroup.alpha = endAlpha;
    }
}
