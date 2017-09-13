using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	private bool canListen = true;

	void Start () {
		
	}

	void Update () {

		if (canListen) {
			if (Input.GetButton ("Fire")) {
				
				EventsManager.FireBtn ();
			}

			if (Input.GetButtonDown ("Jump")){
				EventsManager.JumpBtn ();

			}

			if (Input.GetButtonDown ("Dash")) {
				EventsManager.DashBtn ();

			}

			if (Input.GetAxisRaw ("Vertical") > 0) {
				//Chama o evento Interact
				EventsManager.Interact ();
			}

			if (Input.GetAxisRaw ("Vertical") < 0) {
				//Chama o evento VerticalDown
				EventsManager.VerticalDown ();
			}

			if (Input.GetButtonDown ("Start")) {
				EventsManager.StartBtn ();
			}

			if (Input.GetButtonDown ("TotalMap")) {
				EventsManager.TotalMapBtn ();
			}

			if (Input.GetButtonDown("MagneticField")) {
				EventsManager.MagneticFieldBtn ();
			}

			if (Input.GetButtonDown ("Jetpack")) {
				EventsManager.JetpackBtn ();
			}

			if (Input.GetAxisRaw ("Vertical") < 0 && Input.GetButtonDown ("Jump")) {
				EventsManager.ClimbDownCmd ();
			}

		}

	}

	void FixedUpdate () {


		if (Input.GetButton ("Jump")) {
			EventsManager.JumpBtnHold ();
		}


		if (canListen) {
			
			float horizontalMovement = Input.GetAxis ("Horizontal");

			EventsManager.HorizontalBtn (horizontalMovement);

		}

	}

	public void StopListening() {
		canListen = false;
	}

	public void StartListening() {
		canListen = true;
	}

	private void OnEnable() {
		EventsManager.onCutsceneStart += StopListening;
		EventsManager.onCutsceneEnd += StartListening;
	}

	private void OnDisable() {
		EventsManager.onCutsceneStart -= StopListening;
		EventsManager.onCutsceneEnd -= StartListening;
	}
}
