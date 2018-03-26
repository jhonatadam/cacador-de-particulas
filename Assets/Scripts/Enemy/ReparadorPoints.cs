using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReparadorPoints : MonoBehaviour {
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

	public int Size(){
		return GetPoints().Count;
	}
	public List<GameObject> GetPoints(){
		List<GameObject> points = new List<GameObject>();
		foreach (Transform t in transform) {
			points.Add (t.gameObject);
		}
		return points;
	}
}
