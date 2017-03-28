using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	public void OnTriggerExit2D(Collider2D other) {
		if(other.tag == "Player") {
			this.gameObject.GetComponent<BoxCollider2D> ().isTrigger = false;
		}
	}
}
