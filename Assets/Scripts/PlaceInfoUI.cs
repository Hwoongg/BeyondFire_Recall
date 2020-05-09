using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlaceInfoUI : MonoBehaviour
{
    public GameObject objInfoText;
    Text InfoText;


	// Use this for initialization
	void Start ()
    {
        InfoText = objInfoText.GetComponent<Text>();
	}
	
	public IEnumerator TextFadeInRoutine()
    {
        float nowLerp = 0.0f;
        float nowAlpha;

        while(true)
        {
            nowAlpha = Mathf.Lerp(0, 1, nowLerp);
            InfoText.color = new Color(1, 1, 0, nowAlpha);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 완료
                nowLerp += Time.deltaTime;
            else
                break;

            yield return null;
        }
        yield break;
    }
    public IEnumerator TextFadeOutRoutine()
    {
        float nowLerp = 0.0f;
        float nowAlpha;

        while (true)
        {
            nowAlpha = Mathf.Lerp(1, 0, nowLerp);
            InfoText.color = new Color(1, 1, 0, nowAlpha);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 완료
                nowLerp += Time.deltaTime;
            else
                break;

            yield return null;
        }
        yield break;
    }

    public IEnumerator TextFadeRoutine(string _text)
    {
        // 텍스트 활성화
        TextOn(_text);

        // Fade In
        yield return StartCoroutine(TextFadeInRoutine());

        yield return new WaitForSeconds(1.0f);

        // Fade Out
        yield return StartCoroutine(TextFadeOutRoutine());

        // 텍스트 비활성화
        TextOff();

        yield break;
    }

    public void TextOn(string _text)
    {
        objInfoText.SetActive(true);
        InfoText.text = _text;
        InfoText.color = new Color(1, 1, 0, 1);
    }

    public void TextOff()
    {
        objInfoText.SetActive(false);
    }
}
