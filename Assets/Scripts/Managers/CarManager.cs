using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarManager : MonoBehaviour {

	public static CarManager instance;	
	void Awake(){if(instance == null){instance = this;DontDestroyOnLoad(this);}}

	public List<Car> cars = new List<Car>();

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
