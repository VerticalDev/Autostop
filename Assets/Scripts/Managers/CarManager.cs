using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarManager : MonoBehaviour {

	public static CarManager instance;	
	void Awake(){if(instance == null){instance = this;}}

	public List<Car> cars = new List<Car>();
	public float timeInCar = 10f; 

	// Use this for initialization
	void Start () {
		Invoke ("leaveCar", timeInCar);
	}
	
	public void leaveCar(){
		Debug.Log ("Leaving car now");
		GameManager.instance.gotoRandomRoad ();	
	}
}
