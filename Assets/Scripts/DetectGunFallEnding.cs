using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectGunFallEnding : MonoBehaviour
{
	public float GUN_FELL_HEIGHT = -300;
	public GameObject gun;
    public GameObject youWinText;


	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
        if (gun.transform.position.y <= GUN_FELL_HEIGHT)
        {
            youWinText.SetVisible(true);
            this.enabled = false;
        }
	}
}
