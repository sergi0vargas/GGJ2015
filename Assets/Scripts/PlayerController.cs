using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float WalkingSpeed = 10;
    public float RunningSpeed = 20;
    public float BrokenLegsSpeed = 2;

	public float speed = 10;
    public float jumpForce = 8;

	public float legBreakingSpeed = -18;

	public enum PlayerState
	{
		Normal,
		LegsBroken,
		Jumping
	}

	public PlayerState State;

    private bool isGrounded = true;

    private float minY = -20; //si baja mas muere (reinicia?)

	void Awake() {
		State = PlayerState.Normal;
	}

	void Update () {

        // Reinicia el nivel con "R"
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

		if ( Input.GetAxis("Horizontal")!=0 ) {

            transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
		}

        if (transform.position.y <= minY)
        {
            StartCoroutine("Muere");
        }
	}

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && isGrounded && State != PlayerState.LegsBroken)
        {
           rigidbody2D.AddForce(Vector2.up * jumpForce *  100);
           isGrounded = false;
        }
    }

    void ChangeState(PlayerState state)
    {
        State = state;

        switch (state)
        {
            case PlayerState.Normal:
                speed = WalkingSpeed;
                break;
            case PlayerState.LegsBroken:
                speed = BrokenLegsSpeed;

                break;
            case PlayerState.Jumping:
                break;
            default:
                break;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Level"))
        {
            isGrounded = true;
			if (col.relativeVelocity.y < legBreakingSpeed) {
                ChangeState(PlayerState.LegsBroken);
			}
        }
		Debug.Log(State);
		Debug.Log(col.relativeVelocity);
    }

    IEnumerator Muere()
    {   //Quita gravedad y frena jugador - espera por la animacion de muerte y luego reinicia el nivel
        rigidbody2D.gravityScale = 0;
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(3);
        Application.LoadLevel(Application.loadedLevel);
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
