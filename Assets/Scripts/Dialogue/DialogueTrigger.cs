using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

	public Dialogue dialogue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			if (!dialogue.over) {
				dialogue.Activate ();
				EventsManager.DialogueStart ();
			}
			Destroy (this.gameObject);
		}

	}
}
