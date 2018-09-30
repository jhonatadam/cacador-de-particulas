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
		awakeTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
        awakeTime += Time.deltaTime;
		if (awakeTime > startChange && awakeTime < endChange) {
			time = awakeTime - startChange;
			float p = time * (1 / dChange);
			sr.color = color.Evaluate (p);
		}
		if (loop) {
			if (awakeTime > endChange) {
                awakeTime = startChange;
			}
		}
		if (!loop && awakeTime > endChange) {
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
    public void Reset() {
        awakeTime = 0;
        sr.color = color.Evaluate(0);
    }
}
