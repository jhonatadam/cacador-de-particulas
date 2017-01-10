using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public Player player;
	public float offset;

	public ContactCheck playerCheck;

	void FixedUpdate () {
		transform.position = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
		//transform.Translate (new Vector3 ((offset * player.GetDelta ()).x, 0, 0));
	}

}
