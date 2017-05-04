using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScreen : MonoBehaviour {

	public SpriteRenderer arrow;
	public GameObject screen;

	public ContactCheck playerCheck;

	// Use this for initialization
	void Start () {
		arrow.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		arrow.enabled = playerCheck.getIsInContact ();
	}

	public void showScreen() {
		if(playerCheck.getIsInContact()) {
			print("Conteudo Exibido");
		}
	}
}
