using UnityEngine;
using System.Collections;

public class Paralax : MonoBehaviour {

	public Player player;
	public float offset;

	void Start () {
		player = GameObject.Find ("Player").GetComponent<Player> ();
	}

	void FixedUpdate () {
		transform.Translate (new Vector3((offset * player.GetPreviousPositionDifference ()).x, 0, 0));
	}
}
