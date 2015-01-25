using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float WalkingSpeed = 10;
    public float RunningSpeed = 20;
    public float BrokenLegsSpeed = 2;
    public float StairsSpeed = 10;
    public float StairsDownSpeed = .5f;

	public float CurrentSpeed = 10;
    public float jumpForce = 8;

	public float legBreakingSpeed = -18;

	public Animator anim;

	public enum PlayerState
	{
		IdleNormal,
		IdleBrokenLegs,
		Caminando,
		LegsBroken,
		Jumping
	}

	public PlayerState State;

    private bool isGrounded = true;

    private float minY = -20; //si baja mas muere (reinicia?)

	void Awake() {
		State = PlayerState.Caminando;
		anim = GetComponent<Animator> ();
	}

	void Update () {

        InputManager();

		if ( Input.GetAxis("Horizontal") !=0 ) {
			if(Input.GetAxis("Horizontal") <0)
				transform.localScale = new Vector3(-1,1,1);
			else
				transform.localScale = new Vector3(1,1,1);
            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * CurrentSpeed * Time.deltaTime);
			if(State != PlayerState.Jumping )
                ChangeState(PlayerState.Caminando);

            anim.SetFloat("Speed", 1);
		}else{
            if (State != PlayerState.Jumping)
			    ChangeState(PlayerState.IdleNormal);
            anim.SetFloat("Speed", 0);
		}

        if (transform.position.y <= minY)
        {
            StartCoroutine("Muere");
        }
	}

    public void MoveVertical(int direction)
    {
        float newY = 0;
        newY = direction * StairsSpeed * Time.deltaTime;
        if(direction == 1){
            newY = direction * StairsSpeed * Time.deltaTime;
            transform.Translate(0, newY, 0);
        }
        else
        {
            newY = direction * StairsDownSpeed * Time.deltaTime;
            transform.Translate(0, newY, 0);
        }
    }

    void InputManager()
    {
        // Reinicia el nivel con "R"
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Application.LoadLevel(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Application.LoadLevel(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Application.LoadLevel(3);
        }
    }
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && State != PlayerState.LegsBroken)
        {
           ChangeState(PlayerState.Jumping);
           rigidbody2D.AddForce(Vector2.up * jumpForce *  100);
           isGrounded = false;
        }
    }

    void ChangeState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.IdleBrokenLegs:
                break;
			case PlayerState.IdleNormal:
				if (State == PlayerState.LegsBroken) {
				return;
				}
				break;
            case PlayerState.Caminando:
				if (State == PlayerState.LegsBroken) {
					return;
				}
                CurrentSpeed = WalkingSpeed;
                break;
            case PlayerState.LegsBroken:
                CurrentSpeed = BrokenLegsSpeed;
                audio.Play();
                break;
            case PlayerState.Jumping:
                anim.SetBool("Saltando", true);
                break;
            default:
                break;
        }

		State = state;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Level"))
        {
            isGrounded = true;
			if (col.relativeVelocity.y < legBreakingSpeed) {
                ChangeState(PlayerState.LegsBroken);
                anim.SetBool("Arrastandose", true);
            }
            else
            {
                ChangeState(PlayerState.IdleNormal);
                anim.SetBool("Arrastandose", false);
            }
            anim.SetBool("Saltando", false);
        }
    }

    IEnumerator Muere()
    {   //Quita gravedad y frena jugador - espera por la animacion de muerte y luego reinicia el nivel
        rigidbody2D.gravityScale = 0;
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(1);
        Application.LoadLevel(Application.loadedLevel);
    }

    IEnumerator Disappear()
    {        
        yield return new WaitForEndOfFrame();
    }

    public void EnterStair()
    {
        isGrounded = false;
        rigidbody2D.gravityScale = 0;
    }
    public void ExitStair()
    {
        isGrounded = true;
        rigidbody2D.gravityScale = 1;
    }
}
