using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Stair : MonoBehaviour {

    public bool playerOn = false;

    public float timer = 1.2f;
    private float realTimer;
    //BOTONES
    private bool btnA = true;
    private bool btnB = false;
    //Sprite o image
    public Image btnASprite;
    public Image btnBSprite;

    //FALTA RANDOM QUE ACTIVE O DESACTIVE AMBOS BOTONES AL TIEMPO?

    void Start()
    {
        realTimer = timer;
    }

    void Update()
    {
        if (playerOn)
        {
            realTimer -= Time.deltaTime;
            if (realTimer <= 0)
            {
                FlipFLop();
                realTimer = timer;
            }
        }
    }

    void FlipFLop()
    {
        if (btnA)
        {
            btnASprite.color = new Color(btnASprite.color.r,btnASprite.color.g,btnASprite.color.b, 255);
            btnA = !btnA;
        }
        else
        {
            btnASprite.color = new Color(btnASprite.color.r, btnASprite.color.g, btnASprite.color.b, 100);
            btnA = !btnA;
        }

        if (btnB)
        {
            btnBSprite.color = new Color(btnBSprite.color.r, btnBSprite.color.g, btnBSprite.color.b, 255);
            btnB = !btnB;
        }
        else
        {
            btnBSprite.color = new Color(btnBSprite.color.r, btnBSprite.color.g, btnBSprite.color.b, 100);
            btnB = !btnB;
        }
    }

    void EnableBtn()
    {
        btnASprite.enabled = true;
        btnBSprite.enabled = true;
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
            col.gameObject.GetComponent<PlayerController>().EnterStair();
            playerOn = true;
            EnableBtn();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            playerOn = false;
            col.gameObject.GetComponent<PlayerController>().ExitStair();
            DisableBtn();
        }
    }
}
