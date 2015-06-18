using UnityEngine;
using System.Collections;

public class TakeWeelChair : MonoBehaviour {


	void OnTriggerEnter2D (Collider2D col) {
	

		if (col.gameObject.tag.Equals ("Player")) {
			PlayerController pc = col.gameObject.GetComponent<PlayerController>();
			pc.anim.SetBool("enSilla",true);
			pc.CurrentSpeed = pc.RunningSpeed;
            GameObject.FindGameObjectWithTag("LevelMusic").GetComponent<AudioSource>().GetComponent<AudioSource>().pitch = 1.1f;
			Destroy(this.gameObject);
				}

	}
}
