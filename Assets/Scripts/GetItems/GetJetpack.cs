using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetJetpack : MonoBehaviour {

	void Start () {
		if (GameObject.Find ("Player").GetComponent<Player> ().hasJetpack)
			Destroy (gameObject.GetComponentInParent<Transform> ().gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<Player> ().hasJetpack = true;
			other.gameObject.GetComponent<Player> ().SetJetpackActive (true);
			//Adiciona cartão vermelho
			other.gameObject.GetComponent<Player> ().cards.Add (CardEnum.Roxo);
			Destroy (this.gameObject);
		}
	}
}
