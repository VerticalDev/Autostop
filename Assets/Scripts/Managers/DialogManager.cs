using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	public static DialogManager instance;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		}
	}

	public bool isRunningDialog = false;

	public string carId;

	void Start()
	{
		Invoke ("lastDialog", CarManager.instance.timeInCar - 3f);
	}

	Coroutine currentRoutine;

	public void playDialogInCar(Dialog dialog)
	{
		if (!isRunningDialog) {
			isRunningDialog = true;
			currentRoutine = StartCoroutine(dialogRoutine(dialog));
		}
	}

	private Text associatedText;
	IEnumerator dialogRoutine(Dialog d)
	{
		List<string> dialogToShow = new List<string>();
		int dialogId = Random.Range (0, 3);

		switch (dialogId) {
		case 0:
			dialogToShow = d.dialog;
			break;
		case 1:
			if(d.altdialog.Count > 0)
			{
				dialogToShow = d.altdialog;
			}else{
				dialogToShow = d.dialog;
			}
			break;
		case 2:
			if(d.terdialog.Count > 0)
			{
				dialogToShow = d.terdialog;
			}else{
				if(d.altdialog.Count > 0)
				{
					dialogToShow = Random.Range(0, 2) == 0 ? d.dialog : d.altdialog;
				}else{
					dialogToShow = d.dialog;
				}
			}
			break;
		}



		foreach (string s in dialogToShow) {
			Transform box = transform.FindChild(s.Split(';')[0]);
			BoxDialogController bdc = box.GetComponent<BoxDialogController>();

			associatedText = bdc.associatedText;
			associatedText.text = s.Split(';')[1];

			yield return new WaitForSeconds(4f);

			associatedText.text = "";
		}
		isRunningDialog = false;

	}

	public void lastDialog()
	{
		if(currentRoutine != null) StopCoroutine (currentRoutine);
		string s = CarManager.instance.cars.Find (c => c.carId == carId).leaveDialog;
		associatedText.text = "";
		Transform box = transform.FindChild(s.Split(';')[0]);
		BoxDialogController bdc = box.GetComponent<BoxDialogController>();
		
		bdc.associatedText.text = s.Split(';')[1];
	}
}

