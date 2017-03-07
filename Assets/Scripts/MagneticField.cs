using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour {

	public float force;

	public SpriteRenderer playerSr;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Particle") {
			Rigidbody2D particleRb2d = other.gameObject.GetComponent<Rigidbody2D> ();
			Particle particleScript = other.GetComponent<Particle>();

			Vector3 fm = particleScript.charge *
				Vector3.Cross(
					new Vector3 (particleRb2d.velocity.x, particleRb2d.velocity.y, 0), 
					new Vector3 (0, 0, (playerSr.flipX ? force : -force)));
			
			particleRb2d.AddForce (new Vector2(fm.x, fm.y));
		}
	}
		
}
