using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabby : MonoBehaviour {
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


	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		interactionPoint = new GameObject ().transform;
		velocityfactor /= rigidbody.mass;
		rotationfactor /= rigidbody.mass;
	}
	
	// Update is called once per frame
	void Update () {
		if (attachedWand && currentlyInteracting) {
			posDelta = attachedWand.transform.position - interactionPoint.position;
			this.rigidbody.velocity = posDelta * velocityfactor * Time.fixedDeltaTime;

			rotationDelta = attachedWand.transform.rotation * Quaternion.Inverse (interactionPoint.rotation);
			rotationDelta.ToAngleAxis (out angle, out axis);

			if (angle > 180) {
				angle -= 360;
			}

			this.rigidbody.angularVelocity = (Time.fixedDeltaTime * angle * axis) * rotationfactor;
		}
	}

	public override void OnGripPressDown(grabObject wand){
		attachedWand = wand;
		interactionPoint.position = wand.transform.position;
		interactionPoint.rotation = wand.transform.rotation;
		interactionPoint.SetParent (transform, true);

	}


	public override void OnGripPressUp(grabObject wand){
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
