using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmDialogue : MonoBehaviour {

	public GameObject canvas;
	public string text;

	// Use this for initialization
	void Start () {
		canvas.SetActive (false);
	}

	public void ShowAlert() {
		canvas.SetActive (true);
		EventsManager.DialogueStart ();
		Time.timeScale = 0.0f;
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			ShowAlert ();
		}
	}
		
	public void OK() {
		Time.timeScale = 1.0f;
		EventsManager.DialogueEnd ();
		Destroy (this.gameObject);
	}
}
