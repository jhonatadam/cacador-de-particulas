using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlocker : MonoBehaviour {

	public int id;
	private MapManager mapM;

	// Use this for initialization
	void Start () {
		mapM = GetComponentInParent<MapManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			mapM.discoveredMapBlockers.Add (id);
			Destroy (this.gameObject);
		}
	}
}
