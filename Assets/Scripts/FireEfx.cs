using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
// 화염 이펙트 기능 구현 스크립트 입니다.
//

public class FireEfx : MonoBehaviour
{

    // 단계별 화염 프리팹
    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;
    public GameObject Level4;

    public GameObject FirePrefab;

    //public GameObject FireSlot;

    // 화염 단계 상승에 필요한 시간 (sec)
    public float delay;

    // 충돌 시 조절될 산소 게이지 오브젝트
    O2Gauge o2Gauge;
    HPSystem hpSystem;

    // 루틴 중단용 변수
    protected IEnumerator nowRoutine;

    [HideInInspector]
    public int nowLevel;

    // Enable 되면 실행되므로 화염 시작부분을 넣을 것
    public void Start()
    {
        Debug.Log("기본 화염 스크립트 시작 감지");
        Level1.SetActive(false);
        Level2.SetActive(false);
        Level3.SetActive(false);
        Level4.SetActive(false);
        nowLevel = 0;
        nowRoutine = FireEffect();
        StartCoroutine(nowRoutine);
        o2Gauge = FindObjectOfType<O2Gauge>();
        hpSystem = FindObjectOfType<HPSystem>();
    }

    // 좌우 체크 후 가능위치에 불씨 오브젝트 생성.
    void CreateFire()
    {
        // 좌우 LineCast 하여 Fire 또는 Wall이 있는지 검출.
        Vector2 RangeLeftStart = new Vector3(gameObject.transform.position.x - 0.6f, gameObject.transform.position.y + 0.5f);
        Vector2 RangeLeftEnd = new Vector3(gameObject.transform.position.x - 0.9f, gameObject.transform.position.y + 0.5f);
        Vector2 RangeRightStart = new Vector3(gameObject.transform.position.x + 0.6f, gameObject.transform.position.y + 0.5f);
        Vector2 RangeRightEnd = new Vector3(gameObject.transform.position.x + 0.9f, gameObject.transform.position.y + 0.5f);

        //RaycastHit2D[] Lefthits = Physics2D.LinecastAll(RangeLeftStart, RangeLeftEnd);
        //for(int i = 0; i < Lefthits.Length; i++)
        //{
        //    RaycastHit2D Lefthit = Lefthits[i];

        //    if (Lefthit.collider.tag == "Fire" || Lefthit.collider.tag == "Wall")
        //        return;
        //}
        //Instantiate(FirePrefab, RangeLeftEnd, transform.rotation);

        //RaycastHit2D[] Righthits = Physics2D.LinecastAll(RangeRightStart, RangeRightEnd);
        //for (int i = 0; i < Righthits.Length; i++)
        //{
        //    RaycastHit2D Righthit = Righthits[i];

        //    if (Righthit.collider.tag == "Fire" || Righthit.collider.tag == "Wall")
        //        return;
        //}
        //Instantiate(FirePrefab, RangeRightEnd, transform.rotation);

        if(LineCheck(RangeLeftStart,RangeLeftEnd))
        {
            RangeLeftEnd.y -= 0.5f;
            Instantiate(FirePrefab, RangeLeftEnd, transform.rotation);
        }

        if (LineCheck(RangeRightStart, RangeRightEnd))
        {
            RangeRightEnd.y -= 0.5f;
            Instantiate(FirePrefab, RangeRightEnd, transform.rotation);
        }

    }

    // 좌우 LineCast하여 생성 가능 여부 BOOL값 Return. 필요에 따라 분류
    // LineCast(시작점, 끝점, 레이어). RaycastHit2D return
    bool LineCheck(Vector2 RangeStart, Vector2 RangeEnd)
    {

        RaycastHit2D[] hits = Physics2D.LinecastAll(RangeStart, RangeEnd);
        for(int i=0;i<hits.Length;i++)
        {
            RaycastHit2D hit = hits[i];

            if (hit.collider.tag == "Fire" || hit.collider.tag == "Wall")
                return false;
        }
        return true;

    }

    IEnumerator FireEffect()
    {
        // 1단계로 시작.
        //GameObject temp = Instantiate(Level1, transform.position, transform.rotation);
        //temp.transform.SetParent(transform);
        //FireSlot = Level1;
        Level1.SetActive(true);
        nowLevel = 0;
        // delay만큼 대기 후 다음단계
        yield return new WaitForSeconds(delay);
        // 2단계 생성
        //DestroyObject(temp);
        //temp = Instantiate(Level2, transform.position, transform.rotation);
        //temp.transform.SetParent(transform);
        Level2.SetActive(true);
        Level1.SetActive(false);
        nowLevel = 1;





        // 다음단계 도달 절반시점에 개수증가
        yield return new WaitForSeconds(delay / 2);
        CreateFire();
        yield return new WaitForSeconds(delay / 2);
        // 3단계 생성
        //DestroyObject(temp);
        //temp = Instantiate(Level3, transform.position, transform.rotation);
        //temp.transform.SetParent(transform);
        Level3.SetActive(true);
        Level2.SetActive(false);
        nowLevel = 2;



        yield return new WaitForSeconds(delay);
        // 4단계 생성
        //DestroyObject(temp);
        //temp = Instantiate(Level4, transform.position, transform.rotation);
        //temp.transform.SetParent(transform);
        Level4.SetActive(true);
        Level3.SetActive(false);
        nowLevel = 3;


        yield break;
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("FireTrigger"))
    //        return;

    //    o2Gauge.onFire = true;
    //    o2Gauge.dropFireParam = (nowLevel + 1) * (nowLevel + 1);

    //    hpSystem.onFire = true;
    //    hpSystem.SetParam(nowLevel);
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("FireTrigger"))
    //        return;

    //    o2Gauge.onFire = true;
    //    o2Gauge.dropFireParam = (nowLevel + 1) * (nowLevel + 1);

    //    hpSystem.onFire = true;
    //    hpSystem.SetParam(nowLevel);

    //    //hpSystem.ChangeRoutine(nowLevel);
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("FireTrigger"))
    //        return;

    //    o2Gauge.onFire = false;

    //    hpSystem.onFire = false;
    //    //hpSystem.ChangeRoutine(-1);
    //}

}
