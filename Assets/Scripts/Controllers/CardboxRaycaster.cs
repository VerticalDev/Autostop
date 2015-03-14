using UnityEngine;
using System.Collections;

public class CardboxRaycaster : MonoBehaviour {

	Ray ray;
	RaycastHit hit;
	Camera cam;
	int mask;

	public bool off = false;

	public static CardboxRaycaster instance;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		}
	}
	
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
		mask = (1 << LayerMask.NameToLayer("RaycastTarget"));
	}
	
	// Update is called once per frame
	void Update () {
		if (off)
			return;
		ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
		if (Physics.Raycast (ray, out hit, 40f, mask)) {
			if(hit.collider != null && hit.collider.GetComponent<RaycastReceiver>() != null)
			{
				Events.instance.FireRaycastReceivedHandler(hit.collider.GetComponent<RaycastReceiver>());
			}
		}
	}
}
