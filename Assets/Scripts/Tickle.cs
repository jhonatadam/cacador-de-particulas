using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tickle : MonoBehaviour {
	private SpriteRenderer sr;
	private Color originalColor;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		originalColor = sr.color;
	}
	
	// Update is called once per frame
	void Update () {
		float osc = (Mathf.Sin (Time.time) + 1) / 1.0f;
		float osc2 = (Mathf.Sin (Time.time*1-8) + 1) / 1.0f;
		print (osc);
		sr.color = new Color (originalColor.r, originalColor.g * osc, originalColor.b * osc2, osc2);
	}
}
