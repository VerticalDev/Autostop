using UnityEngine;
using System.Collections;

public class EncounterManager : MonoBehaviour {

	public enum EncounterType
	{
		VACHE,
		MOUETTE,
		FATIGUE,
		NONE
	}


	public EncounterType currentEncounterType;
	
	void Start()
	{
		//StartCoroutine (encounterRoutine ());
	}

	IEnumerator encounterRoutine()
	{
		while (true) {

		}
	}
}
