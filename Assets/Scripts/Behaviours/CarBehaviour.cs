using UnityEngine;
using System.Collections;

public class CarBehaviour : MonoBehaviour {

	public Transform dest;
	public Transform limit;
	NavMeshAgent agent;

	public GameManager.carType type;

	public bool pulled = false;

	void Awake(){
		agent = GetComponent<NavMeshAgent> ();
	}

	// Use this for initialization
	void Start () {
		if (dest != null) {
			agent.SetDestination (dest.position);
			if(dest.name.Contains("Target1")) GetComponent<Collider>().enabled = false;
		}
		GameObject player = GameObject.Find ("CardboardMain");
		if (player != null) {
			limit = player.transform;
		}
	}

	public void moveToDest(){
		agent.SetDestination (dest.position);

	}
	
	// Update is called once per frame
	void Update () {
		if (!pulled && agent.remainingDistance>0 && (agent.remainingDistance <= agent.stoppingDistance))
			Destroy (gameObject);

		if (!pulled && limit != null && transform.position.x > limit.position.x) {
			GetComponent<Collider> ().enabled = false;
			//Bruit de klaxons
		}
			
	}

	public void onLoadcastComplete(){
		if (VacheSpawner.instance==null || !VacheSpawner.instance.closetoplayer) {
			pulled = true;
			Transform target = GameObject.Find("carStop").transform;	
			agent.SetDestination (target.position); 
			CardboxRaycaster.instance.off = true;
		}

		Invoke ("leaveScene", 2f);
	}

	public void leaveScene(){
		GameManager.instance.gotoCar(type);
	}
}
