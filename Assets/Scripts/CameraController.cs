using UnityEngine;
using System;
using System.Collections;

[Serializable]
public class CameraLimits {
	public Rect[] floorsLimits;
}

public class CameraController : MonoBehaviour {

	public Player player;
	public Vector3 previousPosition;

	// x se estiver se aproximando da camera (camera espera o player)
	// x+2 se estiver se distanciando (camera acompanha o player)
	public float scrollMultiplier;


	public Vector2 movementWindowSize;
	public Vector2 windowOffset;


	// Limites da camera
	public bool limitCameraMovement = true;
	public Rect[] floorsLimits;


	[HideInInspector] 
	public bool activeTracking = true;


	private Vector3 cameraPosition;
	private Rect windowRect;


	void Start () {
		player = GameObject.Find ("Player").GetComponent<Player> ();
		// salvando limites da camera
		//CameraLimits cl = new CameraLimits ();
		//cl.floorsLimits = floorsLimits;
		//print (JsonUtility.ToJson(cl));

		cameraPosition = transform.position;
		previousPosition = transform.position;
		//These are the root x/y coordinates that we will use to create our boundary rectangle.
		//Starts at the lower left, and takes the offset into account.
		float windowAnchorX = cameraPosition.x - movementWindowSize.x/2 + windowOffset.x;
		float windowAnchorY = cameraPosition.y - movementWindowSize.y/2 + windowOffset.y;

		//From our anchor point, we set the size of the window based on the public variable above.
		windowRect = new Rect(windowAnchorX, windowAnchorY, movementWindowSize.x, movementWindowSize.y);
	}


	void Update() {
		
		//Only worry about updating the camera based on player position if the player has actually moved.
		//If the tracking isn't active at all, we don't bother with any of this crap.
		if ( activeTracking && (player.GetPreviousPositionDifference () != new Vector3 (0, 0, 0)))
		{
			cameraPosition = transform.position;

			Vector3 scrollValue = defineScrollValue ();

			//Move the camera this direction, but faster than the player moved.
			Vector3 playerDiff = player.GetPreviousPositionDifference ();
			Vector3 multipliedDifference =  
				new Vector3 (playerDiff.x * scrollValue.x, playerDiff.y * scrollValue.y, playerDiff.z * scrollValue.z);

			cameraPosition += multipliedDifference;
			
			//updating our movement window root location based on the current camera position
			windowRect.x = cameraPosition.x - movementWindowSize.x/2 + windowOffset.x;
			windowRect.y = cameraPosition.y - movementWindowSize.y/2 + windowOffset.y;

			// We may have overshot the boundaries, or the player just may have been moving too 
			// fast/popped into another place. This corrects for those cases, and snaps the 
			// boundary to the player.

			if(!windowRect.Contains(player.transform.position))
			{
				Vector3 positionDifference = player.transform.position - cameraPosition;
				positionDifference.x -= windowOffset.x;
				positionDifference.y -= windowOffset.y;

				//I made a function to figure out how much to move in order to snap the boundary to the player.
				cameraPosition.x += DifferenceOutOfBounds( positionDifference.x, movementWindowSize.x );


				cameraPosition.y += DifferenceOutOfBounds( positionDifference.y, movementWindowSize.y );

			}

			// Here we clamp the desired position into the area declared in the limit variables.
			if( limitCameraMovement )
			{
				Rect currentFloorLimits = floorsLimits [GetPlayerCurrentFloor ()];
				cameraPosition.y = Mathf.Clamp ( cameraPosition.y, currentFloorLimits.y, currentFloorLimits.yMax);
				cameraPosition.x = Mathf.Clamp ( cameraPosition.x, currentFloorLimits.x, currentFloorLimits.xMax);
			}

			// and now we're updating the camera position using what came of all the calculations above.
			transform.position = cameraPosition;

		}
	}

	void LateUpdate() {
		previousPosition = transform.position;
	}
		
	private Vector3 defineScrollValue () {

		Vector3 difference = player.GetPreviousPositionDifference ();
		Vector3 scrollValue = new Vector3 ();

		scrollValue.x = 
			DefineScrollValueOfAnAxe (difference.x, player.transform.position.x, transform.position.x, 0f);
		scrollValue.y = 
			DefineScrollValueOfAnAxe (difference.y, player.transform.position.y, transform.position.y, -0.58f, 0.5f);
		scrollValue.z = 1; // player não se move em z

		return 	scrollValue;
	}

	private float DefineScrollValueOfAnAxe (
		float difference, float playerPosition, float camPosition, float offset, float delta = 0.1f) {
		// delta: para x (por exemplo) do player dentro do intervalo 
		//(camera.x -delta, camera + delta) o valor do scroll eh 1  

		if (playerPosition < (camPosition + offset - delta)) { // player esta a esquerda da camera
			if (difference < 0) { // se esta se distanciando da camera
				return 1 + scrollMultiplier;			
			} else if (difference > 0) { // se esta indo em direcao a camera
				return 1 - scrollMultiplier;
			}
		} else if (playerPosition > (camPosition + offset + delta)) { // player esta a direita da camera
			if (difference < 0) { // se esta indo em direcao a camera
				return 1 - scrollMultiplier;			
			} else if (difference > 0) { // se esta se distanciando da camera
				return 1 + scrollMultiplier;
			}
		} else { // player e camera estao na mesma posicao no eixo x
			return 1;
		}

		return 1;
	}

	static float DifferenceOutOfBounds ( float differenceAxis, float windowAxis ) {
		float difference;

		// We're seeing here if the player has overshot it at all on this axis. If not, we just set the 
		// difference to zero. This is because if we subtract the boundary distance when the player isn't far 
		// from the camera, we'll needlessly compensate, and screw up the camera.
		if (Mathf.Abs (differenceAxis) <= windowAxis/2)
			difference = 0f;
		// And if the player legit overshot the boundary, we subtract the boundary from the distance.
		else
			difference = differenceAxis - (windowAxis/2) * Mathf.Sign (differenceAxis);


		//Returns something if the overshot was legit, and zero if it wasn't.
		return difference;

	}


	private int GetPlayerCurrentFloor () {
		int currentFloor = 0;
		float minDist = Mathf.Infinity;

		for (int i = 0; i < floorsLimits.Length; i++) {
			
			if ((player.transform.position.y >= floorsLimits [i].y) && 
				(player.transform.position.y <= floorsLimits [i].yMax + 1)) {
				return i;
			}

			float distance = Mathf.Abs (floorsLimits [i].y - player.transform.position.y);
			if (distance < minDist) {
				currentFloor = i;
				minDist = distance;
			}
		}

		return currentFloor;
	}
	public Vector3 GetPreviousPositionDifference () {
		return transform.position - previousPosition;
	}
}
