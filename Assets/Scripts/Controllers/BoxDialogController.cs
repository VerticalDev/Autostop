using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BoxDialogController : MonoBehaviour {

	public Text associatedText;

	Dialog dialog;


	bool alreadyLaunched = false;
	void Start()
	{
		Car car = CarManager.instance.cars.Find (c => c.carId == DialogManager.instance.carId);
		if (car != null) {
			dialog = car.dialogs.Find(c => c.passengerName == this.gameObject.name);
		}
	}

	public void onLoadcastComplete()
	{
		if (!alreadyLaunched) {
			if(!DialogManager.instance.isRunningDialog) alreadyLaunched = true;
			DialogManager.instance.playDialogInCar(dialog);
		}
	}
}
