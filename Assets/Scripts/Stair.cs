using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stair : MonoBehaviour {

    public bool playerOnStair = false;

    public float timerMax = 1.5f;
    public float timerMin = 1f;
	public PlayerController pc;
    float realTimer;
    //BOTONES
    private bool btnA = true;
    private bool btnB = false;
    //Sprite o image
    public Image btnASprite;
    public Image btnBSprite;
    public GameObject pisoSuperior;

    //FALTA RANDOM QUE ACTIVE O DESACTIVE AMBOS BOTONES AL TIEMPO?

    void Start()
    {
        realTimer = Random.Range(timerMin, timerMax);
		gameObject.tag = "Escalera";
    }

    void Update()
    {
        if (playerOnStair)
        {
            TimerThings();
			if (ButtonCheck()) 
            {
				pc.MoveVertical(1);
			}
            else
            {
				//pc.MoveVertical(-1);
			}
        }
    }

    bool ButtonCheck(){
		bool check = false;
		if (btnASprite.enabled) 
        {
			if (Input.GetButtonDown("Fire1"))
            {
				check = true;
			}
		}
		if (btnBSprite.enabled) 
        {
			if (Input.GetButtonDown("Fire2"))
            {
				check = true;
			}
		}
		return check;
    }
    void TimerThings(){

        realTimer -= Time.deltaTime;
        if (realTimer <= 0)
        {
            FlipFLop();
            realTimer += 1;
        }
    }

    void FlipFLop()
    {
        btnASprite.enabled = !btnASprite.enabled;
        btnBSprite.enabled = !btnBSprite.enabled;
    }

    void EnableBtn()
    {
        btnASprite.enabled = btnA;
        btnBSprite.enabled = btnB;
    }

    void DisableBtn()
    {
        btnASprite.enabled = false;
        btnBSprite.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
			//EnableStairOutside(col);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
			DisableStairOutside(col);
        }
    }

	public void EnableStairOutside(Collider2D col){
		pc = col.gameObject.GetComponent<PlayerController>();
		pc.EnterStair();
		playerOnStair = true;
		EnableBtn ();
	}
	
	public void DisableStairOutside(Collider2D col){

        pisoSuperior.collider2D.isTrigger = false;
		playerOnStair = false;
        if (pc != null)
        {
            pc.ExitStair();
            pc = null;
        }
		DisableBtn ();
	}
}
