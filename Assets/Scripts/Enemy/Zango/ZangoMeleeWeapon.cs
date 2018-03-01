using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZangoMeleeWeapon : MonoBehaviour {

	// Referência para o script
	// de comportamento do inimigo.
	[HideInInspector]
	public ZangoBehavior behavior;

	//Referencia para o player
	private Player player;

	// true se arma está pronta para
	// atirar, false caso contrário.
	[HideInInspector]
	public bool isLoaded;

	// Tempo que demorar parar a arma 
	// do inimigo ser recarregada.
	public float reloadTime;

	// Dano da bala.
	public float upToDownDamage;
	public float rotatingDamage;

	// Contagem regressiva para 
	// recarregar a pistola
	[HideInInspector]
	public float timeCounter;

	// Distância a qual o inimigo
	// começa a atacar o player.
	public float range;

	private Animator animator;

	//Variavel que indica se o machado atingiu o player.
	private bool axeHitPlayer;

	//Variavel que indica se o machado atingiu o player.
	private bool axeHitFloor;

	//Variável que indica o golpe atual;
	//none -> nenhum golpe;
	//upToDown -> golpe baixo para cima;
	//rotating -> ataque rotatório;
	//EnergyBall -> bola de energia;
	//Cure -> cura
	public string actualAttack = "none";


	//Tempo que o machado tem que ficar preso no chão
	private float axeInFloorTime = 1f;
	//Boleano que indica se o cronometro do machado no chão está ativo;
	private bool axeChronometer = false;
	//Instante que o cronometro começou a contar;
	private float axeChronometerInit = 0.0f;
	//Tempo que o zango fica no ataque rotatorio
	private float rotatingAxe = 4f;
	//Booleano que indica se o cronometro do ataque rotatorio esta ativo;
	private bool rotatingChronometer = false;
	//Contador de quantas vezes o rotatorio foi executado
	[HideInInspector]
	public float rotationCounter = 0;
	private float rotationDmgCounter = 0f;
	//força de atração da rotação
	public float attractionPower;


	//referencia para o prefab da bola de particulas
	//Associado no inspector
	public GameObject energyBall;
	//referencia para a posicao de saida da bola
	private Transform energyBallExit;
	//dano da bola de particulas
	//Associado no inspector
	[HideInInspector]
	public float energyBallDamage = 0.0f;
	//velocidade da bola de particulas
	//Associado no inspector
	public float energyBallSpeed = 1.0f;
	//Contador de quantas vezes o energy ball foi usada
	[HideInInspector]
	public float energyBallCounter = 0;
	//numero de saltos para soltar a bola de energia
	public int jumpsToEnergyBall = 6;


	//Quantidade de HP que o zango se curará
	private float cureAmount = 40.0f;
	//Contador de hp para a cura
	[HideInInspector]
	public float cureCounter = 0;



	//HP do zango
	private ZangoHealth zangoHealth;

	//Effector que atrai as coisas
	private GameObject rotatingAttractor;

	// Use this for initialization
	void Start () {
//		Time.timeScale = .5f;
		animator = GetComponentInChildren <Animator> ();
		behavior = GetComponent<ZangoBehavior> ();
		isLoaded = true;
		zangoHealth = GetComponent<ZangoHealth> ();

		player = behavior.player;

		energyBallExit = transform.GetChild (1);

		rotatingAttractor = transform.GetChild (2).gameObject;
	}

	void Update () {
//		Hit ();


		if (rotatingChronometer) {

			//APLICANDO FORÇA NO ALVINN (SUGANDO)
			//vetor entre zango e alvinn
			Vector3 v = transform.position - player.transform.position;
			if (transform.position.x > player.transform.position.x) {
				player.transform.position = new Vector3 (player.transform.position.x + attractionPower, player.transform.position.y, player.transform.position.z);
			} else {
				player.transform.position = new Vector3 (player.transform.position.x - attractionPower, player.transform.position.y, player.transform.position.z);
			}


			
			if (Time.time - axeChronometerInit >= rotatingAxe) {
				//parar atque rotatorio
				rotatingChronometer = false;
				animator.SetTrigger ("Attack2-free");
				isLoaded = true;
				actualAttack = "none";
			}
		}

		if (axeChronometer) {
			print ("Contando o tempo. Restante: " + (Time.time - axeChronometerInit));
			if (Time.time - axeChronometerInit >= axeInFloorTime) {
				//liberar machado do chao
				axeChronometer = false;
				if (actualAttack == "upToDown")
					animator.SetTrigger ("AxeFree");
				if (actualAttack == "Cure")
					zangoCure ();
				//Tirar isso quando tiver a animação
				isLoaded = true;
				actualAttack = "none";
				print ("Machado liberado. FInito.");
			}
		}

		if (isLoaded) {

			//Cura
			//a cada 100 de hp
			if (cureCounter >= 100.0f) {
				cureCounter = 100.0f - cureCounter;
				startZangoCure ();
				return;
			}
			
			//Ataque2 - rotatorio
			//a cada 50
			if (rotationCounter >= 50.0f) {
				rotationCounter = 50.0f - rotationCounter;
				startRotatingAttack ();
				return;
			}


			//lancar bola de energia
			//a cada 40
			if (energyBallCounter >= 40.0f) {
				energyBallCounter = 40.0f - energyBallCounter;
				shootEnergyBall ();
			}

			if (behavior.jumpsNumber >= jumpsToEnergyBall) {
				behavior.jumpsNumber = 0;
				shootEnergyBall ();
				return;
			}

			//Ataque 1 - up to down
			if (Vector3.Distance(behavior.player.transform.position, transform.position) < range) {
				// bater
				upToDownAttack();

			}
		} else {
			
		}
	}
		

	public void upToDownAttack() {
		isLoaded = false;
		axeHitPlayer = false;
		animator.SetBool ("HitPlayer", false);
		//Dizendo que o golpe atual é upToDown
		actualAttack = "upToDown";

		animator.SetTrigger ("Attack");
		print ("Começou up to down");
	}

	public void startRotatingAttack() {
		isLoaded = false;
		animator.SetTrigger ("Attack2");
		actualAttack = "rotating";

	}
		
	private void OnTriggerEnter2D(Collider2D other) {
		string tag = other.gameObject.tag;


		if (tag == "Player") {
			if (actualAttack == "upToDown") {
				axeHitPlayer = true;
				animator.SetBool ("HitPlayer", true);
				print ("Up to down acertou player. Finito");
			
				//Calcula a direcao que o player esta em relacao ao inimigo para aplicar o knockback.
				float direction = other.transform.position.x - transform.position.x;
				direction = direction / Mathf.Abs (direction);
				other.gameObject.GetComponent<Player> ().Knockback (direction, 0.5f, 0.1f, 2);

				other.gameObject.GetComponent<PlayerHealth> ().DamagePlayer (upToDownDamage);
				isLoaded = true;
				actualAttack = "none";
			}
		} else {
			if (actualAttack == "upToDown") {
				axeHitPlayer = false;


			}
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		string tag = other.gameObject.tag;


		if (tag == "Player") {

			if (actualAttack == "rotating") {
				//if (Time.time - rotationDmgCounter >= 1/4) {
					

				float dano = Mathf.Lerp (0, 20, 1f) * Time.deltaTime * 2.5f;
				print ("Rotatório causando dano " + dano);
				other.gameObject.GetComponent<PlayerHealth> ().DamagePlayer (dano);
//					rotationDmgCounter = Time.time;
				//}
			}
		}
	}

	//chamada quando o macahado encosta no chao durante ataque 1
	public void onStuckStart() {
		if (!axeChronometer) {
			print ("Up to down acertou o chao. Iniciando contagem.");
			axeChronometer = true;
			axeChronometerInit = Time.time;

		}
	}

	//funcao que atira a bola de energia de particulas
	private void shootEnergyBall() {
		isLoaded = false;
		actualAttack = "EnergyBall";
		//ATENÇÃO: GAMBIARRA ENQUANTO NÃO TEM A ANIMAÇÃO DA BOLA DE ENERGIA (ATIRANDO)
		axeChronometer = false;
		onStuckStart ();
		//FIM DA GAMBIARRA
		GameObject temp;

		float ang = Vector2.Angle (energyBallExit.position, player.transform.transform.position);
		print (ang);
		ang = Mathf.Atan2 (player.transform.position.y - energyBallExit.position.y, (player.transform.position.x - energyBallExit.position.x)*(behavior.isFacingRight ? 1 : -1));

		Quaternion rota = Quaternion.Euler (energyBallExit.rotation.eulerAngles.x, energyBallExit.rotation.eulerAngles.y,  ang*Mathf.Rad2Deg);

		temp = Instantiate (energyBall, energyBallExit.position, rota) as GameObject;
		temp.GetComponent<Rigidbody2D> ().velocity = temp.transform.right * energyBallSpeed; 

		temp.GetComponent<EnemyBullet> ().setDamage (energyBallDamage);
	}

	public void startRotatingChronometer() {
		if (!rotatingChronometer) {
			rotatingChronometer = true;
			axeChronometerInit = Time.time;
			rotationDmgCounter = Time.time;
			//				rotatingAttractor.SetActive (true);
		}
	}

	//função para inicializar a cura
	private void startZangoCure() {
		isLoaded = false;
		//Descomentar quando tiver a animação
		//animator.SetTrigger ("Cure");
		actualAttack = "Cure";
		rotatingAttractor.SetActive (true);
		//ATENÇÃO: GAMBIARRA ENQUANTO NÃO TEM A ANIMAÇÃO DE CURA
		//fazer do mesmo modo do ataque rotatório
		axeChronometer = false;
		onStuckStart ();
		//FIM DA GAMBIARRA
	}

	//Função para curar de fato. Chamda depois da animação de cura
	private void zangoCure() {
		//animator.SetTrigger ("Cure-free");
		isLoaded = true;
		actualAttack = "none";
		rotatingAttractor.SetActive (false);

		zangoHealth.CureEnemy (cureAmount);
	}
}

