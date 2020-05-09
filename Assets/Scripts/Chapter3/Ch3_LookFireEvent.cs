using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3_LookFireEvent : MonoBehaviour
{

    public Dialogue dialogue;
    GameObject player;
    bool isActive;

	// Use this for initialization
	void Start ()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        isActive = true;
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(isActive)
        {
            isActive = false;

            StartCoroutine(LookFire());
        }
    }

    IEnumerator LookFire()
    {
        FindObjectOfType<CharacterMover>().moveType = CharacterMover.MoveType.NONE;

        // 플레이어 왼쪽방향 보기
        player.transform.localScale = new Vector3(1, 1, 1);

        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        yield return new WaitUntil(() => FindObjectOfType<DialogueManager>().objDialogueBox.activeSelf == false);

        FindObjectOfType<CharacterMover>().moveType = CharacterMover.MoveType.COMMANDMOVE;
        yield break;
    }
}
