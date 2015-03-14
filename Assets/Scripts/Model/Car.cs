using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Car  {

	public string carId; 
	public List<Passenger> passengers = new List<Passenger>();
	public List<Dialog> dialogs = new List<Dialog>();
	public string leaveDialog;
	
}
