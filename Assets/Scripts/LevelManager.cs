using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public float timer = 30;
    public Text timerTxt;

	public string Level = "Level1";

    void Start()
    {
        timerTxt = GameObject.FindGameObjectWithTag("Timer").GetComponent<Text>();
    }


	void Update () {

        timer -= Time.deltaTime;
        timerTxt.text = "Time Left: " + timer.ToString("f0");

        if (timer <= 0)
        {
			//var player = GameObject.FindGameObjectWithTag("Player");
			// Muere y reinicia el nivel
        }
	}

    void Perdio()
    {
        ///TEMP
        Time.timeScale = 0;

    }


}
