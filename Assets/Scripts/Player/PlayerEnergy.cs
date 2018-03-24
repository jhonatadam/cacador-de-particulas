using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnergyLevel {Verde, Amarelo, Vermelho};

public class PlayerEnergy : MonoBehaviour {

	public float maxEnergy = 100f;
	public float energy;
	public EnergyLevel level;
	private PistolController pc;
	ParticleSystem pistolGlow;

	private Player player;


	// Use this for initialization
	void Start () {
		player = GetComponent<Player> ();
	}

	public void OnPistolEnable(){ //não é um callback padrão
		//Inicializa o HP do player
		//energy = 0;
		pc = gameObject.GetComponentInChildren<PistolController>();
		print (pc);
		pistolGlow = gameObject.GetComponentsInChildren<ParticleSystem>()[0];
	}

	// Update is called once per frame
	void Update () {
		if (gameObject.GetComponent<Player> ().hasPistol) {
			updateLevel ();
		}
	}

	void FixedUpdate() {
		
	}

	/* Função que consome energia.
	 * 
	 * 
	 * 
	 **/
	public void ConsumeEnergy(float cost) {
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
		if (!player.GetUpdateOn())
			return;
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

		if (temp <= 0.5f) {
			if (level != EnergyLevel.Verde) {
				ParticleSystem.MainModule main = pistolGlow.main;
				main.startColor = new Color (0.1f, 1.0f, 0.2f);
				gameObject.GetComponent<Player> ().SwitchAnimator ("verde");
				pistolGlow.Emit (50);
			}
			level = EnergyLevel.Verde;
			pc.setAnimation ("Fire1");

		} else if (temp <= 0.8f) {
			if (level != EnergyLevel.Amarelo) {
				ParticleSystem.MainModule main = pistolGlow.main;
				main.startColor = new Color (1.0f, 1.0f, 0.2f);
				gameObject.GetComponent<Player> ().SwitchAnimator ("amarelo");
				pistolGlow.Emit (50);
			}
			level = EnergyLevel.Amarelo;
			pc.setAnimation ("Fire2");

		} else {
			if (level != EnergyLevel.Vermelho) {
				ParticleSystem.MainModule main = pistolGlow.main;
				main.startColor = new Color (1.0f, 0.1f, 0.2f);
				gameObject.GetComponent<Player> ().SwitchAnimator ("vermelho");
				pistolGlow.Emit (50);
			}
			level = EnergyLevel.Vermelho;
			pc.setAnimation ("Fire3");
		}
	}
}
