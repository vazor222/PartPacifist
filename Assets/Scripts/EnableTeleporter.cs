using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTeleporter : MonoBehaviour {
	public GameObject blueTeleporterObject;

	private bool cubeOnePresent;
	private bool cubeTwoPresent;



	// Use this for initialization
	void Start () {
		cubeOnePresent = false;
		cubeTwoPresent = false;
		blueTeleporterObject.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
	//detects the cube being placed on pedestal
	public void cubePresent(bool pedestal){
		if (pedestal == true) {
			cubeOnePresent = true;
		}
		else {
			cubeTwoPresent = true;

		}
		//activate teleporter if both parameters
		if (cubeOnePresent == true && cubeTwoPresent == true)
			teleporterActivate ();

	}
	//detects the cubes being removed
	public void cubeRemoved(bool pedestal){
		if (pedestal == true) {
			cubeOnePresent = false;
		}
		else {
			cubeTwoPresent = false;

		}
		if (cubeOnePresent == false || cubeTwoPresent == false)
			teleporterDeactivate();
	}

	private void teleporterActivate(){
		blueTeleporterObject.SetActive (true);


	}

	private void teleporterDeactivate(){
		blueTeleporterObject.SetActive (false);

	}

}

