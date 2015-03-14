using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadSystem : MonoBehaviour {

	public List<GameObject> props;
	public float propInterval = 5f;
	public Transform propSpawnLoc;
	public float citySpawnProbability = 0.3f;

	// Use this for initialization
	void Start () {
		InvokeRepeating ("createProp", propInterval,propInterval);
	}

	public void createProp(){
		if(Random.value<citySpawnProbability)
			Instantiate (props [Random.Range (0, props.Count+1)], propSpawnLoc.position, Quaternion.identity);
	}

}
