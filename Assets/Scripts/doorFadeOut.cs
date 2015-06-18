using UnityEngine;
using System.Collections;

public class doorFadeOut : MonoBehaviour {

    SpriteRenderer doorSprite;
    public float doorFadeOutTomer = 0.1f;

	// Use this for initialization
	void Start () {

        doorSprite = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {

        doorSprite.color = Color.Lerp(doorSprite.color, new Color(doorSprite.color.r, doorSprite.color.g, doorSprite.color.b, 0),doorFadeOutTomer);
	}
}
