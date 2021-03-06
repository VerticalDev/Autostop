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

	public List<AudioClip> klaxons;

	public float winterProbability = 0.3f;

	public int carsTaken = 0; 

	public bool goToRandomRoadAtStart = true;

	public bool alreadyGoToOne = false;

	// Use this for initialization
	void Start () {
		if (goToRandomRoadAtStart) {
			gotoRoad (roadType.summer1);
			alreadyGoToOne = true;
		}
	}

	public void gotoRoad(roadType type){
		async = Application.LoadLevelAsync ("Road" + type.ToString ());
	}

	public void gotoRandomRoad(){
		string roadName = "Road";
		roadName = string.Concat(roadName, ((Random.value<winterProbability) ? "winter": "summer"));
		roadName = string.Concat(roadName, Random.Range(1,3).ToString());
		if (roadName == "Roadsummer1" && alreadyGoToOne)
			roadName = "Roadsummer2";
		Debug.Log ("Goin' to : " + roadName);
		if (UIManager.instance != null) {
			UIManager.instance.changeLoadingSprite(UIManager.instance.surlaroute);
			UIManager.instance.enableLoading(true);
		}
		async = Application.LoadLevelAsync (roadName);
	}

	public void gotoCar(carType type){
		carsTaken++; 
		if (UIManager.instance != null) {
			UIManager.instance.changeLoadingSprite(UIManager.instance.embarquement);
			UIManager.instance.enableLoading(true);
		}
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
