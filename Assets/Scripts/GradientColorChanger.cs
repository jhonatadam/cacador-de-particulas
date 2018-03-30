using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradientColorChanger : MonoBehaviour {

	public Gradient color;
	private SpriteRenderer sr;
	public float startChange;
	public float endChange;
	private float dChange;
	private float time = 0;
	// Use this for initialization
	void Start () {	
		sr = GetComponent<SpriteRenderer> ();
		dChange = endChange - startChange;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startChange && Time.time < endChange) {
			time += Time.deltaTime;
			float p = time * (1 / dChange);
			sr.color = color.Evaluate (p);
		}
	}
}
