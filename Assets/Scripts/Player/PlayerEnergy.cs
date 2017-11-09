using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnergyLevel {Verde, Amarelo, Vermelho};

public class PlayerEnergy : MonoBehaviour {

	public float maxEnergy = 100f;
	public float energy;
	public EnergyLevel level;
	private PistolController pc;


	// Use this for initialization
	void Start () {
		//Inicializa o HP do player
		//energy = 0;
		pc = gameObject.GetComponentInChildren<PistolController>();
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

	public int getLevelId() {
		if (level == EnergyLevel.Verde) {
			return 1;
		}
		if (level == EnergyLevel.Amarelo) {
			return 2;
		}
		if (level == EnergyLevel.Vermelho) {
			return 3;
		}
		return 1;
	}

	private void updateLevel() {
		float temp = energy / maxEnergy;
		ParticleSystem red = GameObject.Find("FaiscaVermelha").GetComponent<ParticleSystem>();
		ParticleSystem green = GameObject.Find("FaiscaVerde").GetComponent<ParticleSystem>();
		ParticleSystem yellow = GameObject.Find("FaiscaAmarela").GetComponent<ParticleSystem>();
		if (temp <= 0.5f) {
			if (level != EnergyLevel.Verde) {
				green.Emit (100);
			}
			level = EnergyLevel.Verde;
			pc.setAnimation ("Fire1");

		} else if (temp <= 0.8f) {
			if (level != EnergyLevel.Amarelo) {
				yellow.Emit (100);
			}
			level = EnergyLevel.Amarelo;
			pc.setAnimation ("Fire2");

		} else {
			if (level != EnergyLevel.Vermelho) {
				red.Emit (100);
			}
			level = EnergyLevel.Vermelho;
			pc.setAnimation ("Fire3");
		}
	}
}
