    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropHP : MonoBehaviour {
	public float amount = 20;
	public bool active = true;
    private AudioManager audioManager;
    // Use this for initialization
    void Start () {
        audioManager = AudioManager.instance;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (active)
            {
                audioManager.PlaySound("DropHP");
                coll.gameObject.GetComponent<PlayerHealth>().CurePlayer(amount);
                GetComponent<EffectDestroy>().End();
                active = false;
            }
        }
    }
}
