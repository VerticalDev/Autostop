using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public delegate void RaycastReceivedHandler(RaycastReceiver rr);

public class Events : MonoBehaviour {

	//Singleton
	static Events mInst;
	static public Events instance { get { return mInst; } }
	
	void Awake () {
		if(mInst == null) mInst = this;
		DontDestroyOnLoad(this); 
	}



	public event RaycastReceivedHandler RaycastReceiverEvent;		
	public void FireRaycastReceivedHandler(RaycastReceiver rr){
		if(RaycastReceiverEvent != null){
			RaycastReceiverEvent(rr);	
		}
	}
}
