using System.Collections;
using UnityEngine;

public class interactableObject : MonoBehaviour {

	public virtual void OnTriggerPressDown(grabObject wand){}
	public virtual void OnTriggerPressUp(grabObject wand){}
	public virtual void OnGripPressDown(grabObject wand){}
	public virtual void OnGripPressUp(grabObject wand){}

}
