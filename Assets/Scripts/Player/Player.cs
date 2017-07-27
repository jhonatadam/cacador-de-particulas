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

	private bool dashing = false;
	private float dashEnlapsedTime = 0.0f;

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
	public GameObject bullet;
	public float bulletSpeed;
	public float pistolDamage;
	public float pistolEnergyCost;
	public float pistolPushTime;

	private float pistolEnlapsedTime = 0.0f;

	void Start () {
		animator = GetComponent <Animator> ();

		rb2d = GetComponent<Rigidbody2D> ();

		playerEnergy = gameObject.GetComponent<PlayerEnergy> ();

		previousPosition = transform.position;

		canJump = true;
	}

	void Update () {
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

		pistol.SetActive (hasPistol);
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

	public void Jump () {
		print (canJump);
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

		if (playerEnergy.energy < pistolEnergyCost)
			return;
		
		if (hasPistol && (Time.time - pistolEnlapsedTime) >= pistolPushTime) {
			GameObject temp;
			temp = Instantiate (bullet, bulletExit.position, bulletExit.rotation) as GameObject;
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
		if (magneticField.activeInHierarchy) {
			magneticField.SetActive (false);
		} else if(playerEnergy.energy >= magneticField.GetComponent<MagneticField>().energyUse*Time.deltaTime) {
			magneticField.SetActive (true);
		}
	}

	private void OnEnable() {
		//Configurando listeners de eventos
		EventsManager.onJumpBtn += Jump;
		EventsManager.onDashBtn += Dash;
		EventsManager.onMagneticFieldBtn += SwitchMagneticField;
		EventsManager.onHorizontalBtn += MoveHorizontally;
		EventsManager.onFireBtn += Fire;
		EventsManager.onClimbDownCmd += ClimbDown;
	}

	private void OnDisable() {
		//Configurando listeners de eventos
		EventsManager.onJumpBtn -= Jump;
		EventsManager.onDashBtn -= Dash;
		EventsManager.onMagneticFieldBtn -= SwitchMagneticField;
		EventsManager.onHorizontalBtn -= MoveHorizontally;
		EventsManager.onFireBtn -= Fire;
		EventsManager.onClimbDownCmd -= ClimbDown;
	}






}
