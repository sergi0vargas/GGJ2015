using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public const int NUMBER_OF_LEVELS = 4;

    public int[] TimePerLevel = new int[NUMBER_OF_LEVELS + 1];

    public float timer = 30;
    public Text timerTxt;

    float tickTimer = 0;

	public string Level = "Level1";

    void Start()
    {
        timerTxt = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
        TimePerLevel[0] = 300;
        TimePerLevel[1] = 30;
        TimePerLevel[2] = 30;
        TimePerLevel[3] = 90;
        TimePerLevel[4] = 90;

        timer = TimePerLevel[Application.loadedLevel];
    }


	void Update () {
        timer -= Time.deltaTime;
        timerTxt.text = "Time Left: " + timer.ToString("f0");

        if (timer < 6 && timer >= 0)
        {
            tickTimer += Time.deltaTime;
            if (tickTimer >= 1)
            {
                tickTimer -= 1;
				audio.Play();
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
