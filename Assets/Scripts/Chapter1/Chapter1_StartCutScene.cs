using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter1_StartCutScene : MonoBehaviour {

    public Dialogue dialogue1;
    public Dialogue dialogue2;

    public GameObject objSiren;
    public GameObject objFadeEfx;
    private FadeEffect fadeEfx;
    DialogueManager dm;

	// Use this for initialization
	void Start () {
        fadeEfx = objFadeEfx.GetComponent<FadeEffect>();
        dm = DialogueManager.Instance();
        StartCoroutine(Chapter1StartCutScene());
        
	}
	
	IEnumerator Chapter1StartCutScene()
    {
        objFadeEfx.SetActive(true);
        fadeEfx.FadeIn();
        dm.StartDialogue(dialogue1);
        yield return new WaitUntil(() => dm.canvasObj.activeSelf == false);

        objSiren.SetActive(true);
        dm.StartDialogue(dialogue2);
        yield return new WaitUntil(() => dm.canvasObj.activeSelf == false);

        
        fadeEfx.FadeOut();
        
        yield return new WaitForSeconds(4.0f);

        SceneManager.LoadScene("Chapter1");
        yield break;
    }
}
