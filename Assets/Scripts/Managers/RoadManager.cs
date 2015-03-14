using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoadManager : MonoBehaviour {

	public static RoadManager instance;
	void Awake(){if (instance == null) {instance = this; DontDestroyOnLoad(this);}}

	public Transform carSpawnTransform;
	public float carSpawnProbability;

	public List<GameObject> cars = new List<GameObject>();

	// Use this for initialization
	void Start () {
		StartCoroutine ("carSpawnRoutine");
	}

	public IEnumerable carSpawnRoutine(){
		yield return new WaitForSeconds(2f);
		instanciateCar (Random.Range(0,cars.Count-1));
	}
	
    public void instanciateCar(int carNum){
		GameObject car = (GameObject)Instantiate ( (Object)cars [carNum], carSpawnTransform.position, Quaternion.identity);
		car.name = "car";
	}

	// Update is called once per frame
	void Update () {
	
	}
}
