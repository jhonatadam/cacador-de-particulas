using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public Player player;
	public Elevator[] elevators;
	public DoorPanel[] doorPanels;

	void Update () {

		if (Input.GetButtonDown ("Fire")) {

			if (player) {
				player.Fire ();
			}

		}

		if (Input.GetButtonDown ("Jump")) {

			if (player) {
				player.Jump ();
			}

		}

		if (Input.GetButtonDown ("Dash")) {

			if (player) {
				player.Dash ();
			}

		}

		if (Input.GetButtonDown ("Interact")) {

			foreach  (DoorPanel doorPanel in doorPanels) {
				doorPanel.Push ();
			}

		}

		if (Input.GetButtonDown ("Start")) {
			
		}

	}

	void FixedUpdate () {

		float horizontalMovement = Input.GetAxis ("Horizontal");
		float verticalMovement = Input.GetAxis ("Vertical");

		if (player) {
			player.MoveHorizontally (horizontalMovement);
		}

		foreach (Elevator elevator in elevators) {
			elevator.GoToNextFloor (verticalMovement);
		}

	}
}
