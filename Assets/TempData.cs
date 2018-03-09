using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempData : MonoBehaviour {

	public DoorState[] doorState;

	public int[] elevatorFloors;

	public List<int> discoveredMapBlockers;

	//Lista de todos os diálogos. True para diálogo que não será mais exibido, false c.c.;
	public List<bool> dialoguesOver;

	void Update () {
		
	}
}
