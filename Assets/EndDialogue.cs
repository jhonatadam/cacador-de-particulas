﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndDialogue : MonoBehaviour {
	private GameObject dialogue;
	private float timer = 0;
	public float delayForCredits = 4;
	public GameObject blackScreen;
	private bool fading = false;
	// Use this for initialization
	void Start () {
		dialogue = GameObject.Find ("Dialogue Final");
		blackScreen = GameObject.Find ("Black Screen");
	}
	
	// Update is called once per frame
	void Update () {
		if (fading) {
			timer += Time.deltaTime;
			if (timer > delayForCredits) {
				SceneManager.LoadScene ("Creditos");

			}
		}
		if (dialogue.GetComponent<Dialogue> ().over && !fading) {
			blackScreen.GetComponent<GradientColorChanger> ().enabled = true;
			blackScreen.GetComponent<GradientColorChanger> ().endChange = delayForCredits;
			fading = true;
		}
	}
}
