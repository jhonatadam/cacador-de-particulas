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

	private Animator animator;
	private SpriteRenderer sr;
	private Rigidbody2D rb2d;

	public Vector3 previousPosition;

	public GroundCheck groundCheck;

	public float maxHealth = 200f;
	public float health;

	public float damageCoolDown = 10f;
	private float damageTime = 0f;

	private bool updateOn = true;

	public GameObject magneticField;

	void Start () {
		animator = GetComponent <Animator> ();
		sr = GetComponent <SpriteRenderer> ();
		rb2d = GetComponent<Rigidbody2D> ();

		previousPosition = transform.position;

		//Inicializa o HP do player
		health = maxHealth;
	}

	void Update () {
		if (dashing) {
			
			if (dashEnlapsedTime < dashPushTime) { // está em propulsão para frente
				rb2d.velocity = new Vector2 (rb2d.velocity.x, 0.0f);
				animator.SetFloat ("playerXVelocity", Mathf.Abs(rb2d.velocity.x));
				dashEnlapsedTime += Time.deltaTime;

				// restaurando angulo original do player ao longo do dash
				transform.Rotate (new Vector3 (0,0, (sr.flipX ? -1 : 1)  * (dashAngle * (Time.deltaTime / dashPushTime))));
			}else if (dashEnlapsedTime < dashTime) { // está parado, faz uma pequena espera pra recuperar os movimentos
				rb2d.velocity = new Vector2 ((sr.flipX ? -dashEndSpeed : dashEndSpeed) , rb2d.velocity.y);
				animator.SetFloat ("playerXVelocity", Mathf.Abs(rb2d.velocity.x));
				dashEnlapsedTime += Time.deltaTime;

				// restaurando angulo original do player ao longo do dash
				//transform.Rotate (new Vector3 (0,0, (sr.flipX ? -1 : 1) * (dashAngle * (Time.deltaTime / (dashTime - dashPushTime)))));
			} else { // saindo do dash
				dashing = false;
				updateOn = true;
				dashEnlapsedTime = 0.0f;
				transform.localEulerAngles = new Vector3 (0, 0, 0);
			}
		}
	}

	void FixedUpdate() {
		UpdateDamageTime ();
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
		if (updateOn && (groundCheck.isGrounded () || groundCheck.isPlatformed())) {
			
			// aplica forca do salto
			rb2d.AddForce (new Vector2 (0, jumpForce));

			// atualizando animator
			animator.SetBool("jump", true);
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
			rb2d.velocity = new Vector2 ((sr.flipX ? -dashStartSpeed : dashStartSpeed) , 0.0f);
			transform.Rotate (new Vector3 (0, 0, (sr.flipX ? 1 : -1) * dashAngle));
		}
	}

	public void Fire () {
	
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

	private void UpdateSpriteDirection (float horizontalMovement) {
		if (horizontalMovement < 0.0f) {
			sr.flipX = true; 
		} else if (horizontalMovement > 0.0f) {
			sr.flipX = false;
		} 
	}


	/* Função que gera dano no player.
	 * 
	 * 
	 * 
	 **/
	public void DamagePlayer(float damage) {
		if (damageTime < damageCoolDown)
			return;
		
		if (health - damage < 0) {
			health = 0;
			damageTime = 0;
			return;
		} 
		health -= damage;
		damageTime = 0;

	}

	/* Função que cura o HP do player
	 * 
	 * 
	 * 
	 * */
	public void CurePlayer(float cure) {
		if (health + cure > maxHealth) {
			health = maxHealth;
			return;
		}

		health += cure;
	}

	void UpdateDamageTime() {
		damageTime++;
	}

	/* Função que ativa/desativa o campo magnético
	 * 
	 * */
	public void SwitchMagneticField() {
		if (magneticField.activeInHierarchy) {
			magneticField.SetActive (false);
		} else {
			magneticField.SetActive (true);
		}
	}
}
