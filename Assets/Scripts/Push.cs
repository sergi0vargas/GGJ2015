using UnityEngine;
using System.Collections;

public class Push : MonoBehaviour {

    public float UpForce = 1000;

    float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (timer < 1)
        {
            return;
        }
        timer = 0;
        PlayerController pc = col.gameObject.GetComponent<PlayerController>();
        pc.GetComponent<Rigidbody2D>().AddForce(Vector2.up * UpForce);
        Debug.Log("UP");
    }
}
