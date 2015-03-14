using UnityEngine;
using System.Collections;

public class CarBehaviour : MonoBehaviour {

	Transform dest;

	// Use this for initialization
	void Start () {
		dest = GameObject.Find ("CarTarget").transform;
		GetComponent<NavMeshAgent> ().SetDestination (dest.position);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
