using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Chapter2Manager : MonoBehaviour {
    public GameObject objCamera;

    public GameObject objFadeEfx;
    private FadeEffect fadeEfx;

    private bool isFading;

    public GameObject amb;
    public GameObject fir;
    //public GameObject street;
    public Vector3 saveVector3;
    public GameObject High;
    public bool walkTime = false;

	// Use this for initialization
	void Start () {
        isFading = false;
        fadeEfx = objFadeEfx.GetComponent<FadeEffect>();

        objFadeEfx.SetActive(true);
        fadeEfx.FadeIn();

        StartCoroutine("CameraMove");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(objCamera.transform.position.x >= -8.2f)
        {
            objCamera.transform.Translate(Vector3.left * Time.deltaTime * 0.6f);
            if (objCamera.transform.position.x <= -7.7f && objCamera.transform.position.x >= -8.2f)
                if (!isFading)
                {
                    isFading = true;
                    fadeEfx.FadeOut();
                    amb.GetComponent<AudioSource>().Stop();
                    fir.GetComponent<AudioSource>().Stop();
                    //street.GetComponent<AudioSource>().Stop();

                    SceneManager.LoadScene("Ch2_SubwayReady");
                }

        }
        else if(objCamera.transform.position.x <= -7.7f)
        {
            objCamera.transform.Translate(Vector3.right * Time.deltaTime * 0.6f);
            {
                if (objCamera.transform.position.x <= -7.7f && objCamera.transform.position.x >= -8f)
                    if (!isFading)
                    {
                        isFading = true;
                        fadeEfx.FadeOut();
                        amb.GetComponent<AudioSource>().Stop();
                        fir.GetComponent<AudioSource>().Stop();
                        //street.GetComponent<AudioSource>().Stop();
                    }
            }
        }
        if (walkTime == true)
        {
            High.transform.Translate(Vector3.right * Time.deltaTime * 1.5f);
        }
	}

    IEnumerator CameraMove()
    {
        yield return new WaitForSeconds(4f);
        saveVector3 = objCamera.transform.position;
        objCamera.transform.position = new Vector3(-19.5f, -9.0f, -10f);
        walkTime = true;
        yield return new WaitForSeconds(2f);
        walkTime = false;
        objCamera.transform.position = saveVector3;
        yield return new WaitForSeconds(2f);
        saveVector3 = objCamera.transform.position;
        objCamera.transform.position = new Vector3(-18.3f, -9.0f, -10f);
        walkTime = true;
        yield return new WaitForSeconds(1.5f);
        walkTime = false;
        objCamera.transform.position = saveVector3;
        yield return new WaitForSeconds(1.5f);
        saveVector3 = objCamera.transform.position;
        objCamera.transform.position = new Vector3(-17.7f, -9.0f, -10f);
        walkTime = true;
        yield return new WaitForSeconds(1f);
        walkTime = false;
        objCamera.transform.position = saveVector3;
        //yield return new WaitForSeconds(0.1f);
        //saveVector3 = objCamera.transform.position;
        //objCamera.transform.position = new Vector3(-17.55f, -9.0f, -10f);
        //yield return new WaitForSeconds(0.1f);
        //objCamera.transform.position = saveVector3;
        //yield return new WaitForSeconds(0.1f);
        //saveVector3 = objCamera.transform.position;
        //objCamera.transform.position = new Vector3(-17.55f, -9.0f, -10f);
        //yield return new WaitForSeconds(0.1f);
        //objCamera.transform.position = saveVector3;
        //yield return new WaitForSeconds(0.1f);
        //saveVector3 = objCamera.transform.position;
        //objCamera.transform.position = new Vector3(-17.55f, -9.0f, -10f);
        //yield return new WaitForSeconds(0.1f);
        //objCamera.transform.position = saveVector3;
        //yield return new WaitForSeconds(0.1f);
        //saveVector3 = objCamera.transform.position;
        //objCamera.transform.position = new Vector3(-17.55f, -9.0f, -10f);
        //yield return new WaitForSeconds(0.1f);
        //objCamera.transform.position = saveVector3;


    }
}
