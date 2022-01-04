using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class LoadingCurtain : MonoBehaviour
{
    private CanvasGroup _curtain;
    private const float FadeTime = 0.03f;

    private void Awake()
    {
        _curtain = GetComponent<CanvasGroup>();
        DontDestroyOnLoad(this);
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _curtain.alpha = 1;
    }

    public void Hide() =>
        StartCoroutine((FadeIn()));

    private IEnumerator FadeIn()
    {
        while (_curtain.alpha > 0)
        {
            _curtain.alpha -= FadeTime;
            yield return new WaitForSeconds(FadeTime);
        }

        gameObject.SetActive(false);
    }
}