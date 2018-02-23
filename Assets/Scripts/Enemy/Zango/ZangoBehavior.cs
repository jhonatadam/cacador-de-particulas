using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZangoBehavior : MonoBehaviour {

	[HideInInspector]
	public Player player;

	// Velocidade em que o inimigo
	// se move em relação ao Player.
	public float moveSpeed;

	// Rigidbody.
	private Rigidbody2D rb2d;

	private ZangoMeleeWeapon weapon;

	// Referência do animator
	public Animator animator;

	[HideInInspector]
	public bool isFacingRight;

	//timer para o pulo do zango
	private float zangoJumpTimer;
	//delay do pulo do zango
	public float zangoJumpDelay;
	//forca do pulo
	public int jumpForce;

	[HideInInspector]
	public bool jumping = false;

	//TESTE: plataforma que ele vai pular pra ela
	public Transform platformTest;

	public GameObject actualPlatform = null;

	//Game object que contem as plataformas
	public GameObject platformsGraph;

	//HP do zango
	private ZangoHealth zangoHealth;
	//número de saltos feitos
	[HideInInspector]
	public int jumpsNumber = 0;


	// Use this for initialization.
	void Start () {
		try {
			// Buscando referência do Player.
			player = GameObject.Find ("Player").GetComponent<Player> ();

		} catch {
			Debug.Log ("Zango: não encontrou o objeto Player.");
			player = null;
		}

		rb2d = GetComponent<Rigidbody2D> ();
		isFacingRight = true;
		zangoJumpTimer = zangoJumpDelay * -1;
		weapon = GetComponent<ZangoMeleeWeapon> ();
		animator = GetComponentInChildren <Animator> ();

		zangoHealth = GetComponent<ZangoHealth> ();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.T))
			zangoHealth.DamageEnemy (10);
		if (Input.GetKeyDown (KeyCode.Y))
			zangoJump ();
		//para a execucao se estiver em algum desses estados
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Ataque1") ||
			animator.GetCurrentAnimatorStateInfo (0).IsName ("Ataque1-stuck") ||
			animator.GetCurrentAnimatorStateInfo (0).IsName ("Ataque1-back")  ||
			animator.GetCurrentAnimatorStateInfo (0).IsName ("Ataque2-start") ||
			animator.GetCurrentAnimatorStateInfo (0).IsName ("Ataque2") ||
			animator.GetCurrentAnimatorStateInfo (0).IsName ("Ataque2-end")){
			if(!jumping)
				Stop ();
			return;
			
		}
		//ATENÇÃO: GAMBIARRA ENQUANTO NÃO TEM A ANIMAÇÃO DA BOLA DE ENERGIA (ATIRANDO)
		if (!weapon.isLoaded && (weapon.actualAttack == "EnergyBall" ||
			weapon.actualAttack == "Cure")) {
			Stop ();
			return;
		}
		//FIM DA GAMBIARRA

		Vector3 playerPos = player.transform.position;

		//Raycast
		bool platformBetween = trackPlayer ();

		UpdateGuidanceFollowPlayer ();

		if (!jumping) {
			if (platformBetween && playerAboveZango())
				chasePlayerInPlatform ();
			Move (); 

		}
			
		float playerDistance = Vector3.Distance (transform.position, playerPos);
		if (weapon.range > playerDistance) {
			Stop ();
		}
