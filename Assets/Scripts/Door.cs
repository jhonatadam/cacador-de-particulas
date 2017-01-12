using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	//id da porta.
	public int id;
	//Variável que indica se a porta está aberta.
	public bool opened = false;

	private Animator animator;



	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (opened) {
			animator.SetBool ("Opened", opened);
		}
	}
}
