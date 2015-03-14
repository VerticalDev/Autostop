using UnityEngine;
using System.Collections;

public class CarBehaviour : MonoBehaviour {

	public Transform dest;
	public Transform limit;
	NavMeshAgent agent;

	public bool pulled = false;

	void Awake(){
		agent = GetComponent<NavMeshAgent> ();
	}

	// Use this for initialization
	void Start () {
		if (dest != null) {
			agent.SetDestination (dest.position);
		}
	}

	public void moveToDest(){
		agent.SetDestination (dest.position);
		GameObject player = GameObject.Find ("CardboardMain");
		if (player != null) {
			limit = player.transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!pulled && agent.remainingDistance <= agent.stoppingDistance)
			Destroy (gameObject);
		if (!pulled && limit != null && transform.position.x > limit.position.x) {
			GetComponent<Collider> ().enabled = false;
			//Bruit de klaxons
		}
			
	}

	public void onLoadcastComplete(){
		pulled = true;
		Transform target = GameObject.Find("carStop").transform;	
		agent.SetDestination (target.position); 
		CardboxRaycaster.instance.off = true;
	}
}
