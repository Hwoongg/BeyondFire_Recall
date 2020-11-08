using System.Collections;
using UnityEngine;

public class Ch3_Monster3 : MonoBehaviour {

    //public GameObject Monster1;
    public GameObject Monster2;

    public float time;
    bool Looping;

    PannelController pannelController;
    Animator Monsteranimator;
    AudioSource Audio;

    bool isOnce = false;

    private void Start()
    {
        this.Audio = gameObject.GetComponent<AudioSource>();
        Looping = false;
        pannelController = new PannelController();
        pannelController.pannelRenderer = Monster2.GetComponent<SpriteRenderer>();
        Monsteranimator = Monster2.GetComponent<Animator>();
        Monsteranimator.SetBool("isStop", true);
        Monsteranimator.speed = 1.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOnce)
        {
            if (!Looping)
                StartCoroutine(MonsterFade());
            isOnce = true;
        }
    }
    

    IEnumerator MonsterFade()
    {
        Looping = true;
        this.Audio.Play();
        yield return StartCoroutine(pannelController.CustomFade(1f, 0.05f));
        
        // 사운드 출력
        // ...

        yield return new WaitForSeconds(0.3f);

        Monsteranimator.SetBool("isStop", false);
        yield return StartCoroutine(pannelController.CustomFade(0.04f));
        Monsteranimator.SetBool("isStop", true);

        Looping = false;

        yield break;
    }
}
