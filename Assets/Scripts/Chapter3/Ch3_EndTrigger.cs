using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ch3_EndTrigger : InteractionSystem {

    public override void doAction()
    {
        var mover = FindObjectOfType<CharacterMover>();
        if(mover)
        {
            if(mover.charState == CharacterMover.CharState.RESCUE)
            {
                StartCoroutine(Routine());
            }
        }
        
    }

    public override void upAction()
    {
        doAction();
    }

    public override void downAction()
    {
        doAction();
    }

    IEnumerator Routine()
    {
        FindObjectOfType<FadeEffect>().FadeOut();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("Ending_Interview");
    }
}
