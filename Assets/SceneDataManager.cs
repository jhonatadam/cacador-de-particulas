using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData {

	public string currentScene;

	public float health;
	public float energy;

	// Permissao das portas
	public bool[] doors;

	public Vector3 position;
	public Vector2 velocity;

}

[Serializable]
public class SceneData {

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

	public string sceneName;

	public GameObject player;
	
	public Elevator[] elevators;

	public Door[] doors;

	//Dados do player
	private PlayerData playerData = new PlayerData();

	// Dados da cena
	private SceneData sceneData = new SceneData();

	private string playerFileName = "Player.json";
	private string sceneFileName = "";
	private string filePath = null;

	// caminhos onde o arquivo deve ser salvo
	private string standaloneSaveFilePath = "Particles_Data/Resources/Save/";
	private string editorSaveFilePath = "Assets/Resources/JSONData/Save/";

	private string standaloneTempFilePath = "Particles_Data/Resources/Temp/";
	private string editorTempFilePath = "Assets/Resources/JSONData/Temp/";

	void Start () {
		// definindo nome do arquivo da cena
		sceneFileName = sceneName + ".json";

		// selecionando caminho dos arquivos
		#if UNITY_STANDALONE
		filePath = standaloneTempFilePath;
		#endif

		#if UNITY_EDITOR
		filePath = editorTempFilePath;
		#endif

		// carregando player
		string playerJson = LoadJsonFile (filePath + playerFileName);

		if (!playerJson.Equals("")) {
			JsonUtility.FromJsonOverwrite(playerJson, playerData);

			player.transform.position = playerData.position;
			player.GetComponent <Rigidbody2D> ().velocity = playerData.velocity;

		}


		// carregando cena
		string sceneJson = LoadJsonFile (filePath + sceneFileName);

		if (!sceneJson.Equals ("")) {
			
		}

	}

	void OnDestroy () {

		// definindo caminho dos arquivos
		#if UNITY_STANDALONE
		filePath = standaloneTempFilePath;
		#endif

		#if UNITY_EDITOR
		filePath = editorTempFilePath;
		#endif

		//salvando dados do player
		string playerJson = JsonUtility.ToJson (playerData);
		SaveJsonFile (filePath + playerFileName, playerJson);


		#if UNITY_EDITOR
		UnityEditor.AssetDatabase.Refresh ();
		#endif
	}

	public void Save () {

	}

	private string LoadJsonFile (string file) {
		string json = "";

		try {
			using (FileStream fs = new FileStream (file, FileMode.Open)) {
				using (StreamReader sr = new StreamReader (fs)) {
					json = sr.ReadToEnd();
				}
			}
		} catch {
		}

		return json;
	}

	private void SaveJsonFile (string file, string json) {
		print ("salvando");
		using (FileStream fs = new FileStream (file, FileMode.Create)) {
			using (StreamWriter sw = new StreamWriter (fs)) {
				sw.Write (json);
			}
		}
	}

	public void UpdatePlayerData () {
		playerData.position = player.transform.position;
		playerData.velocity = player.GetComponent <Rigidbody2D> ().velocity;
	}
}
