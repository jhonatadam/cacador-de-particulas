using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atrator : MonoBehaviour {
	public string description;
	public string animationCall = "none";
	public float timer;
	public int max;
	private bool done = false;
	private int qtd = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!done) {
			timer = timer - Time.deltaTime * qtd;
		}
		if (timer <= 0) {
			done = true;
		}
	}
	public bool IsFull(){
		return qtd == max;
	}
	public bool IsDone(){
		return done;
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Reparador") {
			other.gameObject.GetComponent<ReparadorBehavior> ().StopLookingForAtrator (gameObject);
			qtd += 1;
		}
	}
}
