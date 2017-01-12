using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public GameObject player;
	public float offset;

	// Limites da camera
	public float topLimit;
	public float bottomLimit;
	public Vector2[] lateralLimits;


	public ContactCheck playerCheck;

	void FixedUpdate () {
		transform.position = new Vector3 (player.transform.position.x, transform.position.y, transform.position.z);
		//transform.Translate (new Vector3 ((offset * player.GetDelta ()).x, 0, 0));
	}

}
