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

		// nao sei pq, mas só funciona assim
		// aparentemente checar se é nulo não é suficiente
		try {
			player.MoveHorizontally (horizontalMovement);
		} catch {
		}

		foreach (Elevator elevator in elevators) {
			elevator.GoToNextFloor (verticalMovement);
		}

	}
}
