using UnityEngine;
using System.Collections;

public class GroundCheck : MonoBehaviour {

	private bool grounded = false;
	//Indica se o player está em uma plataforma
	private bool platformed = false;
	private ArrayList groundTag;

	//Plataforma em que o player está em cima
	private GameObject platform;

	//Objeto em que o player está em cima
	private GameObject groundObject;

	void Start () {
		groundTag = new ArrayList ();

		groundTag.Add ("Ground");
		groundTag.Add ("Elevator");
	}

	void OnTriggerStay2D(Collider2D other)
	{
		groundObject = other.gameObject;

		foreach (string tag in groundTag) {
			if (other.gameObject.tag == tag) {
				grounded = true;
			}
		}

		if(other.gameObject.tag == "Platform") {
			platformed = true;
			platform = other.gameObject;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		groundObject = null;
		foreach (string tag in groundTag) {
			if (other.gameObject.tag == tag) {
				grounded = false;
			}
		}

		if(other.gameObject.tag == "Platform") {
			platformed = false;
			platform = null;
		}
	}

	public bool isGrounded ()
	{
		return grounded;

	}

	public bool isPlatformed() {
		return platformed;
	}

	public GameObject getPlatform() {
		return platform;
	}

	public GameObject getGroundObject() {
		return groundObject;
	}
}
