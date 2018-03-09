using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour {
	
	/* ==================== SISTEMA DE EVENTOS =======================
	 * 1 - Os eventos que terminam com btn sao eventos de quando se 
	 *     apertam botoes;
	 * 
	 * 2 - As funcoes estaticas sao funcoes que ativam os eventos;
	 * 
	 * 3 - Os eventos que terminam com cmd sao eventos de quando se
	 * 	   acionam comandos;
	 * 
	 * 4 - Os eventos que terminam com BtnHold são eventos de quando se
	 *     seguram algum botão;
	 * */

	//Delegate que recebe os eventos
	public delegate void Event();

	public delegate void MovementEvent(float movement);

	//Quando se aperta o botao Interact
	public static Event onInteract;
	//Quando se aperta o botao de vertical negativo
	public static Event onVerticalDown;


	//Quando se aperta o botao de pausa
	public static Event onStartBtn;
	//Quando se aperta o botao que mostra o total map
	public static Event onTotalMapBtn;

	//Quando se aperta o botao de pulo
	public static Event onJumpBtn;
	//Quando se aperta o botao de dash
	public static Event onDashBtn;
	//Quando se aperta o botao do campo magnetico
	public static Event onMagneticFieldBtn;
	//Quando se aperta o eixo horizontal, positivo ou negativo
	public static MovementEvent onHorizontalBtn;
	//Quando se aperta o botao de atirar
	public static Event onFireBtn;
	//Quando se o comando climbDown
	public static Event onClimbDownCmd;

	//Quando uma cutscene inicia
	public static Event onCutsceneStart;
	//Quando uma cutscene termina
	public static Event onCutsceneEnd;

	//Quando uma cutscene inicia
	public static Event onDialogueStart;
	//Quando uma cutscene termina
	public static Event onDialogueEnd;

	//Quando se abre alguma tela (Ex: Interactives)
	public static Event onScreenShown;
	//Quando se fecha alguma tela (Ex: Interactives)
	public static Event onScreenDismissed;

	//Quando se segura o botão de pulo
	public static Event onJumpBtnHold;
	//Quando se aperta o botão do jetpack
	public static Event onJetpackBtn;

	public static void Interact() {
		if (onInteract != null)
			onInteract ();
	}

	public static void VerticalDown() {
		if (onVerticalDown != null)
			onVerticalDown ();
	}

	public static void StartBtn() {
		if (onStartBtn != null)
			onStartBtn ();
	}

	public static void TotalMapBtn () {
		if (onTotalMapBtn != null)
			onTotalMapBtn ();
	}

	public static void JumpBtn() {
		if (onJumpBtn != null)
			onJumpBtn ();
	}

	public static void DashBtn() {
		if (onDashBtn != null)
			onDashBtn ();
	}

	public static void MagneticFieldBtn() {
		if (onMagneticFieldBtn != null)
			onMagneticFieldBtn ();
	}

	public static void HorizontalBtn(float movement) {
		if (onHorizontalBtn != null)
			onHorizontalBtn (movement);
	}

	public static void FireBtn() {
		if (onFireBtn != null)
			onFireBtn ();
	}

	public static void ClimbDownCmd() {
		if (onClimbDownCmd != null)
			onClimbDownCmd();
	}

	public static void CutsceneStart() {
		if (onCutsceneStart != null)
			onCutsceneStart ();
	}

	public static void CutsceneEnd() {
		if (onCutsceneEnd != null)
			onCutsceneEnd ();
	}

	public static void DialogueStart() {
		if (onDialogueStart != null)
			onDialogueStart ();
	}

	public static void DialogueEnd() {
		if (onDialogueEnd != null)
			onDialogueEnd ();
	}

	public static void ShowScreen() {
		if (onScreenShown != null) {
			onScreenShown ();
		}
	}

	public static void DismissScreen() {
		if (onScreenDismissed != null) {
			onScreenDismissed ();
		}
	}

	public static void JumpBtnHold() {
		if (onJumpBtnHold != null) {
			onJumpBtnHold ();
		}
	}

	public static void JetpackBtn() {
		if (onJetpackBtn != null) {
			onJetpackBtn ();
		}
	}
}
