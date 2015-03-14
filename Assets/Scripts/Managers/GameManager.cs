using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	void Awake(){if (instance == null) {instance = this;DontDestroyOnLoad(this);}}

	AsyncOperation async = new AsyncOperation();

	public enum carType {
		van,
		volvo,
		caravan,
		cadillac
	}

	public enum roadType {
		summer,
		summer2,
		winter
	}

	public int carsTaken = 0; 

	// Use this for initialization
	void Start () {
		gotoRoad (roadType.summer);
	}

	public void gotoRoad(roadType type){
		async = Application.LoadLevelAsync ("Road" + type.ToString ());
	}

	public void gotoCar(carType type){
		async = Application.LoadLevelAsync ("Car" + type.ToString ());
	}

	// Update is called once per frame
	void Update () {
		if (async.progress > 0 && async.progress < 1) {
			Debug.Log("Loadin'");
		}
	}
}
