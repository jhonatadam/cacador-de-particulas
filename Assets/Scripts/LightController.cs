﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

	public bool isBugged;

	// recebe um valor de 0 à 1000
	public int bugProbability;

	public AudioClip bulbBlinkSound;

	private SpriteRenderer sr;
	private AudioSource audioSource; 


	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isBugged) {
			if (Random.Range (0, 1000) < bugProbability) {
				if (sr.enabled) {
					sr.enabled = false;
					//audioSource.PlayOneShot (bulbBlinkSound);
				}		
			} else {
				if (!sr.enabled) {
					sr.enabled = true;
				}
			}
		}
	}
}
