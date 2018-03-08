﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorTimer : MonoBehaviour {

    private Text text;
    private AudioSource countdown;
    private AudioSource start;
    private int time = 20;
    private int fontsize = 250;
    private float delta = -0.2f;

	void Start () {
        text = transform.GetComponent<Text>();
        countdown = transform.GetChild(0).GetComponent<AudioSource>();
        start = transform.GetChild(1).GetComponent<AudioSource>();
    }
	
	void Update () {
        delta += Time.deltaTime;
        if (delta >= 1f)
        {
            delta = 0f;
            time -= 1;
            text.text = time.ToString();
            if (time == 15)
            {
                IEnumerator toOrange = Fade(text.color, new Color(0.923f, 0.509f, 0.011f), Time.time, 4f);
                StartCoroutine(toOrange);
            }

            if (time == 8)
            {
                IEnumerator toGreen = Fade(text.color, new Color(0.008f, 0.503f, 0.012f), Time.time, 4f);
                StartCoroutine(toGreen);
            }
            if (time < 10 && time > 0)
            {
                StartCoroutine("Bounce");
                countdown.Play();
            }

            if (time <= 0)
            {
                SceneManager.LoadScene("UnderwaterMap");
            }
        }
        if (time == 2 && delta >= 0.1f)
        {
            start.Play();
        }
    }

    IEnumerator Bounce()
    {
        for (int i =1; i < 10; i++)
        {
            text.fontSize = fontsize + i * 3;
            yield return null;
        }

        for (int i = 1; i < 10; i++)
        {
            text.fontSize = fontsize - i * 3;
            yield return null;
        }
    }

    IEnumerator Fade(Color oldcolor, Color newcolor, float starttime, float delta)
    {
        float frac = 1f;
        while (frac != 0f)
        {
            float curr = Time.time;
            frac = Mathf.Max((starttime + delta - Time.time) / delta, 0);
            Color c = Color.Lerp(newcolor, oldcolor, frac);
            text.color = c;

            yield return null;
        }
    }
}