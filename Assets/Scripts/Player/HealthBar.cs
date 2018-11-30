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
	private bool foundPlayer = false;
    public float energyBlinkDelay;
    private float lastblink;
    private Color energyOriginal;
    public Color energyBlinkColor;
    public float damageBlinkDelay;
    private float dmgBlinkDelay;
    public Color damageBlinkColor;
    private Color hpOriginal;
	// Use this for initialization
	void Start () {
        //player = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
        //playerEnergy = GameObject.Find ("Player").GetComponent<PlayerEnergy> ();
        lastblink = Time.time;
        energyOriginal = energyBar.color;
        hpOriginal = hpBar.color;
	}

	void Update() {
        if (!foundPlayer) {
            if (GameObject.Find("Player") != null) {
                player = GameObject.Find("Player").GetComponent<PlayerHealth>();
                playerEnergy = GameObject.Find("Player").GetComponent<PlayerEnergy>();
                foundPlayer = true;
            }
        }
        if (foundPlayer) {
            if ((playerEnergy.energy / playerEnergy.maxEnergy) >= 0.8f) {
                if (Time.time - lastblink > energyBlinkDelay) {
                    if (compareColor(energyBar.color, energyOriginal)) {
                        energyBar.color = energyBlinkColor;
                    } else {
                        energyBar.color = energyOriginal;
                    }
                    lastblink = Time.time;
                }
            }
            if ((playerEnergy.energy / playerEnergy.maxEnergy) < 0.8f) {
                energyBar.color = energyOriginal;
            }
            if (dmgBlinkDelay > 0) {
                dmgBlinkDelay -= Time.deltaTime;
                float p = dmgBlinkDelay / damageBlinkDelay;
                hpBar.color = new Color(hpOriginal.r * (1 - p) + damageBlinkColor.r * p, hpOriginal.g * (1 - p) + damageBlinkColor.g * p, hpOriginal.b * (1 - p) + damageBlinkColor.b * p);
            }
            if (dmgBlinkDelay <= 0) {
                hpBar.color = hpOriginal;
            }
        }
    
		
			
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (foundPlayer) {
			CalculateHpBarSize ();
			CalculateEnergyBarSize ();
		}
	}

    public void DamageBlink(float damage) {
        print(damageBlinkDelay);
        dmgBlinkDelay = damageBlinkDelay;
    }

	void CalculateHpBarSize() {
		perCent = player.health / player.maxHealth;
		hpBar.fillAmount = perCent;
		perCent *= 100;
			
	}

	void CalculateEnergyBarSize() {
		perCentEnergy = playerEnergy.energy / playerEnergy.maxEnergy;
		energyBar.fillAmount = perCentEnergy;
		perCentEnergy *= 100;

	}
    private bool compareColor(Color a, Color b) {
        return a.r == b.r && a.g == b.g && a.b == b.b;
    }
}
