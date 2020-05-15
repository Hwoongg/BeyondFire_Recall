using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// Fade Effect 오브젝트 접근용 클래스. 항상 활성화 되어있을것
//

public class Fader : MonoBehaviour
{
    FadeEffect fadeEffect;

    private void Awake()
    {
        // 자식에 붙은 페이드 이펙트 컴포넌트 획득
        fadeEffect = transform.GetChild(0).GetComponent<FadeEffect>();
    }
    
    public void FadeIn()
    {
        fadeEffect.gameObject.SetActive(true);
        fadeEffect.FadeIn();
    }

    public void FadeOut()
    {
        fadeEffect.FadeOut();
    }
}
