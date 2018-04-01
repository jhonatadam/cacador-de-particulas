using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Incubadora : MonoBehaviour {
	private GameObject player;
	// Use this for initialization
	void Start () {
		GameObject player = Resources.Load<GameObject>("Prefabs/Player");

		if (player != null) {
			player = Instantiate (player);
			player.name = "Player";
			player.transform.position = transform.position;
			player.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
			DontDestroyOnLoad (player);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
