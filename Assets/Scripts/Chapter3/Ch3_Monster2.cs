using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch3_Monster2 : MonoBehaviour {

    // 오른쪽가는애
    public GameObject Monster1;

    // 왼쪽가는애
    public GameObject Monster2;

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

        float destPosX1 = Monster1.transform.position.x;
        destPosX1 += 1;

        float destPosX2 = Monster2.transform.position.x;
        destPosX2 -= 1;
        while (true)
        {
            Monster1.transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
            Monster2.transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);

            if (Monster1.transform.position.x > destPosX1)
                break;

            yield return null;
        }

        Monster1.SetActive(false);
        Monster2.SetActive(false);

        gameObject.SetActive(false);

        yield break;
    }
}