//
//		if (Input.GetKeyDown (KeyCode.K) && !jumping) {
//			//jumpToPlatform (new Vector2(6.15f, 1.86f));
////			jumpToPlatform (new Vector2(10f, 4.33f));
////			chasePlayerInPlatform();
//			shootEnergyBall();
//		}

	}

	//Retorna o limite em funcao da posicao do player
	public void UpdateGuidanceFollowPlayer () {

		if (isFacingRight) {
			if (transform.position.x > player.transform.position.x) {
				isFacingRight = false;
				transform.rotation = new Quaternion (0, 180, 0, 0);
			}
		} else {
			if (transform.position.x < player.transform.position.x) {
				isFacingRight = true;
				transform.rotation = new Quaternion (0, 0, 0, 0);
			}
		}
	}

	public bool playerAboveZango() {
		if (transform.position.y < player.transform.position.y) {
			return true;
		} else {
			return false;
		}
	}

	public void Move () {
		// Determinando velocidade.
		float playerSpeed = (player != null ? player.speed : 1); 
		float speed = (isFacingRight ? moveSpeed * playerSpeed : -moveSpeed * playerSpeed);

		// Atualizando velocidade.
		rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
	}

	public void Jump() {
		if (Time.time - zangoJumpTimer >= zangoJumpDelay) {
			rb2d.AddForce (new Vector2 (8000 * (isFacingRight ? 1 : -1), jumpForce));
			zangoJumpTimer = Time.time;
			jumping = true;
		}
	}



	//Função cria um raycast em direção ao player e checa se há uma plataforma entre eles dois
	//retorna true se houver, falso caso contrário.
	private bool trackPlayer() {
		Debug.DrawLine (transform.position, player.transform.position);

		//Raycast retorna a lista de objetos que o raycast cruzou.
		RaycastHit2D [] trackedObject = Physics2D.LinecastAll (new Vector2 (transform.position.x, transform.position.y),
			new Vector2 (player.transform.position.x, player.transform.position.y), LayerMask.NameToLayer("Interative"));

		if (trackedObject != null) {

			foreach (RaycastHit2D hit in trackedObject) {
				if (hit.collider.tag != "Enemy") {
					if (hit.collider.tag == "Platform") {
						return true;
					}
				}
			}
		}

		return false;
	}

	//Função que escolhe o menor caminho entre o player e o zango e move zango para a próxima plataforma.
	public void chasePlayerInPlatform() {
		
		if (actualPlatform == null) {
			Debug.Log ("<color=yellow>zango actual platform is null</color>", this.gameObject);
			return;
		}
		GameObject playerActualPlatform = player.gameObject.GetComponentInChildren<GroundCheck> ().getGroundObject ();

		if (playerActualPlatform == null) {
			Debug.Log ("<color=yellow>player actual platform is null</color>", this.gameObject);
			return;
		}
			
		int zangoPlatform = 0;
		for (int i = 0; i < platformsGraph.transform.childCount; i++) {
			if (platformsGraph.transform.GetChild (i).gameObject.name == actualPlatform.name)
				zangoPlatform = i;
		}

		int destPlatform = 0;
		for (int i = 0; i < platformsGraph.transform.childCount; i++) {
			if (platformsGraph.transform.GetChild (i).gameObject.name == playerActualPlatform.name)
				destPlatform = i;
		}

		if (zangoPlatform == destPlatform)
			return;

		int next = PlataformsGraph.nextPlatform (zangoPlatform, destPlatform);

		//TROCAR ISSO PELO PULO OBLIQUO
		//transform.position = platformsGraph.transform.GetChild (next).position;
//		jumpToPlatform(platformsGraph.transform.GetChild (next).position);
		zangoJump();
		jumpsNumber++;
	}


	//funcao que executa o lancamento obliquo até a plataforma pl
	public void jumpToPlatform(Vector2 pl) {
		float x = pl.x - transform.position.x;
		float y = pl.y - transform.position.y;

		float a = Mathf.Deg2Rad * 45f;
		a = Mathf.Atan2 (y, x);
		float v;

		v = Mathf.Sqrt ((rb2d.gravityScale*x*x)/2*(Mathf.Tan(a)*x - y) * Mathf.Cos(a));

		print ("V " + 2*(Mathf.Tan(a)*x - y) * Mathf.Cos(a));

		float vx = Mathf.Cos (a) * v;
		float vy = Mathf.Sin (a) * v;

		rb2d.velocity = new Vector2 (vx, vy);
	}

	public void zangoJump() {
		//rb2d.velocity = new Vector2 (20 * (player.transform.position.x < transform.position.x ? -1 : 1), 8);
		rb2d.AddForce(new Vector2(0, 40000));
		rb2d.AddForce(new Vector2(10000* (player.transform.position.x < transform.position.x ? -1 : 1), 0));
		print ("Velocidade x " + rb2d.velocity.x);
	}


	public void Stop () {
		rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
	}
		
}
