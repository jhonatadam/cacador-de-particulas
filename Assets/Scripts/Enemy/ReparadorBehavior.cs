using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReparadorBehavior : EnemyBehavior {
	// Coordenada (em x) máxima (right) e 
	// mínima (left) que o patrulheiro caminha.
	// durante sua patrulha.
	public float rightLimit;
	public float leftLimit;
	public float turningDelay = 1;
	private float lastTurning = 0;
	// Arma corpo a corpo
	private ReparadorMeleeWeapon weapon;
	public GameObject PointsController;
	private List<GameObject> PointsList;
	private bool FoundPoint = false;
	private int following = 0;
	private bool isReparing = false;
	private float reparingElapsed = 0;
	// Referência do animator
	private Animator animator;
	private bool sawPlayer;
	private GameObject reparando;
	private bool lookingForAtrator = true;
	private SpriteRenderer sr;
	private bool hasPointsController = false;
	void OnDestroy(){
		weapon = null;
		PointsController = null;
		animator = null;
		reparando = null;
		sr = null;
	}
	void Start () {
		base.Start ();
		weapon = GetComponent <ReparadorMeleeWeapon> ();
		animator = GetComponentInChildren <Animator> ();
		PointsController = GameObject.Find ("ReparadorPointsController");
		if (!Resources.ReferenceEquals (PointsController, null)) {
			hasPointsController = true;
		}
		sr = GetComponentInChildren<SpriteRenderer> ();
	}

	void Update () {
		if (dead) {
			return;
		}
		// se o samurai estiver atacando, o seu 
		// comportamento não é atualizado
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack1") ||
		    animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack2")) {
			Stop2 ();
			return;
		}
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Voando")) {
			// Andar
			Move ();
		} else {
			Stop2 ();
		}
		Act ();
		if (rb2d.velocity.x > 0) {
			sr.flipX = true;
		} else if(rb2d.velocity.x < 0) {
			sr.flipX = false;
		}
	}
	void LateUpdate () {
		if (dead) {
			return;
		}
		animator.SetBool ("isSeeingThePlayer", isSeeingThePlayer);
		animator.SetBool ("sawPlayer", sawPlayer);
	}

	public override void Patrol () {
		if (dead) {
			return;
		}
		// Atualizando orientação
		if (Time.time - lastTurning > turningDelay) {
			UpdateGuidancePatrol ();
			lastTurning = Time.time;
		}
	}
		
	public void Stop2 () {
		rb2d.velocity = new Vector2 (0, 0);
	}

	public void UpdateGuidancePatrol () {
		
	}
	void OnTriggerEnter2D(Collider2D other){
		
		if (hasPointsController && PointsController.GetComponent<ReparadorPoints> ().Size () > 0) {
			if (other.gameObject.tag == "ReparerPoint") {
				if (other.gameObject.Equals (GetPoints()[following])) {
					FoundPoint = true;
					following = (following + 1) % PointsController.GetComponent<ReparadorPoints> ().Size ();
					reparingElapsed = 0.0f;
					rb2d.velocity = new Vector2 (0, 0);
					//print ("FOUND YOU");
				}
			}
		}
	}
	private float distanceTo(GameObject go){
		return Vector2.Distance (transform.position, go.transform.position);
	}
	public override void Attack ()
	{
		if (Time.time - lastTurning > turningDelay) {
			//UpdateGuidanceFollowPlayer ();
			lastTurning = Time.time;
		}
		float playerDistance = Vector3.Distance (transform.position, player.transform.position);

		if (weapon.range < playerDistance) {
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("AttackMode")) {
				Move ();
			}
		} else {
			Stop ();
		}
	}

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

	public void Move () {
		if (dead) {
			return;
		}
		if (hasPointsController && PointsController.GetComponent<ReparadorPoints>().Size() > 0) { 


			if (FoundPoint) {
				reparingElapsed += Time.deltaTime;
				if (reparingElapsed >=  GetPoints () [following].GetComponent<ReparadorPoint>().reparingTime) {
					FoundPoint = false;
				}
			} else {
				if (isSeeingThePlayer) {
					float dy = player.transform.localPosition.y - transform.position.y;
					float dx = player.transform.localPosition.x - transform.position.x;
					float angle = Mathf.Atan2 (dy, dx);
					float vx = Mathf.Cos (angle) * moveSpeed;
					float vy = Mathf.Sin (angle) * moveSpeed;

					rb2d.velocity = new Vector2 (vx, vy);
				} else {
					following = following % PointsController.GetComponent<ReparadorPoints> ().Size ();

					float dy = GetPoints () [following].transform.localPosition.y - transform.position.y;
					float dx = GetPoints () [following].transform.localPosition.x - transform.position.x;
					float angle = Mathf.Atan2 (dy, dx);
					float vx = Mathf.Cos (angle) * moveSpeed;
					float vy = Mathf.Sin (angle) * moveSpeed;

					rb2d.velocity = new Vector2 (vx, vy);
				}
			}
		}
	}

	public bool IsFacingThePlayer () {
		if (isFacingRight) {
			return transform.position.x < player.transform.position.x;
		} else {
			return transform.position.x > player.transform.position.x;
		}
	}
	public override bool Look ()
	{
		bool ret = (EnemyInCamera () && player.isActiveAndEnabled);
		sawPlayer = ret ? ret : sawPlayer;
		return ret;
	}
	public List<GameObject> GetPoints(){
		return PointsController.GetComponent<ReparadorPoints> ().GetPoints ();
	}
}
