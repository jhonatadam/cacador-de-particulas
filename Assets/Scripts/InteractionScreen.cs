using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScreen : MonoBehaviour {

	public SpriteRenderer arrow;
	public GameObject screen;

	public ContactCheck playerCheck;

	private bool isShown = false;

	// Use this for initialization
	void Start () {
		arrow.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		arrow.enabled = playerCheck.getIsInContact ();
		if (isShown && !arrow.enabled) {
			Destroy (screen);
			isShown = false;
		}
	}

	public void showScreen() {
		if(playerCheck.getIsInContact()) {
			isShown = true;
			Instantiate (screen);
		}
	}
}
