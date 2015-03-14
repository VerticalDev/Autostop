using UnityEngine;
using System.Collections;

public class CarBehaviour : MonoBehaviour {

	public Transform dest;
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
	}
	
	// Update is called once per frame
	void Update () {
		if (!pulled && agent.remainingDistance <= agent.stoppingDistance)
			Destroy (gameObject);
	}

	public void onLoadcastComplete(){
		pulled = true;
		Transform target = GameObject.Find("carStop").transform;	
		agent.SetDestination (target.position); 
	}
}
