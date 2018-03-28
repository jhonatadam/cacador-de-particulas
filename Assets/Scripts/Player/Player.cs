using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Collections;



public class Player : MonoBehaviour {

	public float speed;
	public float jumpForce;
	public float dashTime;
	public float dashPushTime;
	public float dashStartSpeed;
	public float dashEndSpeed;
	public float dashAngle;

	private float KBTime;
	private float KBAngle;
	private float KBPushTime;
	private float KBStartSpeed;
	public float KBEndSpeed;

	private bool dashing = false;
	private float dashEnlapsedTime = 0.0f;

	//KB significa KnockBack
	private bool knockbacking = false;
	private float KBElapsedTime = 0.0f;
	private float KBdirection;

	private bool flipX = false;

	private Animator animator;
	private Rigidbody2D rb2d;

	private PlayerEnergy playerEnergy;

	public Vector3 previousPosition;

	public GroundCheck groundCheck;

	private bool updateOn = true;
	public bool canJump;

	public GameObject magneticField;

	public bool hasPistol = false;
	public GameObject pistol;
	public Transform bulletExit;
	public GameObject bulletG;
	public GameObject bulletR;
	public GameObject bulletY;
	public float bulletSpeed;
	public float pistolDamage;
	public float pistolEnergyCost;
	public float pistolPushTime;
	public GameObject ondaJetpack;

	private float pistolEnlapsedTime = 0.0f;

	public bool hasJetpack = false;
	public GameObject jetpack;

	public List<CardEnum> cards;

	private AudioSource fire1;
	private AudioSource fire2;
	private AudioSource fire3;

	public RuntimeAnimatorController jet;
	public RuntimeAnimatorController jetgun1;
	public RuntimeAnimatorController jetgun2;
	public RuntimeAnimatorController jetgun3;
	public RuntimeAnimatorController gun1;
	public RuntimeAnimatorController gun2;
	public RuntimeAnimatorController gun3;
	public RuntimeAnimatorController naked;

	void Start () {
		animator = GetComponent <Animator> ();

		rb2d = GetComponent<Rigidbody2D> ();

		playerEnergy = gameObject.GetComponent<PlayerEnergy> ();

		previousPosition = transform.position;

		canJump = true;
		fire1 = gameObject.GetComponents<AudioSource> () [0];
		fire2 = gameObject.GetComponents<AudioSource> () [1];
		fire3 = gameObject.GetComponents<AudioSource> () [2];

		cards = new List<CardEnum> ();
	}

