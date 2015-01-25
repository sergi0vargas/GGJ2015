using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour {

	public float timer = 0;

	
	// Update is called once per frame
	void Update () {
	
		timer += Time.deltaTime;
		if (timer > 3) {
			Application.LoadLevel(1);
				}
	}
}
