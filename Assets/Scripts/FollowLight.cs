using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLight : MonoBehaviour {

	public GameObject player;

	// limites que a luz percorre
	public Rect limits;

	private SpriteRenderer sr;

	void Start () {
		player = GameObject.Find ("Player");
		sr = GetComponent <SpriteRenderer> ();
		sr.enabled = true;

	}

	void Update () {
		if (limits.Contains (player.transform.position)) {
			if (!sr.enabled) {
				sr.enabled = true;
			}

			transform.position = 
				new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);

		} else {
			if (sr.enabled) {
				sr.enabled = false;
			}
		}
	}
}
