using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : Cutscene {

	private Dialogue dialogue1;

	void FixedUpdate() {
		if (updateOn == true) {
			print (getCutsceneTime ());
		
			if (scope == 0) {
				if (getCutsceneTime () >= 0f && getCutsceneTime () <= 2f) {
					player.MoveHorizontally (1);
				}
				if (getCutsceneTime () > 2f && getCutsceneTime () <= 2.1f) {
					player.Jump ();
				}
				if (getCutsceneTime () > 2.4f && getCutsceneTime () <= 2.5f) {
					player.Dash ();
				}
				if (getCutsceneTime () > 2.5f && getCutsceneTime () <= 4f) {
					player.MoveHorizontally (1);
				}
				if (getCutsceneTime () > 5f && getCutsceneTime () <= 5.1f) {
					dialogue1.Activate ();
				}
				if (getCutsceneTime () > 5.1f) {
					if (!dialogue1.isActive ()) {
						scope++;
						startTimer ();
					}
				}
			}

			if (scope == 1) {
				if (getCutsceneTime () >= 0f && getCutsceneTime () <= 2f) {
					player.MoveHorizontally (-1);
				}
				if (getCutsceneTime () > 2f) {
					Exit ();
				}
			}
		}
	}

	public override void StartScene() {
		//disable player movement and attack controls?
		//grab player reference and move to certain point
		player = GameObject.Find ("Player").GetComponent<Player>();
		dialogue1 = gameObject.transform.GetChild (0).gameObject.GetComponent<Dialogue> ();
		EventsManager.CutsceneStart();
		scope = 0;
		updateOn = true;
		print ("tartou");
		startTimer ();

		//while (timer <= 60) {
			//player.MoveHorizontally (1);
		//}

		//stopTimer ();




	}

	public override void Execute() {
		//bring up a dialog window where the player talks
		//player.Jump();

	}

	public override void Exit() {
		//re-enable controls, other clean-up.
		EventsManager.CutsceneEnd();
		updateOn = false;
	}
}
