using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLinkTeleporter : MonoBehaviour
{
	public float POSITION_CHECK_TIME_LENGTH_IN_SECONDS = 3;
	public GameObject myDestination;
	public GameObject headCannon;
	public GameObject preTeleportPosistion;

	private float positionCheckStartTime;

	// Use this for initialization
	void Start()
	{
		positionCheckStartTime = 0;
		Debug.Log("Current preTeleportPosistion:" +preTeleportPosistion.transform.position );
		Debug.Log("Current myDestination:" +myDestination.transform.position );
	}

	// Update is called once per frame
	void Update()
	{
		//Debug.Log("Current playerRig:" +playerRig.transform.position );
		//Debug.Log("Current targetLocation:" +targetLocation.transform.position );
	}

	void OnTriggerEnter(Collider other)
	{
		if (!this.gameObject.activeSelf)
			return;
		teleportCheck(other);

	}

	void OnTriggerStay(Collider other)
	{
		if (!this.gameObject.activeSelf)
			return;
		teleportCheck(other);
	}

	private void teleportCheck(Collider other)
	{
		if (positionCheckStartTime == 0)
		{
			if (other.gameObject == headCannon && facingMostlyForward(other.gameObject.transform.forward))
			{
				// start timer
				positionCheckStartTime = Time.time;
				Debug.Log ("TimerStarted:" + positionCheckStartTime);	
			}
		}
		else
		{
			if( Time.time - positionCheckStartTime >= POSITION_CHECK_TIME_LENGTH_IN_SECONDS )
			{
				Debug.Log ("Teleport time? other.gameObject:"+other.gameObject+" headCannon:"+headCannon);
				if (other.gameObject == headCannon && facingMostlyForward (other.gameObject.transform.forward)) {
					// teleport now!
					Debug.Log ("Teleported!");
					//Debug.Log ("preTeleportPosistion when teleporting:" + preTeleportPosistion.transform.position);
					//Debug.Log ("myDestination when teleporting:" + myDestination.transform.position);
					float x = 0;
					float y = .5f;
					float z = 0f;
					Vector3 offset = preTeleportPosistion.transform.position - headCannon.transform.position + new Vector3 (x, y, z);

					//Vector3 detectorHeight = new Vector3 (0, 2.5, 0);
					//This is done because the center of the detector is 2.5 units lower than the height of the player.

					preTeleportPosistion.transform.position = myDestination.transform.position + offset ; //OriginTransform.position = Pointer.SelectedPoint + offset;
					//Debug.Log ("myDestination after teleporting:" + myDestination.transform.position);
					//Debug.Log("preTeleportPosistion after teleporting:" +preTeleportPosistion.transform.position );
				} else
					Debug.Log ("was not other or matching forward");
				positionCheckStartTime = 0;
			}
		}
	}

	private bool facingMostlyForward(Vector3 playerCurrentForward)
	{
		bool dotResult = Vector3.Dot(playerCurrentForward, gameObject.transform.forward) > 0;
		Debug.Log ("checking facing: " + playerCurrentForward + " gameObject.transform.forward:"+gameObject.transform.forward+" result:"+dotResult);
		return dotResult;
	}
}
