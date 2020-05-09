using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PannelController
{
    public SpriteRenderer pannelRenderer;

    public IEnumerator FadeIn()
    {
        float nowLerp = 0.0f;
        float nowAlpha;

        while (true)
        {
            nowAlpha = Mathf.Lerp(1, 0, nowLerp);

            pannelRenderer.color = new Color(1, 1, 1, nowAlpha);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 완료
                nowLerp += Time.deltaTime;
            else
                break;

            yield return null;
        }

        yield break;
    }

    public IEnumerator FadeOut()
    {
        float nowLerp = 0.0f;
        float nowAlpha;

        while (true)
        {
            nowAlpha = Mathf.Lerp(0, 1, nowLerp);

            pannelRenderer.color = new Color(1, 1, 1, nowAlpha);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 완료
                nowLerp += Time.deltaTime;
            else
                break;

            yield return null;
        }
    }

    // 현재 알파값을 기준, 입력된 알파값까지 조정
    public IEnumerator CustomFade( float _fAlpha, float _fTime = 1)
    {
        float nowLerp = 0.0f;
        float nowAlpha;
        float firstAlpha = pannelRenderer.color.a;

        while (true)
        {
            nowAlpha = Mathf.Lerp(firstAlpha, _fAlpha, nowLerp);

            pannelRenderer.color = new Color(1, 1, 1, nowAlpha);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 완료
                nowLerp += Time.deltaTime / _fTime;
            else
                break;

            yield return null;
        }
    }

    public IEnumerator FadeOut(float _fNowAlpha, float _fAlpha)
    {
        float nowLerp = 0.0f;
        float nowAlpha;
        float firstAlpha = pannelRenderer.color.a;

        while (true)
        {
            nowAlpha = Mathf.Lerp(firstAlpha, _fAlpha, nowLerp);

            pannelRenderer.color = new Color(1, 1, 1, nowAlpha);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 완료
                nowLerp += Time.deltaTime;
            else
                break;

            yield return null;
        }
    }
}
