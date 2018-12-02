using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZangoHealth : EnemyHealth {

	private ZangoMeleeWeapon weapon;
	private GameObject battleStarter;
	// Use this for initialization
	void Start () {
        dyingElapsed = dyingTime;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        gravityOriginal = rb2d.gravityScale;
        health = maxHealth;
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();
        weapon = gameObject.GetComponent<ZangoMeleeWeapon> ();
		battleStarter = GameObject.Find ("BattleStarter");
        audioManager = AudioManager.instance;
        animator = GetComponent<Animator>();
        if (animator == null) {
            animator = gameObject.GetComponentInChildren<Animator>();
        }
        
    }

	// Update is called once per frame
	void Update () {
        if (dead) {
            dyingElapsed -= Time.deltaTime;
            if (Time.time - lastSparkle > sparkleInterval && sparkleAmount > 0) {
                GameObject sprk = Instantiate(shortSparkle);
                sprk.transform.position = new Vector3(transform.position.x + Random.Range(-sparkleRadius / 2.0f, sparkleRadius / 2.0f), transform.position.y + Random.Range(-sparkleRadius / 2.0f, sparkleRadius / 2.0f), transform.position.z);
                lastSparkle = Time.time;
                sparkleAmount--;
            }
            return;
        }
        GetComponentInChildren<SpriteRenderer>().color = new Color(1, health/maxHealth, health/maxHealth);
		if (health <= 0) {
            battleStarter.GetComponent<ZangoBattleStarter>().portaEnd.SetActive(false);
            audioManager.StopSound("Zango");
            audioManager.PlaySound("Enemy Death Zango");
            KillEnemy ();
		}
	}

	public override void DamageEnemy(float damage) {

		if (health - damage < 0) {
			health = 0;
			return;
		} 
		health -= damage;

		weapon.energyBallCounter += damage;
		weapon.cureCounter += damage;
		weapon.rotationCounter += damage;

	}

	/* Função que cura o HP do player
	 * 
	 * 
	 * 
	 * */
	public void CureEnemy(float cure) {
		if (health + cure > maxHealth) {
			health = maxHealth;
			return;
		}

		health += cure;
	}

    public bool IsDead() {
        return dead;
    }

	//public void KillEnemy() {
		//battleStarter.GetComponent<ZangoBattleStarter> ().camera2.SetActive (false);
		//battleStarter.GetComponent<ZangoBattleStarter> ().camera1.SetActive (true);
        //rb2d.velocity = new Vector2(0, 0);
        //rb2d.gravityScale = 0;
        //animator.SetTrigger("Dead");
        //Collider2D[] colliders = GetComponentsInChildren<Collider2D>();
        //for (int i = 0; i < colliders.Length; i++) {
            //colliders[i].enabled = false;
        //}
        //SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();
        //sr.color = new Color(0.3f, 0.23f, 0.35f, 0.9f);
        
        //TODO essa é apenas uma morte provisória, é preciso fazer corretamente. Colocar animações e etc.
        //dead = true;
        //GetComponent<EnemyBehavior>().dead = true;
    //}
}
