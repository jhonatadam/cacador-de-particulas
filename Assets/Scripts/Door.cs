using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorState {Locked, Unlocked, Opened};

public class Door : MonoBehaviour {

	//id da porta.
	public int id;

	// indica o estado da porta (locked, unlocked e opened)
	public DoorState state;

	private Animator animator;

	//Cartão necessário para abrir a porta
	public CardEnum necessaryCard;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();

		if (state == DoorState.Opened) {
			animator.Play ("OpenedDoor");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if ((state == DoorState.Opened) && !animator.GetCurrentAnimatorStateInfo (0).IsName ("OpenedDoor")) {
			animator.SetBool ("Opened", true);
		}
	}

}
