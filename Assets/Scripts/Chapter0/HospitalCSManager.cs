using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HospitalCSManager : MonoBehaviour {

    int ActionCount;
    bool isStop;

    float checkTime;
    // 제어에 필요한 변수들
    public GameObject Character;
    Animator CharacterAnimator;
    

    public GameObject objCamera;
    public GameObject objFadeEfx;
    
	void Start ()
    {
        ActionCount = 0;
        CharacterAnimator = Character.GetComponent<Animator>();
        
	}
	
	void Update ()
    {
        CameraMove();
        switch(ActionCount)
        {
            case 0:
                Event0();
                break;
            case 1:
                Event1();
                break;
            case 2:
                Event2();
                break;
            case 3:
                break;
        }
	}

    void Event0()
    {
        objFadeEfx.SetActive(true);
        objFadeEfx.GetComponent<FadeEffect>().FadeIn();

        ActionCount++;
    }

    void Event1()
    {
        if (objFadeEfx.GetComponent<FadeEffect>().MoveEnd)
        {
            CharacterAnimator.SetBool("IsSoom", true);
            ActionCount++;
        }
    }

    void Event2()
    {
        StartCoroutine("TackleRoutine");
        ActionCount++;
    }

    //void Event3()
    //{
    //    objFadeEfx.SetActive(true);
    //    objFadeEfx.GetComponent<FadeEffect>().FadeOut();
    //    FindObjectOfType<ChangeScene>().LoatCutSceneFireEdu();
    //}

    IEnumerator TackleRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        checkTime = Time.time;

        while (true)
        {
            Character.transform.localScale = new Vector3(-1, 1, 1);
            CharacterAnimator.SetBool("IsTackle", false);
            CharacterAnimator.SetBool("IsWalk", true);
            
            float x = 1 * Time.deltaTime * 1.0f;
            Character.transform.Translate(x, 0, 0);

            if (Character.transform.position.x > 0.55f)
            {
                CharacterAnimator.SetBool("IsWalk", false);
                CharacterAnimator.SetBool("IsSoom", true);
                yield return new WaitForSeconds(1.7f);
                GetComponent<AudioSource>().Play();
                yield return new WaitForSeconds(0.3f);

                CharacterAnimator.SetBool("IsSoom", false);
                CharacterAnimator.SetBool("IsTackle", true);
                
                Character.GetComponent<Rigidbody2D>().AddForce(new Vector3(200, 100, 0));
            }

            if(Time.time > checkTime + 10.0f)
            {
                objFadeEfx.GetComponent<FadeEffect>().FadeOut();
                SceneManager.LoadScene("CS_FireEdu");
                break;
            }
            yield return null;
        }

        yield break;
    }

    void CameraMove()
    {
        objCamera.transform.position = new Vector3(Character.transform.position.x, 0.9f, -10);
    }

    
    
}
