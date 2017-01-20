﻿using UnityEngine;
using System.Collections;

public class Elevator : MonoBehaviour {

	private bool moving;

	// Posições dos andares no eixo y
	public float[] floorsPosition;

	// Andar atual
	public int currentFloor;

	// Andar que o player deseja alcançar
	public int nextFloor;

	// Tamanho dos saltos na atualização do elevador
	// (influencia na velocidade e na precisão com que
	// o elevador alcança o andar seguinte)
	public float step;

	// checa que o player está dentro do elevador
	public ContactCheck playerCheck;

	// referências do player
	public Player player;
	public CameraController cam;

	// referencia para o sprite do elevador
	SpriteRenderer sr;

	// sprites do elevador
	Sprite stopSprite;
	Sprite upSprite;
	Sprite downSprite;
	Sprite[] sprites;

	string spritePath = "Sprites/Scenario/Lab/";
	string spriteName = "elevator-1x2";

	// sound effects
	public AudioClip startSound;
	public AudioClip endSound;
	public AudioClip movingSound;

	private AudioSource audioSource;

	void Start () {
		nextFloor = currentFloor;

		sr = GetComponent <SpriteRenderer> ();
		sprites = Resources.LoadAll<Sprite>(spritePath + spriteName);

		audioSource = GetComponent <AudioSource> ();
	}

	void Update () {
		if ( playerCheck.getIsInContact() && (currentFloor == nextFloor) ) {
			float verticalMovement = Input.GetAxis ("Vertical");

			if (verticalMovement > 0.0f && (floorsPosition.Length > (currentFloor + 1))) {
				nextFloor += 1;
				sr.sprite = sprites[1];

				player.SetUpdateOn (false);
				cam.activeTracking = false;

				audioSource.PlayOneShot (startSound, 0.4f);
				audioSource.Play (0);
			}
			if (verticalMovement < 0.0f && (currentFloor > 0)) {
				nextFloor -= 1;
				sr.sprite = sprites[2];

				player.SetUpdateOn (false);
				cam.activeTracking = false;

				audioSource.PlayOneShot (startSound, 0.4f);
				audioSource.Play (0);
			}
		}

		if (currentFloor != nextFloor) {
			if (currentFloor < nextFloor) {
				if (transform.position.y < floorsPosition[nextFloor]) {
					Move ();
				} else {
					Stop ();
				}
			} else {
				if (transform.position.y > floorsPosition[nextFloor]) {
					Move ();
				} else {
					Stop ();
				}
			}
		}
			
	}

	private void Move () {
		moving = true;
		int direction = nextFloor - currentFloor;
		Vector3 speed = new Vector3 (0, step, 0);

		transform.Translate (direction * speed);
		player.transform.Translate (direction * speed);
		cam.transform.Translate (direction * speed);

	}

	private void Stop () {
		currentFloor = nextFloor;
		transform.position = new Vector3 (transform.position.x, floorsPosition[nextFloor], transform.position.z);
		sr.sprite = sprites[0];
		moving = false;
		cam.activeTracking = true;
		player.SetUpdateOn (true);

		audioSource.Stop ();
		audioSource.PlayOneShot (endSound, 0.4f);
	}

	public bool GetMoving () {
		return moving;
	}
		
}
