using UnityEngine;
using System.Collections;
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
		foreach (string s in d.dialog) {
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

