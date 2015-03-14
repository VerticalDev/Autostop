using UnityEngine;
using System.Collections;

public class RaycastReceiver : MonoBehaviour {

	public string methodName = "onRaycastLoad";
	public string actionMethod = "onLoadcastComplete";

	Collider colliderObj;

	// Use this for initialization
	void Start () {
		colliderObj = GetComponent<Collider> ();
		Events.instance.RaycastReceiverEvent += new RaycastReceivedHandler (onRaycastReceived);
	}

	void OnDestroy()
	{
		Events.instance.RaycastReceiverEvent -= new RaycastReceivedHandler (onRaycastReceived);
	}
	
	public void onRaycastReceived(RaycastReceiver rr)
	{
		if (rr == this && methodName != "onRaycastReceived") {
			this.SendMessage(methodName, this, SendMessageOptions.DontRequireReceiver);
		}
	}

	public void onRaycastComplete()
	{
		colliderObj.enabled = false;
		if (actionMethod != "onRaycastComplete") 
			this.SendMessage(actionMethod, this, SendMessageOptions.DontRequireReceiver);


	}
}
