using UnityEngine;
using System.Collections;

public class ElevatorController : MonoBehaviour {

	public Vector3 topPosition;
	public Vector3 downPosition;
	public Vector3 speed;
	public ContactCheck playerCheck;
	public GameObject player;
	public GameObject camera;
	public GameObject arrowUp;
	public GameObject arrowDown;

	private bool isInTop = false;

	private bool playerIn = false;

	private bool move = false;

	void Start () {
		downPosition = transform.position;
	}

	void Update () {
		if (playerCheck.getIsInContact()) {
			if (Input.GetKeyDown (KeyCode.F)) {
				move = true;
				arrowUp.SetActive (false);
				arrowDown.SetActive (false);
			}
		}

		if (move) {
			if (!isInTop) {
				if (transform.position.y < topPosition.y) {
					transform.Translate (speed);
					player.transform.Translate (speed);
					camera.transform.Translate (speed);
				} else {
					arrowDown.SetActive (true);
					isInTop = true;
					move = false;
				}
			} else {
				if (transform.position.y > downPosition.y) {
					transform.Translate (-1*speed);
					player.transform.Translate (-1*speed);
					camera.transform.Translate (-1*speed);
				} else {
					arrowUp.SetActive (true);
					isInTop = false;
					move = false;
				}
			}
		}
	}
		
}
