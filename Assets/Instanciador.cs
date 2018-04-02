using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instanciador : MonoBehaviour {
	public GameObject inimigo;
	public float delay;
	private float interval = 0;
	public bool limitRespawn = false;
	public int maxRespawn;
	private int count = 0;
	private SpriteRenderer sr;
	private bool active = false;
	// Use this for initialization
	void Start () {

		//referencio, mudo e depois esqueço dele.
		sr = GetComponent<SpriteRenderer> ();
		sr.color = new Color (0, 0, 0, 0);
		sr = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (active) {
			interval -= Time.deltaTime;
			if (interval <= 0) {
				if (!limitRespawn || count < maxRespawn) {
					GameObject inm = Instantiate (inimigo);
					inm.transform.position = transform.position;
					interval = delay;
					count++;
				}
			}
		} else {
			if (GameObject.Find ("Player") != null && GameObject.Find ("Player").GetComponent<Player> ().hasPistol) {
				active = true;
			}
		}
	}
	void OnDestroy(){
		inimigo = null;
		sr = null;
	}
}
