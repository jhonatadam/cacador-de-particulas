using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Dialogue : MonoBehaviour {

	private string[] dialogo;

	private DialogueText dialogueText;

	public TextAsset arquivoTexto;

	public GameObject canvas;
	public Text CampoDeTextoAutor;
	public Text CampoDeTexto;


	public AudioSource audioSource;
	public AudioClip audioClip;

	//Flag que indica se a caixa de texto está ativa ou não.
	//Serve para iniciar e parar a conversa.
	[SerializeField]
	private bool active = false;

	//Flag que indica se o player pode passar para a próxima fala.
	//Isso acontece quando a fala acaba.
	private bool canContinue = false;
	//Flag que indica se a fala pode ser exibida.
	//Enquanto esta flag for true canContinue é false.
	private bool continua = true;

	//Indica qual a fala atual.
	private int textoAtual = 0;

	//Flag que indica se o dialogo acabou
	private bool over = false;

	void Start () {
		canvas.SetActive (false);

		audioSource.clip = audioClip;

		LerTexto ();
	}

	void Update () {
		if (active) {
			canvas.SetActive (true);

			if (canContinue && Input.GetButtonDown ("Jump")) {
				continua = true;
				CampoDeTexto.text = "";
				if (over) {
					DeActivate ();
					canvas.SetActive (false);
				}
			}
//			if (over && Input.GetButtonDown ("Jump")) {
//				DeActivate ();
//			}
			
			ShowDialogue ();

		}
		
	}

	public void Activate() {
		active = true;
	}

	public void DeActivate() {
		active = false;
	}

	private IEnumerator ShowMessage(string message) {
		canContinue = false;
		//Texto do autor (linhas pares)
		CampoDeTextoAutor.text = dialogo [textoAtual++];
		//Texto do diálogo (linhas ímpares
		message = dialogo [textoAtual];
		foreach (char c in message) {
			if (c.ToString() != " ") {
				audioSource.Play ();
			}
			CampoDeTexto.text += c;
			yield return new WaitForSeconds(0.080f); 
		}
		canContinue = true;

	}

	private void ShowDialogue() {
		if (textoAtual == dialogo.Length) {
			over = true;
			print ("CABOU");
		} else if(continua) {
			StartCoroutine(ShowMessage(dialogo [textoAtual]));
			//StartCoroutine(ShowMessage(dialogo [textoAtual]));
			textoAtual++;
			continua = false;
//			StartCoroutine(Example());

		}

	}

	IEnumerator Example()
	{
		print(Time.time);
		yield return new WaitForSeconds(1);
		print(Time.time);
		yield return new WaitForSeconds(1);
		print ("HEUHUEHE");
	}

	private void LerTexto() {
		dialogo = arquivoTexto.text.Split ('\n');
	}

	public bool isActive() {
		return active;
	}
}
