using UnityEngine;
using System.Collections;

public class ResetSmoke : MonoBehaviour {


	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.tag.Equals ("Humo")) {
			Debug.Log("OK");
			col.transform.position = new Vector3(-col.transform.position.x, col.transform.position.y, col.transform.position.z);
				}
	}
}
