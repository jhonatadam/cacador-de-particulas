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
	public bool loop = false;
	private bool done = false;
	private float awakeTime = 0;
	// Use this for initialization
	void Start () {	
		sr = GetComponent<SpriteRenderer> ();
		dChange = endChange - startChange;
		awakeTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - awakeTime > startChange && Time.time - awakeTime < endChange) {
			time += Time.deltaTime;
			float p = time * (1 / dChange);
			sr.color = color.Evaluate (p);
		}
		if (loop) {
			if (Time.time - awakeTime > endChange) {
				startChange = endChange;
				endChange += dChange;
				time = 0;
			}
		}
		if (!loop && Time.time - awakeTime > endChange) {
			done = true;
		}
	}
	void OnDestroy(){
		color = null;
		sr = null;
	}
	public bool IsDone(){
		return done;
	}
}
