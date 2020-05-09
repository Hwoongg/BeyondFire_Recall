﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ch2_WallSound : MonoBehaviour {
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
    }
}
