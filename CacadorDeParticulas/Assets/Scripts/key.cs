using UnityEngine;
using System.Collections;

public class key : MonoBehaviour {

	bool up = true;
	float scale = 1.0f;

	float variation = 0.005f;
	float maxScale = 1.2f;
	float minScale = 1.0f;


	public SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (scale >= maxScale)
			up = false;
		if (scale <= minScale)
			up = true;

		if (up) {
			scale += variation;
		} else {
			scale -= variation;
		}

		sr.transform.localScale = new Vector3(scale, scale, scale);
	}
}
