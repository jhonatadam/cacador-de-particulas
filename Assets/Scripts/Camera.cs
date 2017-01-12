using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public GameObject player;


	public float scrollMultiplier = 1.8f;
	public Vector2 movementWindowSize;
	public Vector2 windowOffset;


	// Limites da camera
	public bool limitCameraMovement = true;
	public float topLimit;
	public float bottomLimit;
	public Vector2[] lateralLimits;


	[HideInInspector] 
	public bool activeTracking = true;


	private Vector3 cameraPosition;
	private Vector3 playerPosition;
	private Vector3 previousPlayerPosition;
	private Rect windowRect;


	void Start () {
		cameraPosition = transform.position;

		previousPlayerPosition = player.transform.position;


		//These are the root x/y coordinates that we will use to create our boundary rectangle.
		//Starts at the lower left, and takes the offset into account.
		float windowAnchorX = cameraPosition.x - movementWindowSize.x/2 + windowOffset.x;
		float windowAnchorY = cameraPosition.y - movementWindowSize.y/2 + windowOffset.y;

		//From our anchor point, we set the size of the window based on the public variable above.
		windowRect = new Rect(windowAnchorX, windowAnchorY, movementWindowSize.x, movementWindowSize.y);
	}


	void LateUpdate()
	{
		playerPosition = player.transform.position;

		//Only worry about updating the camera based on player position if the player has actually moved.
		//If the tracking isn't active at all, we don't bother with any of this crap.
		if ( activeTracking && playerPosition != previousPlayerPosition )
		{

			cameraPosition = transform.position;

			//Get the distance of the player from the camera.
			Vector3 playerPositionDifference = playerPosition - previousPlayerPosition;

			//Move the camera this direction, but faster than the player moved.
			Vector3 multipliedDifference = playerPositionDifference * scrollMultiplier;

			cameraPosition += multipliedDifference;

			//updating our movement window root location based on the current camera position
			windowRect.x = cameraPosition.x - movementWindowSize.x/2 + windowOffset.x;
			windowRect.y = cameraPosition.y - movementWindowSize.y/2 + windowOffset.y;

			// We may have overshot the boundaries, or the player just may have been moving too 
			// fast/popped into another place. This corrects for those cases, and snaps the 
			// boundary to the player.
			if(!windowRect.Contains(playerPosition))
			{
				Vector3 positionDifference = playerPosition - cameraPosition;
				positionDifference.x -= windowOffset.x;
				positionDifference.y -= windowOffset.y;

				//I made a function to figure out how much to move in order to snap the boundary to the player.
				cameraPosition.x += DifferenceOutOfBounds( positionDifference.x, movementWindowSize.x );


				cameraPosition.y += DifferenceOutOfBounds( positionDifference.y, movementWindowSize.y );

			}

			// Here we clamp the desired position into the area declared in the limit variables.
			if( limitCameraMovement )
			{
				cameraPosition.y = Mathf.Clamp ( cameraPosition.y, bottomLimit, topLimit );
				//cameraPosition.x = Mathf.Clamp ( cameraPosition.x, limitLeft, limitRight );
			}

			// and now we're updating the camera position using what came of all the calculations above.
			transform.position = cameraPosition;

		}

		previousPlayerPosition = playerPosition;
	}

	static float DifferenceOutOfBounds ( float differenceAxis, float windowAxis )
	{
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
