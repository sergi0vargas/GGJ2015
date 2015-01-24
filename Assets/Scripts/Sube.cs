using UnityEngine;
using System.Collections;

public class Sube : MonoBehaviour {

	public Stair escalera;

	void Start(){

		escalera = GameObject.FindGameObjectWithTag("Escalera").GetComponent<Stair>();
	}

	void OnTriggerEnter2D(Collider2D col){

		if (col.gameObject.tag.Equals ("Player")) {

			escalera.EnableStairOutside(col);
				}
	}

}
