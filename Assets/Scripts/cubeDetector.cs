using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeDetector : MonoBehaviour {
	public EnableTeleporter blueTeleporter;
	public bool ayeAmNeo;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.GetComponent<grabby>() != null){
			blueTeleporter.cubePresent (ayeAmNeo);



		}
	}
}
