using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

	public bool isBugged;

	// recebe um valor de 0 à 1000
	public int bugProbability;

	public float redProbability = 0.5f;

	public AudioClip bulbBlinkSound;

	private SpriteRenderer sr;
	private AudioSource audioSource; 

	public ContactCheck cc;
	private ParticleSystem ps;

	public string color = "white";

	public float lastChange = 0;
	public float changeDelay = 10.0f;
	private float realChangeDelay = 10.0f;
	private Color originalColor;
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		originalColor = sr.color;
		//audioSource = GetComponent<AudioSource> ();
		ps = GetComponentInChildren<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isBugged) {
			if (Random.Range (0, 1000) < bugProbability) {
				if (sr.enabled) {
					sr.enabled = false;
					if (cc.getIsInContact ()) {
						//audioSource.PlayOneShot (bulbBlinkSound);
					}
				}		
			} else {
				if (!sr.enabled) {
					sr.enabled = true;
				}
			}
		}
		if (Time.time - lastChange > realChangeDelay) {
			if (Random.Range (0.0f, 1.0f) < redProbability) {
				if (color == "white") {
					sr.color = new Color (1.0f, 0, 0.2f);
					color = "red";
					var main = ps.main;
					main.startColor = new Color (1.0f, 0, 0.2f);
					ps.Emit (Random.Range (10, 30));
				} else {
					sr.color = originalColor;
					color = "white";
					var main = ps.main;
					main.startColor = new Color (1.0f, 1.0f, 1.0f);
					ps.Emit (Random.Range (10, 30));
				}
			} else {
				if (color == "white") {
					sr.color = new Color (0.2f, 0, 1.0f);
					color = "blue";
					var main = ps.main;
					main.startColor = new Color (0.2f, 0, 1.0f);
					ps.Emit (Random.Range (10, 30));
				} else {
					sr.color = originalColor;
					color = "white";
					var main = ps.main;
					main.startColor = new Color (1.0f, 1.0f, 1.0f);
					ps.Emit (Random.Range (10, 30));
				}
			}
			lastChange = Time.time;
			realChangeDelay = Random.Range (changeDelay, changeDelay*2);
		}
	}
}
