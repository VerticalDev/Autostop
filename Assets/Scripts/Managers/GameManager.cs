using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager instance;
	void Awake(){if (instance == null) {instance = this;DontDestroyOnLoad(this);}}

	AsyncOperation async = null;

	public enum carType {
		van,
		volvo,
		caravan,
		cadillac
	}

	public enum roadType {
		summer1,
		summer2,
		winter1,
		winter2
	}

	public float winterProbability = 0.3f;

	public int carsTaken = 0; 

	public bool goToRandomRoadAtStart = true;

	// Use this for initialization
	void Start () {
		if(goToRandomRoadAtStart) gotoRoad (roadType.summer1);
	}

	public void gotoRoad(roadType type){
		async = Application.LoadLevelAsync ("Road" + type.ToString ());
	}

	public void gotoRandomRoad(){
		string roadName = "Road";
		roadName = string.Concat(roadName, ((Random.value<winterProbability) ? "winter": "summer"));
		roadName = string.Concat(roadName, Random.Range(1,3).ToString());
		Debug.Log ("Goin' to : " + roadName);
		async = Application.LoadLevelAsync (roadName);
	}

	public void gotoCar(carType type){
		async = Application.LoadLevelAsync ("Car" + type.ToString ());
	}

	// Update is called once per frame
	void Update () {
		if (async!=null && async.progress > 0 && async.progress < 1) {
			Debug.Log("Loadin'");
		}

		if(Input.GetKeyDown(KeyCode.F1)){
			gotoRandomRoad();
		}
	}
}
