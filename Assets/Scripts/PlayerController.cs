using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public float speed = 10;
    public float jumpForce = 10;

    private bool isGrounded = true;

    private float minY = -20;//si baja mas muere (reinicia?)

	void Update () {

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
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
           rigidbody2D.AddForce(Vector2.up * jumpForce *  100);
           isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Level"))
        {
            isGrounded = true;
        }
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
