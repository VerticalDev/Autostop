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

	public float encounterMinStart = 10f;
	public float encounterMaxStart = 20f;

	public float maxEncounterDuration = 10f;

	public Headbang headbang;
	public AudioSource asource;
	public AudioClip clipWoosh;

	bool waitingEndOfEvent = false;

	public GameObject bee;

	public bool noEncounterHere = false;
	
	void Start()
	{

		if(!noEncounterHere && VacheSpawner.instance != null && headbang != null) StartCoroutine (encounterRoutine ());
	}

	IEnumerator encounterRoutine()
	{


		while (true) {

			yield return new WaitForSeconds(Random.Range(encounterMinStart, encounterMaxStart));

			waitingEndOfEvent = true;
			int encounter = Random.Range(0, 3);
			currentEncounterType = (EncounterType)encounter;
			switch(currentEncounterType)
			{
			case EncounterType.VACHE:
				VacheSpawner.instance.putVache();
				break;
			case EncounterType.MOUETTE:
				bee.SetActive(true);
				headbang.startChecking(false, 5, this.gameObject, "endOfEvent");
				UIManager.instance.displayDialog("AAAAH ! SECOUEZ LA TETE DE DROITE A GAUCHE !", maxEncounterDuration);
				UIManager.instance.displayArrow(true, true, false, false, maxEncounterDuration);
				break;
			case EncounterType.FATIGUE:
				UIManager.instance.startTired();
				asource.Play();
				break;
			}

			float timePast = 0f;
			while(waitingEndOfEvent && timePast < maxEncounterDuration)
			{
				if(currentEncounterType == EncounterType.FATIGUE && UIManager.instance.tiredFinished) endOfEvent();
				if(currentEncounterType == EncounterType.VACHE && VacheSpawner.instance.currentVache == null) endOfEvent();
				timePast += Time.deltaTime;
				yield return 0;
			}

			if(timePast >= maxEncounterDuration)
			{
				switch(currentEncounterType)
				{
				case EncounterType.VACHE:
					VacheSpawner.instance.deputVache();
					break;
				case EncounterType.MOUETTE:
					headbang.endChecking();
					asource.PlayOneShot (clipWoosh);
					bee.SetActive(false);
					UIManager.instance.destroyArrow();
					UIManager.instance.destroyDialog();
					break;
				case EncounterType.FATIGUE:
					UIManager.instance.finishTired();
					asource.PlayOneShot (clipWoosh);
					break;
				}
			}


			yield return 0;
		}
	}

	public void endOfEvent()
	{
		if (currentEncounterType != EncounterType.VACHE)
			asource.PlayOneShot (clipWoosh);
		bee.SetActive(false);
		headbang.endChecking();
		UIManager.instance.destroyArrow();
		UIManager.instance.destroyDialog();
		waitingEndOfEvent = false;
	}
}
