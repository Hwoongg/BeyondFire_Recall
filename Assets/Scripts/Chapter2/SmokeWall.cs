using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeWall : MonoBehaviour
{
    [SerializeField] Dialogue dialogue;
    bool isMoving;
    [SerializeField] float moveSpeed = 1.0f;

    private void Start()
    {
        isMoving = false;
    }
    private void Update()
    {
        if (isMoving)
        {
            transform.Translate(moveSpeed * Time.deltaTime * -1, 0, 0);
        }
    }

    public void StartMove()
    {
        isMoving = true;
        Destroy(GetComponent<BoxCollider2D>());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DialogueManager.Instance().StartDialogue(dialogue);
    }

}
