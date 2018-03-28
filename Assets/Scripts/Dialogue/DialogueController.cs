using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour {
	public float textFrequency = 0.1f;
	public string[] dialogues;
	public int currentDialogue = 0;
	bool finished = false;
	bool animating = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void textAnimation(){

	}

	void nextDialogue(){
		if (currentDialogue < dialogues.Length - 1) {
			currentDialogue++;
		} else {
			finished = true;
		}
	}
}
