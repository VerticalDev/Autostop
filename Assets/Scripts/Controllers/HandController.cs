using UnityEngine;
using System.Collections;

public class HandController : MonoBehaviour {

	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		Events.instance.RaycastReceiverEvent += new RaycastReceivedHandler (onRaycast);
	}

	void OnDestroy () {
		Events.instance.RaycastReceiverEvent -= new RaycastReceivedHandler (onRaycast);
	}

	void Update()
	{
		if (up && Time.time > lastRaycastCar + 1f) {
			animator.SetTrigger("Down");
			up = false;
		}
	}

	private float lastRaycastCar;
	private bool up;
	public void onRaycast(RaycastReceiver rr)
	{
		if (rr.GetComponent<CarBehaviour> () != null) {
			if(!up)
			{
				animator.SetTrigger("Up");
			}

			up = true;
			lastRaycastCar = Time.time;

		}
	}
}
