using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

	public bool isBugged;

	// recebe um valor de 0 à 1000
	public int bugProbability;

	private SpriteRenderer sr;
	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isBugged) {
			if (Random.Range (0, 1000) < bugProbability) {
				if (sr.enabled) {
					sr.enabled = false;
				}		
			} else {
				if (!sr.enabled) {
					sr.enabled = true;
				}
			}
		}
	}
}
