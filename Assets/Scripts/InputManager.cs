using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {



	void Start () {
		
	}

	void Update () {

		if (Input.GetButtonDown ("Fire")) {

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

		if (Input.GetButtonDown("MagneticField")) {
			EventsManager.MagneticFieldBtn ();
		}

		if (Input.GetAxisRaw ("Vertical") < 0 && Input.GetButtonDown ("Jump")) {
			EventsManager.ClimbDownCmd ();
		}

	}

	void FixedUpdate () {

		float horizontalMovement = Input.GetAxis ("Horizontal");

		EventsManager.HorizontalBtn (horizontalMovement);

	}
}
