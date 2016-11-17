using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour {

	public Vector3 topPosition;
	public Vector3 downPosition;
	public Vector3 speed;

	private bool isInTop = false;

	private bool playerIn = false;

	private bool move = false;

	void Start () {
		downPosition = transform.position;
	}

	void Update () {
		if (playerIn) {
			if (Input.GetKeyDown (KeyCode.F)) {
				move = true;
			}
		}

		if (move) {
			if (!isInTop) {
				if (transform.position.y < topPosition.y) {
					transform.Translate (speed);
				} else {
					isInTop = true;
					move = false;
				}
			} else {
				if (transform.position.y > downPosition.y) {
					transform.Translate (-1*speed);
				} else {
					isInTop = false;
					move = false;
				}
			}
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			playerIn = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			playerIn = false;
		}
	}
}
