using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene_0 : Cutscene {

	private Dialogue dialogue1;

	private GameObject sleepingMachine;

	private AnimationClip flutuando;

	void FixedUpdate() {
		if (updateOn == true) {
			print (getCutsceneTime ());

			if (scope == 0) {
				if (getCutsceneTime () >= 0f && getCutsceneTime () <= 0.1f) {
					// Não faz nada, apenas um tempo para o jogador se ambientar
					animationPl.Play("flutuando");
				}
				if (getCutsceneTime () > 0.1f && getCutsceneTime () <= 3f) {
					// Não faz nada, apenas um tempo para o jogador se ambientar

				}
				if (getCutsceneTime () > 3f && getCutsceneTime () <= 3.1f) {
					//Máquina para de funcionar e ALVINN cai de cara no chao.

					//Mudar isso para algo que apenas DESATIVE a máquina.
					Destroy(sleepingMachine);
				}
				if (getCutsceneTime () > 3.1f && getCutsceneTime () <= 4f) {
					//Máquina para de funcionar e ALVINN cai de cara no chao.
					print("caindo no chao");
				}
				if (getCutsceneTime () > 4f && getCutsceneTime () < 5f) {
					//Alvinn se levanta
					print("levantando");
				}
				if (getCutsceneTime () > 5f && getCutsceneTime () <= 5.1f) {
					//Alvinn olha ao redor
					print("olhando ao redor");
					player.UpdateSpriteDirection (-1);

				}
				if (getCutsceneTime () > 7f && getCutsceneTime () <= 7.5f) {
					//Alvinn olha ao redor
					print("olhando ao redor");
					player.UpdateSpriteDirection (1);
				}
				if (getCutsceneTime () > 9f && getCutsceneTime () <= 9.1f) {
					//Alvinn olha ao redor
					print("olhando ao redor");
					player.UpdateSpriteDirection (-1);
				}

				if (getCutsceneTime () > 11f && getCutsceneTime () <= 11.1f) {
					//Alvinn ouve um barulho e se assusta.
					print("barulho");
					player.UpdateSpriteDirection (1);
				}
			}

			if (scope == 1) {
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

			if (scope == 2) {
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
		player.GetComponent<Animator> ().enabled = false;
		animationPl = player.GetComponent<Animation> ();


		//Obtendo recursos para a cutscene
		dialogue1 = gameObject.transform.GetChild (0).gameObject.GetComponent<Dialogue> ();
		sleepingMachine = GameObject.Find ("Sleeping Machine (Temporary)");

		//Animacoes
		flutuando = Resources.Load("Anims/Player/Cutscenes/evento 0/Flutuando_Feto") as AnimationClip;
		print (flutuando);

		animationPl.AddClip(flutuando, "flutuando");
		print (animationPl.GetClipCount ());

		print(animationPl.GetClip ("flutuando"));

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
