using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3_Monster1 : MonoBehaviour
{
    
    public GameObject Monster;
    public float moveSpeed;

    bool Looping;

    private void Start()
    {
        Looping = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Looping)
            StartCoroutine(MonsterMove());
    }



    IEnumerator MonsterMove()
    {
        Looping = true;

        float destPosX = Monster.transform.position.x;
        destPosX += 1;

        while(true)
        {
            Monster.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);

            if (Monster.transform.position.x > destPosX)
                break;

            yield return null;
        }

        Monster.SetActive(false);
        gameObject.SetActive(false);

        yield break;
    }
}
