using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Player player;

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
		GetComponent <Camera> ().aspect = 16f/9f;
		cameraPosition = transform.position;

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

			float scrollValue = defineScrollValue ();

			//Move the camera this direction, but faster than the player moved.
			Vector3 multipliedDifference = player.GetPreviousPositionDifference () * scrollValue;

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
				Rect currentFloorLimits = floorsLimits [player.currentFloor];
				cameraPosition.y = Mathf.Clamp ( cameraPosition.y, currentFloorLimits.yMax, currentFloorLimits.y);
				cameraPosition.x = Mathf.Clamp ( cameraPosition.x, currentFloorLimits.x, currentFloorLimits.xMax);
			}

			// and now we're updating the camera position using what came of all the calculations above.
			transform.position = cameraPosition;

		}
	}

	private float defineScrollValue () {
		float xDifference = player.GetPreviousPositionDifference ().x;

		// para x do player dentro do intervalo (camera.x -delta, camera + delta) o valor do scroll eh 1 
		float delta = 0.1f; 

		if (player.transform.position.x < (transform.position.x - delta)) { // player esta a esquerda da camera
			if (xDifference < 0) { // se esta se distanciando da camera
				return 1 + scrollMultiplier;			
			} else if (xDifference > 0) { // se esta indo em direcao a camera
				return 1 - scrollMultiplier;
			}
		} else if (player.transform.position.x > (transform.position.x + delta)) { // player esta a direita da camera
			if (xDifference < 0) { // se esta indo em direcao a camera
				return 1 - scrollMultiplier;			
			} else if (xDifference > 0) { // se esta se distanciando da camera
				return 1 + scrollMultiplier;
			}
		} else { // player e camera estao na mesma posicao no eixo x
			return 1;
		}

		return 	1;
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

}
