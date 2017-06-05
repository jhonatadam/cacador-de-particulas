using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour {
	
	/* ==================== SISTEM DE EVENTOS =======================
	 * 1 - Os eventos que terminam com btn sao eventos de quando se 
	 *     apertam botoes;
	 * 
	 * 2 - As funcoes estaticas sao funcoes que ativam os eventos;
	 * 
	 * 3 - Os eventos que terminam com cmd sao eventos de quando se
	 * 	   acionam comandos;
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

	//Quando se aperta o botao de pulo
	public static Event onJumpBtn;
	//Quando se aperta o botao de dash
	public static Event onDashBtn;
	//Quando se aperta o botao do campo magnetico
	public static Event onMagneticFieldBtn;
	//Quando se aperta o eixo horizontal, positivo ou negativo
	public static MovementEvent onHorizontalBtn;
	//Quando se o comando climbDown
	public static Event onClimbDownCmd;

	//Quando uma cutscene inicia
	public static Event onCutsceneStart;
	//Quando uma cutscene termina
	public static Event onCutsceneEnd;

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
}
