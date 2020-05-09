using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExing_Control : MonoBehaviour
{

    RectTransform rt;
    public bool Ready = false;
    bool PlayOn = false;
    public GameObject playBackGround;
    public Vector2 theTouch;
    float Width_;
    Vector2 Nomal_vector2;
    //public Vector2 theTouch2;

    // Use this for initialization
    void Start()
    {
        rt = GetComponent<RectTransform>();
        Width_ = Screen.width;
        Nomal_vector2 = new Vector2(rt.transform.position.x, rt.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos2 = Input.mousePosition;
        Vector2 theTouch2 = new Vector2(pos2.x, pos2.y);
      
        if (Input.touchCount > 0)
        {
            Vector2 pos = Input.GetTouch(0).position;    // 터치한 위치
            //Vector2 pos2 = Input.mousePosition;//Input.GetTouch(0).position;
            Vector2 theTouch = new Vector2(pos.x, pos.y);    // 변환 안하고 바로 Vector3로 받아도 되겠지.
            //Vector2 theTouch2 = new Vector2(pos2.x, pos2.y);    // 변환 안하고 바로 Vector3로 받아도 되겠지.

            //rt.transform.localScale = new Vector3(1.7f, 1.7f, 1);
            //Debug.Log(pos);

            //rt.transform.position = new Vector2(theTouch2.x, theTouch2.y);

            if (Input.GetTouch(0).phase == TouchPhase.Began && Ready)    // 딱 처음 터치 할때 발생한다
            {
                gameObject.transform.SetSiblingIndex(4);
                rt.transform.position = new Vector2(theTouch.x, theTouch.y);
                // 할거 하고
                PlayOn = true;
                playBackGround.SetActive(true);
                rt.transform.localScale = new Vector3(1.75f, 1.75f, 1);
            }

            else if (Input.GetTouch(0).phase == TouchPhase.Moved && PlayOn)    // 터치하고 움직이믄 발생한다.
            {
                rt.transform.position = new Vector2(theTouch.x, theTouch.y);
                Debug.Log("터치 이동 중");
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)    // 터치 따악 떼면 발생한다.
            {
                gameObject.transform.SetSiblingIndex(3);
                rt.transform.position = new Vector2(Nomal_vector2.x, Nomal_vector2.y);
                PlayOn = false;
                playBackGround.SetActive(false);
                rt.transform.localScale = new Vector3(1f, 1f, 1);
                
                // 할거 해라.
                //}
            }
        }
        else if (Input.GetMouseButton(0) && Ready)
                {
            gameObject.transform.SetSiblingIndex(4);
            rt.transform.position = new Vector2(theTouch2.x, theTouch2.y);
            PlayOn = true;
            playBackGround.SetActive(true);
            rt.transform.localScale = new Vector3(1.75f, 1.75f, 1);
        }

        else if (Input.GetMouseButtonUp(0))
        {
            gameObject.transform.SetSiblingIndex(3);
            rt.transform.position = new Vector2(Nomal_vector2.x, Nomal_vector2.y);
            //rt.transform.position = new Vector2(320, 270);
            PlayOn = false;
            playBackGround.SetActive(false);
            rt.transform.localScale = new Vector3(1, 1, 1);
        }
        //else
        //{
        //    Debug.Log("응 아니야");
        //    gameObject.transform.SetSiblingIndex(3);
        //    rt.transform.position = new Vector2(320, 270);
        //    playBackGround.SetActive(false);
        //}
    }

    void OnDisable()
    {
        gameObject.transform.SetSiblingIndex(3);
        rt.transform.position = new Vector2(Nomal_vector2.x, Nomal_vector2.y);
        Debug.Log(Width_);
        Debug.Log(Screen.width);
        playBackGround.SetActive(false);
        Ready = false;
        PlayOn = false;
        rt.transform.localScale = new Vector3(1f, 1f, 1);
    }

    public void ReadyTrue()
    {
        if (!Ready)
        {
            Ready = true;
            playBackGround.SetActive(true);
        }
    }

    public void ReadyFalse()
    {
        if (Ready)
        {
            Ready = false;
            playBackGround.SetActive(false);
        }
    }
}
