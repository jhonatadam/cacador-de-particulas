using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Msg : MonoBehaviour {

	public Canvas msgCv;
	public ContactCheck cc;
	public Player player;

	void Update () {
		if (Input.GetKeyDown (KeyCode.F)) {
			if (cc.getIsInContact ()) {
				msgCv.enabled = !msgCv.enabled;
				player.SetUpdateOn (!msgCv.enabled);
			}
		}
	}
}
