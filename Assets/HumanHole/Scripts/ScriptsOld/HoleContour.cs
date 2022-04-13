using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HoleContour : MonoBehaviour
{
    private SpriteRenderer _sprite;

    public void Appear()
    {
        StartCoroutine(AppearLoop());
    }

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.color = new Color(1, 1, 1, 0);
    }

    public void ShowDoneSuccess()
    {
        StartCoroutine(DissapearLoop(Color.green));
    }

    public void ShowDoneError()
    {
        StartCoroutine(DissapearLoop(Color.red));
    }

    private IEnumerator DissapearLoop(Color color)
    {
        Color targetColor = new Color(color.r, color.g, color.b, 0);
        Vector3 scale = transform.localScale;
        Vector3 targetScale = scale * 1.1f;
        

        float duration = 0.2f;
        float time = 0;
        CubicBezier bezier = new CubicBezier(BezierCurveType.EaseIn);
        while (time < duration)
        {
            float value = bezier.GetValue(time / duration);
            transform.localScale = Vector3.Lerp(scale, targetScale, value);
            _sprite.color = Color.Lerp(color, targetColor, value);
            time += Time.deltaTime;
            yield return null;
        }

        _sprite.color = targetColor;
    }

    private IEnumerator AppearLoop()
    {
        Vector3 scale = transform.localScale * 1.1f;
        Vector3 targetScale = transform.localScale;
        Color color = new Color(1, 1, 1, 0);
        Color targetColor = Color.white;

        float duration = 0.2f;
        float time = 0;
        CubicBezier bezier = new CubicBezier(BezierCurveType.EaseInOut);
        while (time < duration)
        {
            float value = bezier.GetValue(time / duration);
            transform.localScale = Vector3.Lerp(scale, targetScale, value);
            _sprite.color = Color.Lerp(color, targetColor, value);
            time += Time.deltaTime;
            yield return null;
        }

        _sprite.color = targetColor;
    }
}
