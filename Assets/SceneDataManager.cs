using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SceneData {

	// PLAYER--------------------------------------
	public Vector3 playerPosition;
	public Vector2 playerVelocity;
	public bool[] playerDoorPermission;

	// ELEVADOR------------------------------------
	// conjunto de andares atuais dos elevadores
	// (o elevador 0 desse vetor é correspondente
	// ao elevador 0 do vetor de elevadores do 
	// SceneDataManager)
	public int[] elevatorCurrentFloor;

	// PORTA---------------------------------------
	// conjunto de estado das portas
	// (mapeado da mesma forma que os elevadores)
	public DoorState[] doorState;
}

public class SceneDataManager : MonoBehaviour {

	public GameObject player;
	
	public Elevator[] elevators;

	public Door[] doors;

	// Dados da cena
	SceneData sceneData;

	private string fileName = "PlayerInfo.json";
	private string filePath = null;

	// caminhos onde o arquivo deve ser salvo
	private string standaloneFilePath = "Particles_Data/Resources/";
	private string editorFilePath = "Assets/Resources/JSONData/";

	void Start () {
		
	}

	void OnDestroy () {

	}

	public void Save () {
		#if UNITY_STANDALONE
		filePath = standaloneFilePath;
		#endif

		#if UNITY_EDITOR
		filePath = editorFilePath;
		#endif

		string json = JsonUtility.ToJson (sceneData);

		using (FileStream fs = new FileStream (filePath + fileName, FileMode.Create)) {
			using (StreamWriter sw = new StreamWriter (fs)) {
				sw.Write (json);
			}
		}

		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh ();
		#endif
	}

	public void Load () {
		#if UNITY_STANDALONE
		filePath = standaloneFilePath;
		#endif

		#if UNITY_EDITOR
		filePath = editorFilePath;
		#endif

		string json;

		using (FileStream fs = new FileStream (filePath + fileName, FileMode.Open)) {
			using (StreamReader sr = new StreamReader (fs)) {
				jsonInfo = sr.ReadToEnd();
				JsonUtility.FromJsonOverwrite(json, sceneData);
			}
		}


		// carregando dados nos gameobjects da cena...


	}
		
}
