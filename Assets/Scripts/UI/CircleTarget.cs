using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CircleTarget : MonoBehaviour {

	public Image imageCircle;

	private float lastTimeReceiveRaycast;

	public bool sendMessageAtGoal;

	// Use this for initialization
	void Start () {
		imageCircle.fillAmount = 0f;
		Events.instance.RaycastReceiverEvent += new RaycastReceivedHandler (onRaycastReceived);
	}

	void OnDestroy()
	{
		Events.instance.RaycastReceiverEvent -= new RaycastReceivedHandler (onRaycastReceived);
	}
	
	// Update is called once per frame
	void Update () {
		if (imageCircle.fillAmount > 0f && Time.time > lastTimeReceiveRaycast + 0.5f) {
			imageCircle.fillAmount -= Time.deltaTime;
		}
	}

	public void onRaycastReceived(RaycastReceiver rr)
	{
		lastTimeReceiveRaycast = Time.time;
		imageCircle.fillAmount += Time.deltaTime/rr.timeToCharge;

		if (imageCircle.fillAmount >= 1) {
			imageCircle.fillAmount = 0f;
			if(sendMessageAtGoal) rr.onRaycastComplete();
		}
	}
}
