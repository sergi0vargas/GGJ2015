﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public const int NUMBER_OF_LEVELS = 7;

    int[] TimePerLevel = new int[NUMBER_OF_LEVELS];

    public float timer = 30;
    public Text timerTxt;

    float tickTimer = 0;

    void Start()
    {
        TimePerLevel[0] = 300;
        TimePerLevel[1] = 30;
        TimePerLevel[2] = 30;
        TimePerLevel[3] = 75;
        TimePerLevel[4] = 90;
        TimePerLevel[5] = 300;
        TimePerLevel[6] = 300;

		if(Application.loadedLevel != 0 || Application.loadedLevel != 5 || Application.loadedLevel != 6){
			timerTxt = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
			timer = TimePerLevel[Application.loadedLevel];
		}
        if (Application.loadedLevel == 1)
        {
            if (GameObject.FindGameObjectWithTag("LevelMusic") != null)
                GameObject.FindGameObjectWithTag("LevelMusic").GetComponent<AudioSource>().GetComponent<AudioSource>().pitch = 1f;
        }
    }


	void Update () {
        timer -= Time.deltaTime;
		if(timerTxt != null)
        timerTxt.text = "Time Left: " + timer.ToString("f0");

        if (timer < 6 && timer >= 0)
        {
            tickTimer += Time.deltaTime;
            if (tickTimer >= 1)
            {
                tickTimer -= 1;
				//audio.Play();
            }
        }

        if (timer <= 0)
        {
			//var player = GameObject.FindGameObjectWithTag("Player");
			// Muere y reinicia el nivel
            Application.LoadLevel(Application.loadedLevel);
        }
	}

    void Perdio()
    {
        ///TEMP
        Time.timeScale = 0;

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //var player = GetComponent<PlayerController>();
        //player.StartCoroutine("Disappear");
        StartCoroutine("GoToNextLevel");
    }

    IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(1);
        int level = Application.loadedLevel;
        if (level <= NUMBER_OF_LEVELS)
        {
            Application.LoadLevel(++level);
        }
    }
}
