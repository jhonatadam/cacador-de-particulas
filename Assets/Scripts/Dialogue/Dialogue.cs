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

	//numero de caracteres por fala
	public int n_caractere = 100;

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

	//id do diálogo. Envolve todos os diálogos do jogo
	public int id;

	//Flag que indica se o dialogo acabou

	public bool over = false;

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
					EventsManager.DialogueEnd ();
					return;
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
		if (textoAtual + 1 < dialogo.Length) {
			canContinue = false;
			//Texto do autor (linhas pares)
			CampoDeTextoAutor.text = dialogo [textoAtual++];
			//Texto do diálogo (linhas ímpares
			message = dialogo [textoAtual];
			foreach (char c in message) {
				if (c.ToString () != " ") {
					audioSource.Play ();
				}
				CampoDeTexto.text += c;
				yield return new WaitForSeconds (0.080f); 
			}
			canContinue = true;
		}

	}

	private void ShowDialogue() {
		if (dialogo == null)
			return;
		if (textoAtual >= dialogo.Length) {
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
		//string[] temp = arquivoTexto.text.Split ('\n');
		dialogo = ProcessText (arquivoTexto.text.Split ('\n'));
		//dialogo = new string[] {"pato", "quen"};
	}

	//Função que processa os textos, dividindo as falas para caberem na caixa de dialogo
	private string[] ProcessText(string[] dialogues) {
		List<string> dialogues_temp = new List<string> ();
		string autor_temp;
		int j = 0, k = 0;
		Queue words_queue = new Queue ();

		for (int i = 0; i < dialogues.Length; i++) {
			print ("Lendo texto: " +i);
			//i -> autor
			autor_temp = dialogues [i++];
			//i -> fala do autor
			j++;
			dialogues_temp.Add(autor_temp);
			dialogues_temp.Add("");

			foreach (string s in dialogues[i].Split(' ')) {
				words_queue.Enqueue (s);
			}
			int temp_size = 0;
			while (words_queue.Count != 0) {
				if (((string)words_queue.Peek ()).Length + temp_size <= n_caractere) {
					temp_size += ((string)words_queue.Peek ()).Length + 1;
					dialogues_temp [j] += words_queue.Dequeue () + " ";
				} else {
					temp_size = 0;
					j++;
					j++;
					dialogues_temp.Add(autor_temp);
					dialogues_temp.Add("");
				}
			}
			j++;
		}

		return dialogues_temp.ToArray ();


	}

	public bool isActive() {
		return active;
	}
}
