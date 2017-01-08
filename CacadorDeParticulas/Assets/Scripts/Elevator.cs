using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	// Posições dos andares no eixo y
	public float[] floorsPosition;

	// Andar atual
	public int currentFloor;

	// Andar que o player deseja alcançar
	private int nextFloor;

	// Tamanho dos saltos na atualização do elevador
	// (influencia na velocidade e na precisão com que
	// o elevador alcança o andar seguinte)
	public float step;

	// checa que o player está dentro do elevador
	public ContactCheck playerCheck;

	// referências ao player e a câmera
	public GameObject player;
	public GameObject camera;

	void Start () {
		nextFloor = currentFloor;
	}

	void Update () {
		if (playerCheck.getIsInContact() && (currentFloor == nextFloor)) {
			if (Input.GetKeyDown (KeyCode.UpArrow) && (floorsPosition.Length > (currentFloor + 1))) {
				nextFloor += 1;
			}
			if (Input.GetKeyDown (KeyCode.DownArrow) && (currentFloor > 0)) {
				nextFloor -= 1;
			}
		}

		if (currentFloor != nextFloor) {
			if (currentFloor < nextFloor) {
				if (transform.position.y < floorsPosition[nextFloor]) {
					move ();
				} else {
					currentFloor = nextFloor;
				}
			} else {
				if (transform.position.y > floorsPosition[nextFloor]) {
					move ();
				} else {
					currentFloor = nextFloor;
				}
			}
		}
	}

	private void move() {
		int direction = nextFloor - currentFloor;
		Vector3 speed = new Vector3 (0, step, 0);

		transform.Translate (direction * speed);
		player.transform.Translate (direction * speed);
		camera.transform.Translate (direction * speed);
	}
		
}
