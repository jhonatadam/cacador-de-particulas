using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScreen : MonoBehaviour {

	public SpriteRenderer arrow;
	public GameObject screen;

	public ContactCheck playerCheck;

	private bool locked = false;

	private GameObject screenTemp;

	// Use this for initialization
	void Start () {
		arrow.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		arrow.enabled = playerCheck.getIsInContact ();

	}

	public void showScreen() {
		if(playerCheck.getIsInContact() && !locked) {
			screenTemp = Instantiate (screen) as GameObject;
		}
	}

	private void DeleteScreen() {
		if (screenTemp != null)
			Destroy (screenTemp);
	}

	private void LockScreen() {
		locked = true;
	}

	private void UnlockScreen() {
		locked = false;
	}

	private void OnEnable() {
		EventsManager.onInteract += showScreen;
		EventsManager.onScreenShown += LockScreen;
		EventsManager.onScreenDismissed += UnlockScreen;
		EventsManager.onScreenDismissed += DeleteScreen;
	}

	private void OnDisable() {
		EventsManager.onInteract -= showScreen;
		EventsManager.onScreenShown -= LockScreen;
		EventsManager.onScreenDismissed -= UnlockScreen;
		EventsManager.onScreenDismissed -= DeleteScreen;
	}
}
