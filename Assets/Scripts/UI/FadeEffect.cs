using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeEffect : MonoBehaviour {

    public Sprite FadeInSprite;
    public Sprite FadeOutSprite;
    public float MoveSpeed;

    private Image ImageComponent;

    [HideInInspector] public bool MoveEnd;

    private void Awake()
    {
        ImageComponent = GetComponent<Image>();
        MoveEnd = false;
    }

    public void FadeIn()
    {
        // Fade In Image Setting
        ImageComponent.sprite = FadeInSprite;

        StartCoroutine(MoveFadeImage(-940.0f, 1600.0f));
        
        // 완료 시 페이더 비활성화
        // ...
    }

    public void FadeOut()
    {
        ImageComponent.sprite = FadeOutSprite;
        StartCoroutine(MoveFadeImage(-1600.0f, 960.0f));
    }

    // Move to FadeEfx Image.
    IEnumerator MoveFadeImage(float startX, float endX)
    {
        // Move UI Object
        float nowLerp = 0.0f;
        float nowPanelCoordX;
        MoveEnd = false;

        while(true)
        {
            nowPanelCoordX = Mathf.Lerp(startX, endX, nowLerp);
            transform.localPosition = new Vector3(nowPanelCoordX, 0, 0);

            if (nowLerp < 1.0f) // 보간수치가 1 이하면 상승, 넘어가면 이동완료
                nowLerp += Time.deltaTime / MoveSpeed; // 1.5초
            else
            {
                MoveEnd = true;
                break;
            }

            yield return null;
        }

        yield break;
    }

    
    
}
