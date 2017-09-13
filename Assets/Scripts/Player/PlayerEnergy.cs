using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnergyLevel {Verde, Amarelo, Vermelho};

public class PlayerEnergy : MonoBehaviour {

	public float maxEnergy = 100f;
	public float energy;
	public EnergyLevel level;



	// Use this for initialization
	void Start () {
		//Inicializa o HP do player
		energy = 0;
	}

	// Update is called once per frame
	void Update () {
		updateLevel ();
	}

	void FixedUpdate() {
		
	}

	/* Função que consome energia.
	 * 
	 * 
	 * 
	 **/
	public void ConsumeEnergy(float cost) {
		print ("consumiu");
		energy -= cost;

		if (energy < 0) {
			energy = 0;

			return;
		} 

	}

	/* Função que carrega energia
	 * 
	 * 
	 * 
	 * */
	public void ChargeEnergy (float charge) {
		print ("carregou");
		energy += charge;

		if (energy > maxEnergy) {
			energy = maxEnergy;
			return;
		}
			
	}

	public EnergyLevel getLevel() {
		return level;
	}

	private void updateLevel() {
		float temp = energy / maxEnergy;

		if (temp <= 0.5f) {
			level = EnergyLevel.Verde;
		} else if (temp <= 0.8f) {
			level = EnergyLevel.Amarelo;
		} else {
			level = EnergyLevel.Vermelho;
		}
	}
}
