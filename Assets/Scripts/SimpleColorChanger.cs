using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleColorChanger : MonoBehaviour {
	public float startChange;
	public float endChange;
	public Color startColor;
	public Color finalColor;
	private float dChange; //delta change
	private SpriteRenderer sr;
	private float time = 0;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		dChange = endChange - startChange;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= startChange && Time.time <= endChange) {
			time += Time.deltaTime;
			float p = time * (1 / dChange);
			float p2 = 1 - p;
			sr.color = new Color (startColor.r * p2 + finalColor.r * p, startColor.g * p2 + finalColor.g * p, startColor.b * p2 + finalColor.b * p, startColor.a * p2 + finalColor.a * p);
		}
	}
}
