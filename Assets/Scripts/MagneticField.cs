using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour {

	public float force;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == "Particle") {
			Rigidbody2D particleRb2d = other.gameObject.GetComponent<Rigidbody2D> ();
			Particle particleScript = other.GetComponent<Particle>();

			Vector2 diff = other.transform.position - transform.position;

			// pegando vetor unitário
			diff = diff / (Mathf.Abs(diff.x) + Mathf.Abs(diff.y));

			particleRb2d.AddForce (force * particleRb2d.velocity.magnitude * diff);

			print (diff);
		}
	}

	/*
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Particle") {
			Particle part = other.GetComponent<Particle>();
			part.linearMovement = true;
		}
	}
	*/
}
