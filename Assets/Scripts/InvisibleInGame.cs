using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleInGame : MonoBehaviour {
	private SpriteRenderer sr;
	// Use this for initialization
	void Start () {


		//referencio, mudo e depois esqueço dele.
		sr = GetComponent<SpriteRenderer> ();
		sr.color = new Color (0, 0, 0, 0);
		sr = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
