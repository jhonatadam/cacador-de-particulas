using System.Collections;
using UnityEngine;

public class DoorPanel : MonoBehaviour {


	public ContactCheck contactCheck;

	private SpriteRenderer spriteRenderer;

	public Door door;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();

		spriteRenderer.color = new Vector4 (255, 0, 0, 255);
	}
	
	// Update is called once per frame
	void Update () {
		if (contactCheck.getIsInContact () && Input.GetKeyDown (KeyCode.F)) {
			door.opened = true;
			spriteRenderer.color = new Vector4 (0, 255, 0, 255);
		}





	}
}
