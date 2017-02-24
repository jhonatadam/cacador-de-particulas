using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public Player player;
	public GameObject hpBar;
	public Text text;
	private float perCent;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CalculateBarSize ();
	}

	void CalculateBarSize() {
		perCent = player.health / player.maxHealth;
		hpBar.transform.localScale = new Vector3(perCent, 1f, 1f);
		perCent *= 100;
		text.text = perCent + "%";
			
	}
}
