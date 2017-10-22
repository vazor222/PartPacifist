using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectGunFallEnding : MonoBehaviour
{
	public float GUN_FELL_HEIGHT = -30;
	public GameObject gun;
	public GameObject youWinPanel;


	// Use this for initialization
	void Start()
	{
		youWinPanel.SetActive(false);

	}

	// Update is called once per frame
	void Update()
	{
        if (gun.transform.position.y <= GUN_FELL_HEIGHT)
        {
			youWinPanel.SetActive(true);
            this.enabled = false;
        }
	}
}
