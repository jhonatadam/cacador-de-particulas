using UnityEngine;
using System.Collections;

public class Paralax : MonoBehaviour {

	public Player player;
	public float offset;

	void FixedUpdate () {
		transform.Translate (new Vector3((offset * player.GetDelta ()).x, 0, 0));
	}
}
