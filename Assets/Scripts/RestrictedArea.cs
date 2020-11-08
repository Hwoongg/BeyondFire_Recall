using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictedArea : MonoBehaviour
{
    public Dialogue dlg;
    Transform playerTf;
    CharacterMover mover;
    Animator anim;
    Vector3 movePos;
    bool isMoving = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "GetInteraction")
        {
            if (!isMoving)
            {
                playerTf = collision.transform.parent;
                mover = playerTf.GetComponent<CharacterMover>();
                anim = playerTf.GetComponent<Animator>();
                mover.moveType = CharacterMover.MoveType.LOCK;
                anim.SetBool("IsWalk", false);
                // 진입한 방향의 반대쪽을 향하도록
                float dir = playerTf.localScale.x;
                movePos = transform.position; // 자동이동될 좌표
                movePos.x += dir;
                movePos.y = playerTf.position.y;
                movePos.z = playerTf.position.z;// 보정치 있을 경우 추가
                DialogueManager.Instance().StartDialogue(dlg);
                StartCoroutine(PlayerRestirict());
            }
        }
    }

    IEnumerator PlayerRestirict()
    {
        yield return new WaitUntil(() => 
        FindObjectOfType<DialogueManager>().canvasObj.activeSelf == false);
        

        // 플레이어 반대쪽으로 뒤집기
        playerTf.localScale = new Vector3(-playerTf.localScale.x, 1, 1);

        isMoving = true;
        while (true)
        {
            anim.SetBool("IsWalk", true);
            playerTf.transform.position =
                Vector2.MoveTowards(playerTf.position,
                movePos, 0.7f * Time.deltaTime);

            if (playerTf.position.x == movePos.x)
            {
                anim.SetBool("IsWalk", false);
                break;
            }
            yield return null;
        }

        mover.moveType = CharacterMover.MoveType.COMMANDMOVE;
        isMoving = false;
        yield break;
    }
}
