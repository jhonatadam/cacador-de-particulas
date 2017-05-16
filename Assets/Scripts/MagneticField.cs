using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour {

	public float force;

	public SpriteRenderer playerSr;

	private Vector2 partVel;

	void OnTriggerStay2D(Collider2D other)
	{
		/*
		 * CONCERTAR A TRAJETORIA DAS PARTICULAS NO CAMPO. NAO ESTA CORRETO!!!!!!
		 * !
		 * !
		 * 
		 * */
		if (other.gameObject.tag == "Particle") {
			Rigidbody2D particleRb2d = other.gameObject.GetComponent<Rigidbody2D> ();
			Particle particleScript = other.GetComponent<Particle>();

			//print ("Velocidade da particula" + particleRb2d.velocity.magnitude);

			Vector3 fm = particleScript.charge *
				Vector3.Cross(
					new Vector3 (particleRb2d.velocity.x, particleRb2d.velocity.y, 0), 
					//new Vector3 (partVel.x, partVel.y, 0), 
					new Vector3 (0, 0, (playerSr.flipX ? force : -force)));

		//	print ("Fm" + fm);
			
			particleRb2d.AddForce (new Vector2(fm.x, fm.y));
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Particle") {
			Rigidbody2D particleRb2d = other.gameObject.GetComponent<Rigidbody2D> ();
			partVel = new Vector2 (particleRb2d.velocity.x, particleRb2d.velocity.y);
			//print ("Velocidade da particula" + partVel);
		}
	}
}
