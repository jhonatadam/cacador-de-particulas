using UnityEngine;
using System.Collections;

public class GroundCheckController : MonoBehaviour {

	private bool grounded = false;
	private ArrayList groundTag;

	void Start () {
		groundTag = new ArrayList ();

		groundTag.Add ("Ground");
		groundTag.Add ("Elevator");
	}

	void OnTriggerStay2D(Collider2D other)
	{
		foreach (string tag in groundTag) {
			if (other.gameObject.tag == tag) {
				grounded = true;
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		foreach (string tag in groundTag) {
			if (other.gameObject.tag == tag) {
				grounded = false;
			}
		}
	}

	public bool isGrounded ()
	{
		return grounded;

	}
}
