using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class HPSystem : MonoBehaviour
{

    // 체력 수치
    public float Health
    {
        get
        {
            return nowHealth;
        }
        set
        {
            nowHealth = value;
            if(nowHealth <= 0)
            {
                // 화염 사망 연출
                GameManager.Instance().SetNowScene();
                SceneManager.LoadScene("GameOver");
            }
        }
    }
    float MaxHealth;
    float nowHealth;


    [HideInInspector]
    public bool onFire;

    // 화염 단계를 입력받아 세팅되는 감소 수치
    private float dropParam;
    public void SetParam(int _FireLevel)
    {
        switch(_FireLevel)
        {
            case 0:
                dropParam = 10;
                minEfxAlpha = 0.1f;
                maxEfxAlpha = 0.2f;
                routineTime = 0.4f;
                break;
            case 1:
                dropParam = 30;
                minEfxAlpha = 0.2f;
                maxEfxAlpha = 0.3f;
                routineTime = 0.4f;
                break;
            case 2:
                dropParam = 75;
                minEfxAlpha = 0.3f;
                maxEfxAlpha = 0.6f;
                routineTime = 0.325f;
                break;
            case 3:
                dropParam = 125;
                minEfxAlpha = 0.4f;
                maxEfxAlpha = 0.8f;
                routineTime = 0.25f;
                break;
        }

    }
        


    // 화염 데미지 이미지 오브젝트, 컴포넌트
    public GameObject objFireDamage;
    SpriteRenderer imgFireDamage;

    float minEfxAlpha;
    float maxEfxAlpha;
    float routineTime;

    float nowLerp;
    float nowAlpha;
    float velocity;

    float falseTime;

    // 현재 진행중인 루틴
    IEnumerator nowRoutine;



    private void Start()
    {
        imgFireDamage = objFireDamage.GetComponent<SpriteRenderer>();
        MaxHealth = 200;
        Health = MaxHealth;

        onFire = false;
        nowLerp = 0.0f;
        nowAlpha = 0.0f;
        velocity = 1.0f;
    }

    private void Update()
    {
        if(onFire)
        {
            falseTime = 0;

            objFireDamage.SetActive(true);

            Health -= dropParam * Time.deltaTime;

            if (nowLerp < 0)
                velocity = 1.0f;
            if (nowLerp > 1)
                velocity = -1.0f;

            nowAlpha = Mathf.Lerp(minEfxAlpha, maxEfxAlpha, nowLerp);
            imgFireDamage.color = new Color(1, 1, 1, nowAlpha);

            nowLerp += Time.deltaTime / routineTime * velocity;

        }
        else
        {
            falseTime *= Time.deltaTime;
            if (falseTime > 1.0f)
                objFireDamage.SetActive(false);
        }

        
    }

    public void ChangeRoutine(int _fireLevel)
    {
        // 실행중인 코루틴이 있다면 강제로 끊는다
        if (nowRoutine != null)
            StopCoroutine(nowRoutine);

        // 레벨 -1의 입력이 들어왔을 경우 (이펙트 종료 입력)
        // 새 루틴을 생성하지 않고 데미지 필터를 비활성화 한다.
        if(_fireLevel == -1)
        {
            objFireDamage.SetActive(false);
            return;
        }

        // 레벨에 맞는 수치를 재조정
        switch(_fireLevel)
        {
            case 0:
                minEfxAlpha = 0.1f;
                maxEfxAlpha = 0.2f;
                routineTime = 0.8f;
                break;

            case 1:
                minEfxAlpha = 0.2f;
                maxEfxAlpha = 0.3f;
                routineTime = 0.8f;
                break;

            case 2:
                minEfxAlpha = 0.3f;
                maxEfxAlpha = 0.6f;
                routineTime = 0.65f;
                break;

            case 3:
                minEfxAlpha = 0.4f;
                maxEfxAlpha = 0.8f;
                routineTime = 0.5f;
                break;
        }

        // 데미지 필터를 활성화 시키고
        objFireDamage.SetActive(true);

        // 새 루틴을 시작
        nowRoutine = DamageEfxRoutine();
        StartCoroutine(nowRoutine);
    }

    // 화염 피격 데미지 이펙트 효과 루틴
    // 설정된 mixEfxAlpha ~ maxEfxAlpha 사이를 routineTime에 맞춰 보간한다.
    IEnumerator DamageEfxRoutine()
    {
        float nowLerp = 0.0f;
        float nowAlpha;
        float velocity= 1.0f;

        while(true)
        {
            if (nowLerp < 0)
                velocity = 1.0f;
            if (nowLerp > 1)
                velocity = -1.0f;

            nowAlpha = Mathf.Lerp(minEfxAlpha, maxEfxAlpha, nowLerp);
            imgFireDamage.color = new Color(1, 1, 1, nowAlpha);

            nowLerp += Time.deltaTime / routineTime * velocity;

            yield return null;
        }

        // 도달하지 못하고 ChangeRoutine() 에서 해제된다.
        yield break;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Fire"))
            return;


        onFire = true;
        SetParam(collision.GetComponent<FireEfx>().nowLevel);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Fire"))
            return;


        onFire = true;
        SetParam(collision.GetComponent<FireEfx>().nowLevel);

        //hpSystem.ChangeRoutine(nowLevel);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Fire"))
            return;

        onFire = false;
        //hpSystem.ChangeRoutine(-1);
    }
}
