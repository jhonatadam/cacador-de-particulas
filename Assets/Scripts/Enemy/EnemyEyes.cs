using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEyes : MonoBehaviour {
	// POR ENQUANTO NÃO VOU USAR ISSO, TALVEZ NÃO SEJA NECESSÁRIO.

	private Renderer rend;

	// true se o inimigo está 
	// vendo o player, false
	// caso contrário.
	[HideInInspector]
	public bool seeingPlayer = false;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (rend.isVisible);
	}

	private bool Look () {
		return true;
	}
}
