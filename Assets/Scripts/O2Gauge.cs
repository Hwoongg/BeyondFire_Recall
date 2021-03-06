﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//
// 산소 게이지 조절 스크립트 입니다.
// 프로퍼티를 이용하여 불필요한 갱신을 제거.
//

public class O2Gauge : MonoBehaviour {

    public GameObject objGauge;
    private Slider gaugeSilder;

    private float GaugePersentage;
    private const float fMaxGauge = 500;

    [HideInInspector]    
    public float dropFireParam;
    [HideInInspector]
    public float dropIdleParam;

    [HideInInspector]
    public bool onFire;

    private CharacterMover characterMover;

    public GameObject objText;
    private Text persentageText;

    private float fGauge;
    public float Gauge
    {
        get
        {
            return fGauge;
        }
        set
        {
            fGauge = value;
            if(fGauge <= 0)
            {
                GameManager.Instance().SetNowScene();
                SceneManager.LoadScene("GameOver");
            }
            GaugePersentage = fGauge / fMaxGauge;
            gaugeSilder.value = GaugePersentage;
        }
    }
    
	void Start ()
    {
        fGauge = fMaxGauge;
        GaugePersentage = fGauge / fMaxGauge;
        gaugeSilder = objGauge.GetComponent<Slider>();
        onFire = false;
        dropFireParam = 1.0f;
        dropIdleParam = 1.0f;
        characterMover = FindObjectOfType<CharacterMover>();
        persentageText = objText.GetComponent<Text>();
	}

    private void Update()
    {
        // 화염에 의한 감소. (181206. HP 추가로 인한 제거)
        if (onFire)
        {
            Gauge -= dropFireParam * Time.deltaTime;
        }

        // 캐릭터 상태에 따른 감소량 결정
        float dropParam = 0;

        switch(characterMover.charState)
        {
            case CharacterMover.CharState.IDLE:
                dropParam = dropIdleParam;
                break;

            case CharacterMover.CharState.RESCUE:
                dropParam = dropIdleParam * 2;
                break;

            case CharacterMover.CharState.DASH:
                dropParam = dropIdleParam * 2;
                break;

            case CharacterMover.CharState.RESCUEDASH:
                dropParam = dropIdleParam * 4;
                break;
        }

        Gauge -= dropParam * Time.deltaTime;

        // Text Update
        persentageText.text = (int)(GaugePersentage * 100) + "%";
    }

    public void O2GaugePopUp()
    {

    }

    public float GetPercentage()
    {
        return GaugePersentage;
    }

}
