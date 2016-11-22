using UnityEngine;
using System.Collections;

public class ContactCheck : MonoBehaviour {

	public string objTag;
	private bool isInContact = false;

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.tag == objTag) {
			isInContact = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == objTag) {
			isInContact = false;
		}
	}

	public bool getIsInContact ()
	{
		return isInContact;
	}
}
