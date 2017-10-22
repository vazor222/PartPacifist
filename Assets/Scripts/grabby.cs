using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabby : interactableObject {
	public GameObject youLosePanel;






	protected Rigidbody rigidbody;
	protected bool currentlyInteracting;
	protected uint itemId;

	private float velocityfactor = 20000f;
	private Vector3 posDelta;

	private float rotationfactor = 600f;
	private Quaternion rotationDelta;
	private float angle;
	private Vector3 axis;

	private grabObject attachedWand;
	private Transform interactionPoint;

	private bool cannotDropGun;
	private AudioSource gunShot;

	// Use this for initialization
	void Start () {
		cannotDropGun = false;
		rigidbody = GetComponent<Rigidbody> ();
		interactionPoint = new GameObject ().transform;
		velocityfactor /= rigidbody.mass;
		rotationfactor /= rigidbody.mass;
		if( youLosePanel )
			youLosePanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (attachedWand && currentlyInteracting) {

			transform.position = attachedWand.transform.position;
			this.transform.rotation = attachedWand.transform.rotation;
			//posDelta = attachedWand.transform.position - interactionPoint.position;
			//this.rigidbody.velocity = posDelta * velocityfactor * Time.fixedDeltaTime;

			//rotationDelta = attachedWand.transform.rotation * Quaternion.Inverse (interactionPoint.rotation);
			//rotationDelta.ToAngleAxis (out angle, out axis);

			//if (angle > 180) {
			//	angle -= 360;

			//}

			this.rigidbody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationfactor;
		}
	}

	public override void OnGripPressDown(grabObject wand){
		Debug.Log ("start gripping");
		gotGrabbedDown (wand);
	}

	public override void OnGripPressUp(grabObject wand){
		Debug.Log ("stop gripping");
		noLongerGrabbed (wand);
	}

	public override void OnTriggerPressDown(grabObject wand){
		Debug.Log ("triggerPressDown tag:"+this.gameObject.tag );
		if (this.gameObject.tag == "Gun") {
			gunShot = this.GetComponent<AudioSource> ();
			gunShot.Play ();
			Debug.Log ("You lose!");
			cannotDropGun = true;
			if( youLosePanel )
				youLosePanel.SetActive(true);
		}
	}

	public override void OnTriggerPressUp(grabObject wand){
	}

	public void gotGrabbedDown(grabObject wand)
	{
		attachedWand = wand;
		interactionPoint.position = wand.transform.position;
		interactionPoint.rotation = wand.transform.rotation;
		interactionPoint.SetParent (transform, true);
		currentlyInteracting = true;	
	}

	public void noLongerGrabbed(grabObject wand)
	{
		if (cannotDropGun )
			return;
		if (wand == attachedWand) {
			attachedWand = null;
			currentlyInteracting = false;
		}
	}

	public bool IsInteracting(){
		return currentlyInteracting;
	}
	private void OnDestroy(){
		if (interactionPoint) {
			Destroy (interactionPoint.gameObject);
		}
	}

}
