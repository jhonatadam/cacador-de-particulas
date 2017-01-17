using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DoorState {Locked, Unlocked, Opened};

public class Door : MonoBehaviour {

	//id da porta.
	public int id;

	//Variável que indica se a porta está aberta.
	public bool opened = false;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();

		if (opened) {
			animator.Play ("OpenedDoor");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (opened && !animator.GetCurrentAnimatorStateInfo (0).IsName ("OpenedDoor")) {
			animator.SetBool ("Opened", opened);
		}
	}

}
