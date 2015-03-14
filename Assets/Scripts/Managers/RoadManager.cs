using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour {

	public static RoadManager instance;
	void Awake(){if (instance == null) {instance = this; }}

	public List<Transform> carSpawnTransform;
	public float carSpawnProbability;

	public List<GameObject> cars = new List<GameObject>();
	public List<GameObject> displayedCars = new List<GameObject>();

	// Use this for initialization
	void Start () {
		StartCoroutine ("carSpawnRoutine");
	}

	void Update()
	{

	}

	public IEnumerator carSpawnRoutine(){
		yield return new WaitForSeconds(2f);
		if(Random.value<carSpawnProbability) instanciateCar (Random.Range(0,cars.Count),0); 	
		if(Random.value<carSpawnProbability) instanciateCar (Random.Range(0,cars.Count),1); 
		StartCoroutine ("carSpawnRoutine");
	}
	
    public void instanciateCar(int carNum,int lane){
		GameObject car = (GameObject)Instantiate ( (Object)cars [carNum], carSpawnTransform[lane].position, Quaternion.identity);
		Transform mesh = car.transform.GetChild (0);
		if (lane == 1) car.GetComponent<Collider>().enabled = false;
		mesh.GetComponent<Renderer> ().material.mainTextureOffset = new Vector2 (0, -Random.Range(0,3)*0.046875f);
		car.name = "car";
		displayedCars.Add (car);

		CarBehaviour cb = car.GetComponent<CarBehaviour> ();
		cb.dest = GameObject.Find ("CarTarget"+lane.ToString()).transform;
		cb.moveToDest ();
	}
}
