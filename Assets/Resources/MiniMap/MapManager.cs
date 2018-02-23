using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

	[SerializeField]
	public List<int> discoveredMapBlockers = new List<int>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void updateMap() {
		foreach (int i in discoveredMapBlockers) {
			print (i);
		}
		foreach (Transform mp in transform.GetChild(0)) {
			
			MapBlocker mb = mp.gameObject.GetComponent<MapBlocker> ();

			if (discoveredMapBlockers.Contains (mb.id)) {
				print ("tem o id");
				Destroy(mb.gameObject);
			}
		}
	}
}
