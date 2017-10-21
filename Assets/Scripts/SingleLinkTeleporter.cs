using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLinkTeleporter : MonoBehaviour
{
	public float POSITION_CHECK_TIME_LENGTH_IN_SECONDS = 3;
	public GameObject targetLocation;
	public GameObject player;

	private float positionCheckStartTime;

	// Use this for initialization
	void Start()
	{
		positionCheckStartTime = 0;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnTriggerEnter(Collider other)
	{
		teleportCheck(other);
	}

	void OnTriggerStay(Collider other)
	{
		teleportCheck(other);
	}

	private void teleportCheck(Collider other)
	{
		if (positionCheckStartTime == 0)
		{
			if (other.gameObject == player && facingMostlyForward(other.gameObject.transform.forward))
			{
				// start timer
				positionCheckStartTime = Time.time;
			}
		}
		else
		{
			if( Time.time - positionCheckStartTime >= POSITION_CHECK_TIME_LENGTH_IN_SECONDS )
			{
				if (other.gameObject == player && facingMostlyForward(other.gameObject.transform.forward))
				{
					// teleport now!
					player.transform.position = targetLocation.transform.position;
				}
				positionCheckStartTime = 0;
			}
		}
	}

	private bool facingMostlyForward(Vector3 playerCurrentForward)
	{
		return Vector3.Dot(playerCurrentForward, gameObject.transform.forward) > 0;
	}
}
