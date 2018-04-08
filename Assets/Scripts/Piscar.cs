using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piscar : MonoBehaviour {
	private SpriteRenderer sr;
	public Color cor1;
	public Color cor2;
	public float interval;
	public float frequency;
	private float last = 0;
	private int phase = 0;
	// Use this for initialization
	void Start () {
		
		sr = gameObject.GetComponent<SpriteRenderer> ();
		sr.color = new Color (cor1.r, cor1.g, cor1.b, cor1.a);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - last > frequency) {
			if (phase == 0) {
				sr.color = Color.Lerp(new Color (cor1.r, cor1.g, cor1.b, cor1.a), new Color (cor2.r, cor2.g, cor2.b, cor2.a), interval);
				phase = 1;
			} else if (phase == 1) {
				sr.color = Color.Lerp(new Color (cor2.r, cor2.g, cor2.b, cor1.a), new Color (cor1.r, cor1.g, cor1.b, cor1.a), interval);
				phase = 0;
			}
			last = Time.time;
		}
	}
}
