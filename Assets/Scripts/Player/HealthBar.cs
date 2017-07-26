using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	public PlayerHealth player;
	public Image hpBar;
	private float perCent;

	public PlayerEnergy playerEnergy;
	public Image energyBar;
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
		hpBar.fillAmount = perCent;
		perCent *= 100;
			
	}

	void CalculateEnergyBarSize() {
		perCentEnergy = playerEnergy.energy / playerEnergy.maxEnergy;
		perCentEnergy = 1 - perCentEnergy;
		energyBar.fillAmount = perCentEnergy;
		perCentEnergy *= 100;

	}
}