	void Update () {
		if (knockbacking) {
			if (KBElapsedTime < KBPushTime) { //está em propulsão para trás kkk
				rb2d.velocity = new Vector2 (rb2d.velocity.x, 0.0f);
				animator.SetFloat ("playerXVelocity", Mathf.Abs (rb2d.velocity.x));
				KBElapsedTime += Time.deltaTime;
				// restaurando angulo original do player
				//transform.Rotate (new Vector3 (0, 0, (flipX ? 1 : 1) * (dashAngle * (Time.deltaTime / dashPushTime))));
			} else if (KBElapsedTime < KBTime) { // está parado, faz uma pequena espera pra recuperar os movimentos
				rb2d.velocity = new Vector2 ( KBEndSpeed*KBdirection, rb2d.velocity.y);
				animator.SetFloat ("playerXVelocity", Mathf.Abs (rb2d.velocity.x));
				KBElapsedTime += Time.deltaTime;
			} else { //saindo do knockback
				knockbacking = false;
				updateOn = true;
				KBElapsedTime = 0.0f;
				transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
			}
		}
		if (dashing) {
			
			if (dashEnlapsedTime < dashPushTime) { // está em propulsão para frente
				rb2d.velocity = new Vector2 (rb2d.velocity.x, 0.0f);
				animator.SetFloat ("playerXVelocity", Mathf.Abs(rb2d.velocity.x));
				dashEnlapsedTime += Time.deltaTime;

				// restaurando angulo original do player ao longo do dash
				transform.Rotate (new Vector3 (0,0, (flipX ? 1 : 1)  * (dashAngle * (Time.deltaTime / dashPushTime))));
			}else if (dashEnlapsedTime < dashTime) { // está parado, faz uma pequena espera pra recuperar os movimentos
				rb2d.velocity = new Vector2 ((flipX ? -dashEndSpeed : dashEndSpeed) , rb2d.velocity.y);
				animator.SetFloat ("playerXVelocity", Mathf.Abs(rb2d.velocity.x));
				dashEnlapsedTime += Time.deltaTime;

				// restaurando angulo original do player ao longo do dash
				//transform.Rotate (new Vector3 (0,0, (sr.flipX ? -1 : 1) * (dashAngle * (Time.deltaTime / (dashTime - dashPushTime)))));
			} else { // saindo do dash
				dashing = false;
				updateOn = true;
				dashEnlapsedTime = 0.0f;
//				transform.localEulerAngles = new Vector3 (0, 0, 0);
				transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, transform.rotation.w);
			}

		}



	}

	public void SetPistolActive(bool active){
		pistol.SetActive (active);
		SwitchAnimator ("gun");
	}
	public void SetJetpackActive(bool active){
		jetpack.SetActive (active);
		ondaJetpack.SetActive (true);
		SwitchAnimator ("jet");
	}

	void FixedUpdate() {
		
	}

	void LateUpdate() {
		animator.SetFloat ("horizontalMovement", Mathf.Abs(rb2d.velocity.x));
		animator.SetFloat ("verticalMovement", rb2d.velocity.y);

		previousPosition = transform.position;
	}

	public void MoveHorizontally (float horizontalMovement) {
		if (updateOn) {
			// atualizando velocidade
			rb2d.velocity = new Vector2 (horizontalMovement * speed, rb2d.velocity.y);

			// atualizando animator
			UpdateSpriteDirection (horizontalMovement);
		}	
	}
	public void SwitchAnimator(string name){
		//esse metodo é complicado
		//parâmetros válidos são: "gun", "jet", "verde", "amarelo", "vermelho"
		//eu fiz ele assim porque eu queria que ele fosse o mais facil possivel de entender
		//esse metodo não é lento, ele é eficiente O(1)
		//para cada possivel parametro eu busco as possiveis situações
		//para que seja facil de usar o metodo
		if(name == "gun"){
			if (!hasJetpack) {
				if (playerEnergy.level == EnergyLevel.Verde) {
					animator.runtimeAnimatorController = gun1;
				} else if (playerEnergy.level == EnergyLevel.Amarelo) {
					animator.runtimeAnimatorController = gun2;
				} else if (playerEnergy.level == EnergyLevel.Vermelho) {
					animator.runtimeAnimatorController = gun3;
				}
			} else {
				if (playerEnergy.level == EnergyLevel.Verde) {
					animator.runtimeAnimatorController = jetgun1;
				} else if (playerEnergy.level == EnergyLevel.Amarelo) {
					animator.runtimeAnimatorController = jetgun2;
				} else if (playerEnergy.level == EnergyLevel.Vermelho) {
					animator.runtimeAnimatorController = jetgun3;
				}
			}
		}
		if(name == "jet"){
			if (!hasPistol) {
				animator.runtimeAnimatorController = jet;
			} else {
				if (playerEnergy.level == EnergyLevel.Verde) {
					animator.runtimeAnimatorController = jetgun1;
				} else if (playerEnergy.level == EnergyLevel.Amarelo) {
					animator.runtimeAnimatorController = jetgun2;
				} else if (playerEnergy.level == EnergyLevel.Vermelho) {
					animator.runtimeAnimatorController = jetgun3;
				}
			}
		}
		if (hasPistol) {
			if (name == "verde") {
				if (hasJetpack) {
					animator.runtimeAnimatorController = jetgun1;
				} else {
					animator.runtimeAnimatorController = gun1;
				}
			}
			if (name == "amarelo") {
				if (hasJetpack) {
					animator.runtimeAnimatorController = jetgun2;
				} else {
					animator.runtimeAnimatorController = gun2;
				}
			}
			if (name == "vermelho") {
				if (hasJetpack) {
					animator.runtimeAnimatorController = jetgun3;
				} else {
					animator.runtimeAnimatorController = gun3;
				}
			}

		}
	}

	public void Jump () {
		if (canJump && updateOn && (groundCheck.isGrounded () || groundCheck.isPlatformed())) {
			
			//se apertar baixo + pulo em uma plataforma
			if ((Input.GetAxisRaw ("Vertical") < 0) && groundCheck.isPlatformed()) {
				return;
			}
			
			// aplica forca do salto
			rb2d.AddForce (new Vector2 (0, jumpForce));

			// atualizando animator
			animator.SetBool ("jump", true);
			
		}
	}

	public void ClimbDown() {
		if (updateOn && groundCheck.isPlatformed ()) {
			GameObject platform = groundCheck.getPlatform ();
			if (platform) {
				platform.GetComponent<BoxCollider2D> ().isTrigger = true;
			}
		}
	}

	public void Dash () {
		if (updateOn && !dashing) {
			dashing = true;
			updateOn = false;
			rb2d.velocity = new Vector2 ((flipX ? -dashStartSpeed : dashStartSpeed) , 0.0f);
			transform.Rotate (new Vector3 (0, 0, (flipX ? -1 : -1) * dashAngle));
		}
	}

	public void Fire () {
//		print ("atire");

		if (playerEnergy.energy < pistolEnergyCost || !updateOn)
			return;
		
		if (hasPistol && (Time.time - pistolEnlapsedTime) >= pistolPushTime) {
			GameObject temp;
			if (playerEnergy.getLevel () == EnergyLevel.Verde) {
				temp = Instantiate (bulletG, bulletExit.position, bulletExit.rotation) as GameObject;
				fire1.Play ();
			} else if (playerEnergy.getLevel () == EnergyLevel.Amarelo) {
				temp = Instantiate (bulletY, bulletExit.position, bulletExit.rotation) as GameObject;
				fire2.Play ();
			} else {
				temp = Instantiate (bulletR, bulletExit.position, bulletExit.rotation) as GameObject;
				fire3.Play ();
			}

			temp.GetComponent<Rigidbody2D> ().velocity = temp.transform.right * bulletSpeed; 

			float multiplyer = 1f;
			float multEnergy = 1f;
			if (playerEnergy.level == EnergyLevel.Verde) {
				multiplyer = 1f;
				multEnergy = 1f;
			} else if (playerEnergy.level == EnergyLevel.Amarelo) {
				multiplyer = 2.4f;
				multEnergy = 2.5f;
			} else if (playerEnergy.level == EnergyLevel.Vermelho) {
				multiplyer = 5f;
				multEnergy = 5f;
			}

			temp.GetComponent<Bullet> ().setDamage (pistolDamage * multiplyer);

			playerEnergy.ConsumeEnergy (pistolEnergyCost * multEnergy);
			pistolEnlapsedTime = Time.time;

		}
	}

	public Vector3 GetPreviousPositionDifference () {
		return transform.position - previousPosition;
	}
		
	public void SetUpdateOn(bool value) {
		updateOn = value;
		rb2d.velocity = new Vector2 (0, 0);
	}

	public void SetUpdateFalse() {
		SetUpdateOn (false);
	}

	public void SetUpdateTrue() {
		SetUpdateOn (true);
	}

	public bool GetUpdateOn() {
		return updateOn;
	}

	public void UpdateSpriteDirection (float horizontalMovement) {
		if (horizontalMovement < 0.0f) {
//			sr.flipX = true; 
			flipX = true;
			transform.rotation = new Quaternion(0, 180, 0, 0);
		} else if (horizontalMovement > 0.0f) {
//			sr.flipX = false;
			flipX = false;
			transform.rotation = new Quaternion(0, 0, 0, 0);
		} 
	}

	/* Função que ativa/desativa o campo magnético
	 * 
	 * */
	public void SwitchMagneticField() {
		if (!updateOn)
			return;
		if (magneticField.activeInHierarchy) {
			magneticField.SetActive (false);
		} else if(playerEnergy.energy >= magneticField.GetComponent<MagneticField>().energyUse*Time.deltaTime) {
			magneticField.SetActive (true);
		}
	}

	public bool hasCard(CardEnum card) {
		return cards.Contains (card);
	}

	private void OnEnable() {
		//Configurando listeners de eventos
		EventsManager.onJumpBtn += Jump;
		EventsManager.onDashBtn += Dash;
		EventsManager.onMagneticFieldBtn += SwitchMagneticField;
		EventsManager.onHorizontalBtn += MoveHorizontally;
		EventsManager.onFireBtn += Fire;
		EventsManager.onClimbDownCmd += ClimbDown;
		EventsManager.onDialogueStart += SetUpdateFalse;
		EventsManager.onDialogueEnd += SetUpdateTrue;
	}

	private void OnDisable() {
		//Configurando listeners de eventos
		EventsManager.onJumpBtn -= Jump;
		EventsManager.onDashBtn -= Dash;
		EventsManager.onMagneticFieldBtn -= SwitchMagneticField;
		EventsManager.onHorizontalBtn -= MoveHorizontally;
		EventsManager.onFireBtn -= Fire;
		EventsManager.onClimbDownCmd -= ClimbDown;
		EventsManager.onDialogueStart -= SetUpdateFalse;
		EventsManager.onDialogueEnd -= SetUpdateTrue;
	}

	/* Função que joga o Player para tras ao receber dano
	 * direction pode ser -1 ou 1
	 * 
	 * 
	 * */
	public void Knockback(float direction, float time, float pushTime, float speed) {
		if (updateOn && !knockbacking) {
			KBTime = time;
			KBPushTime = pushTime;
			KBStartSpeed = speed;
			knockbacking = true;
			updateOn = false;
			KBdirection = direction;
			rb2d.velocity = new Vector2 (KBStartSpeed*KBdirection, 0.0f);
			transform.Rotate (new Vector3 (0, 0, -KBAngle));
		}
	}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "DeathZone") {
			FakeDeath (coll.GetComponent<DeathZone> ().returnPoint);
		}
	}
	public void FakeDeath(Vector2 returnPoint){
		transform.position = new Vector3 (returnPoint.x, returnPoint.y, transform.position.z);
	}

}
