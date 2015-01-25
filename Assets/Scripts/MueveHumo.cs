using UnityEngine;
using System.Collections;

public class MueveHumo : MonoBehaviour {

	public float maxSpeed = 5;
	public float minSpeed = 1;
	private float speed;
	// Use this for initialization
	void Start () {
		speed = Random.Range (minSpeed, maxSpeed);
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Translate (Vector3.right * Time.deltaTime * speed);
		if (transform.position.x >= 65) {
				
			transform.position = new Vector3(-40 , transform.position.y, transform.position.z);
		}
	}
}
