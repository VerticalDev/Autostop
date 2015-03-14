using UnityEngine;
using System.Collections;

public class Headbang : MonoBehaviour {

	public float necessaryAngleVerti = 15f;
	public float necessaryAngleHori = 20f;
	private int necessaryMovementRequired;
	private int necessaryMovementSuccessful;

	public bool vertical;

	public GameObject objectToCall;
	public string methodName;

	private Vector3 oldForwardVerti;
	private Vector3 oldForwardHori;
	public Transform verticalGameObject;
	// Use this for initialization
	void Start () {
		oldForwardHori = transform.forward;
		oldForwardVerti = verticalGameObject.forward;
	}


	public void startChecking(bool vertical, int requiredMovement, GameObject objectToCall, string methodName)
	{
		if (IsInvoking ("checkHeadBang")) endChecking ();
		this.vertical = vertical;
		this.objectToCall = objectToCall;
		this.methodName = methodName;
		necessaryMovementRequired = requiredMovement;
		InvokeRepeating ("checkHeadBang", 0f, 0.1f);
	}

	public void endChecking()
	{
		CancelInvoke ("checkHeadBang");
	}
	
	// Update is called once per frame
	public void checkHeadBang () {
		Vector3 actualForward = vertical ? verticalGameObject.forward : transform.forward;
		Vector3 actualOldForward = vertical ? oldForwardVerti : oldForwardHori;
		if (Vector3.Angle (actualForward, actualOldForward) > (vertical ? necessaryAngleVerti : necessaryAngleHori)) {

			Vector3 A = transform.position + actualOldForward;
			Vector3 B = transform.position + actualForward;
			Vector3 AB = B - A;

			if(vertical && Mathf.Abs(AB.y) > Mathf.Abs(AB.x))
			{
				necessaryMovementSuccessful++;
			}else if(!vertical && Mathf.Abs(AB.x) > Mathf.Abs(AB.y)){
				necessaryMovementSuccessful++;
			}

			if(necessaryMovementSuccessful >= necessaryMovementRequired)
			{
				objectToCall.SendMessage(methodName);
			}
		}

		if (vertical) {
			oldForwardVerti = verticalGameObject.forward;
		} else {
			oldForwardHori = transform.forward;
		}

	}
}
