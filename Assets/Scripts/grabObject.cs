using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabObject : MonoBehaviour {
	private Valve.VR.EVRButtonId gripSqueeze = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerSqueeze = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input ((int)trackedObj.index); } }
	private SteamVR_TrackedObject trackedObj;

	HashSet<interactableObject> objectsHoveringOver = new HashSet<interactableObject> ();

	private interactableObject closestItem;
	private interactableObject interactingItem;


	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (controller == null) {

		}
		if (controller.GetPressDown (gripSqueeze) || controller.GetPressDown (triggerSqueeze)) {
			float minDistance = float.MaxValue;

			float distance;

			foreach (interactableObject item in objectsHoveringOver){
				distance = (item.transform.position - transform.position).sqrMagnitude;

				if (distance < minDistance) {
					minDistance = distance;
					closestItem = item;
				}
		}

			interactingItem = closestItem;
			closestItem = null;
			if (interactingItem) {
				if (controller.GetPressDown(gripSqueeze)){
					interactingItem.OnGripPressDown(this);
				}
				if (controller.GetPressDown(triggerSqueeze)){
					interactingItem.OnTriggerPressDown(this);
				}	
			}
	}
		if (controller.GetPressUp (triggerSqueeze) && interactingItem != null) {
			interactingItem.OnTriggerPressUp (this);
		}
		if (controller.GetPressUp (gripSqueeze) && interactingItem != null) {
			interactingItem.OnGripPressUp (this);
		}
}
	private void OnTriggerExit(Collider collider){
		interactableObject collidedItem = collider.GetComponent<interactableObject>();
		if (collidedItem) {
			Debug.Log ("removing InteractableObject:" + collidedItem.name);
			objectsHoveringOver.Remove (collidedItem);
		}
	}
	private void OnTriggerEnter(Collider collider){
		interactableObject collidedItem = collider.GetComponent<interactableObject>();
		if (collidedItem) {
			Debug.Log ("adding InteractableObject:" + collidedItem.name);
			objectsHoveringOver.Add (collidedItem);
		}
	}
}
