using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public PlayerHealth player;
	public GameObject hpBar;
	private float perCent;

	public PlayerEnergy playerEnergy;
	public GameObject energyBar;
	private float perCentEnergy;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
		playerEnergy = GameObject.Find ("Player").GetComponent<PlayerEnergy> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CalculateHpBarSize ();
		CalculateEnergyBarSize ();
	}

	void CalculateHpBarSize() {
		perCent = player.health / player.maxHealth;
		hpBar.transform.localScale = new Vector3(perCent, 1f, 1f);
		perCent *= 100;
			
	}

	void CalculateEnergyBarSize() {
		perCentEnergy = playerEnergy.energy / playerEnergy.maxEnergy;
		perCentEnergy = 1 - perCentEnergy;
		energyBar.transform.localScale = new Vector3(perCentEnergy, 1f, 1f);
		perCentEnergy *= 100;

	}
}
