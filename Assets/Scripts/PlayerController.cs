using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
public class PlayerController : MonoBehaviour {


    public float WalkingSpeed = 10;
    public float RunningSpeed = 20;
    public float BrokenLegsSpeed = 1;
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

        if (Application.loadedLevel == 3)
        {
            ChangeState(PlayerState.LegsBroken);
            anim.SetBool("Arrastandose", true);
        }
		if (Application.loadedLevel == 4)
		{
			ChangeState(PlayerState.LegsBroken);
			anim.SetBool("Arrastandose", true);
		}
	}

	void Update () {

        InputManager();
        
        #if !UNITY_IOS && !UNITY_ANDROID && !UNITY_WINRT
            if (Input.GetAxis("Horizontal") != 0)
            {
            MovementManager();
            }
            else
            {
                if (State != PlayerState.Jumping)
                    ChangeState(PlayerState.IdleNormal);
                anim.SetFloat("Speed", 0);
            }
        #else
            if (CrossPlatformInputManager.GetAxis("Horizontal") != 0)
                {
                    MovementManager(CrossPlatformInputManager.GetAxis("Horizontal"));
                }
            else
                {
                    if (State != PlayerState.Jumping)
                        ChangeState(PlayerState.IdleNormal);
                    anim.SetFloat("Speed", 0);
                }
        #endif

        if (transform.position.y <= minY)
        {
            StartCoroutine("Muere");
        }
	}

    void MovementManager()
    {
        if (Input.GetAxis("Horizontal") < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * CurrentSpeed * Time.deltaTime);
        if (State != PlayerState.Jumping)
            ChangeState(PlayerState.Caminando);

        anim.SetFloat("Speed", 1);
    }

    public void MovementManager(float x)
    {
        if (x != 0 && (x>0 || x<0))
        {
            if (x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            else
                transform.localScale = new Vector3(1, 1, 1);
            transform.Translate(Vector3.right * x * CurrentSpeed * Time.deltaTime);
            if (State != PlayerState.Jumping)
                ChangeState(PlayerState.Caminando);

            anim.SetFloat("Speed", 1);
        }
    }

    void JumpingManager()
    {
        if (isGrounded && State != PlayerState.LegsBroken)
        {
            ChangeState(PlayerState.Jumping);
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce * 100);
            isGrounded = false;
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
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Application.LoadLevel(4);
        }
    }
    void FixedUpdate()
    {
        #if UNITY_IOS || UNITY_ANDROID || UNITY_WINRT
            if(CrossPlatformInputManager.GetButtonDown("Jump"))
                JumpingManager();
        #else
        if(Input.GetButtonDown("Jump"))   
            JumpingManager();
        #endif
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
                GameObject.FindGameObjectWithTag("LevelMusic").GetComponent<AudioSource>().GetComponent<AudioSource>().pitch = 0.8f;
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
			if (col.relativeVelocity.y < legBreakingSpeed && Application.loadedLevel != 1) {
                ChangeState(PlayerState.LegsBroken);
                anim.SetBool("Arrastandose", true);
                GetComponent<AudioSource>().Play();
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
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    public void ExitStair()
    {
        isGrounded = true;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}
