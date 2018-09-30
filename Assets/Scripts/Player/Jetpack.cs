using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : MonoBehaviour {

	public float propelForce;
	public float maxUpVelocity;
	[SerializeField]
	private bool activated;

	public float energyUse;

	private Rigidbody2D rb;
	private PlayerEnergy player;
    private float originalGravity;


	// Use this for initialization
	void Start () {
		rb = gameObject.GetComponentInParent<Rigidbody2D> ();
		player = gameObject.GetComponentInParent<PlayerEnergy> ();
        originalGravity = rb.gravityScale;
		activated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SwitchActivated() {
		if (!activated) {
			
			gameObject.GetComponentInParent<Player> ().canJump = false;
            rb.gravityScale = 0.3f;
            activated = !activated;
		} else {
			
			gameObject.GetComponentInParent<Player> ().canJump = true;
            rb.gravityScale = originalGravity;
            activated = !activated;
		}
	}
    public void Propel() {
        if (activated && player.GetComponent<Player>().GetUpdateOn()) {
            if (player.energy < energyUse * Time.deltaTime) {
                rb.gravityScale = originalGravity;
                return;
            }
            player.ConsumeEnergy(energyUse * Time.deltaTime);
            rb.velocity = new Vector2(rb.velocity.x, player.GetComponent<Player>().speed*1.6f);

        }
    }


    public void PropelOld() {
		if (activated && player.GetComponent<Player>().GetUpdateOn()) {
			if (player.energy < energyUse * Time.deltaTime)
				return;
			player.ConsumeEnergy (energyUse * Time.deltaTime);
			rb.AddForce (transform.up * propelForce);
			Vector2 v = rb.velocity;

			v.y = Mathf.Clamp(v.y, -float.MaxValue, maxUpVelocity);

			rb.velocity = v;
		}
	}

	private void OnEnable() {
		//Configurando listeners de eventos
		EventsManager.onJumpBtnHold += Propel;
		EventsManager.onJetpackBtn += SwitchActivated;

	}

	private void OnDisable() {
		//Configurando listeners de eventos
		EventsManager.onJumpBtnHold -= Propel;
		EventsManager.onJetpackBtn -= SwitchActivated;

	}
}
