using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReparadorPoints : MonoBehaviour {
	// Use this for initialization
	public GameObject reparadorPoint;
	void Start () {
		DontDestroyOnLoad (gameObject);
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
	public void CreatePoint(Vector2 position, float reparingTime, EnemyHealth eh){
		GameObject point = Instantiate (reparadorPoint);
		point.transform.position = new Vector3 (position.x, position.y, transform.position.z);
		point.transform.SetParent (gameObject.transform);
		point.GetComponent<ReparadorPoint> ().reparingTime = reparingTime;
		point.GetComponent<ReparadorPoint> ().eh = eh;
	}
}
