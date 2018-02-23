using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour {

	// Use this for initialization
	private PlayerEnergy pe;
	void Start () {
		pe = gameObject.GetComponentInParent<PlayerEnergy> ();

	}
	
	// Update is called once per frame
	void Update () {
		//pistolAnimator.SetInteger ("energyLevel", pe.getLevelId ());
	}
	public void setAnimation(string state){
	}
}
