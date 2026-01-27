using UnityEngine;
using System.Collections;

public class CanvasFade : MonoBehaviour {
    public CanvasGroup canvasGroup;
    public float fadeDuration = 3f;

    void Start() {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn() {
        canvasGroup.alpha = 1;
        while (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }
    }
}