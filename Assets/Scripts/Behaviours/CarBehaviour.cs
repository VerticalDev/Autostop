using UnityEngine;
using System.Collections;

public class CarBehaviour : MonoBehaviour {

	public Transform dest;
	NavMeshAgent agent;

	void Awake(){
		agent = GetComponent<NavMeshAgent> ();
	}

	// Use this for initialization
	void Start () {
		agent.SetDestination (dest.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (agent.remainingDistance <= agent.stoppingDistance)
			Destroy (gameObject);
	}
}
