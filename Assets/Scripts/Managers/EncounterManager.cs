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

	public VacheSpawner spawner;
	public Headbang headbang;

	bool waitingEndOfEvent = false;
	
	void Start()
	{

		if(spawner != null && headbang != null) StartCoroutine (encounterRoutine ());
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
				spawner.putVache();
				break;
			case EncounterType.MOUETTE:
				headbang.startChecking(false, 10, this.gameObject, "endOfEvent");
				UIManager.instance.displayDialog("AAAAH ! SECOUEZ LA TETE DE DROITE A GAUCHE !", 7f);
				UIManager.instance.displayArrow(true, true, false, false, 7f);
				break;
			case EncounterType.FATIGUE:
				UIManager.instance.startTired();
				break;
			}

			float timePast = 0f;
			while(waitingEndOfEvent && timePast < maxEncounterDuration)
			{
				if(currentEncounterType == EncounterType.FATIGUE && UIManager.instance.tired) endOfEvent();
				if(currentEncounterType == EncounterType.VACHE && spawner.currentVache == null) endOfEvent();
				timePast += Time.deltaTime;
				yield return 0;
			}

			if(timePast >= maxEncounterDuration)
			{
				switch(currentEncounterType)
				{
				case EncounterType.VACHE:
					spawner.deputVache();
					break;
				case EncounterType.MOUETTE:
					headbang.endChecking();
					UIManager.instance.destroyArrow();
					UIManager.instance.destroyDialog();
					break;
				case EncounterType.FATIGUE:
					UIManager.instance.finishTired();
					break;
				}
			}


			yield return 0;
		}
	}

	public void endOfEvent()
	{
		waitingEndOfEvent = false;
	}
}
