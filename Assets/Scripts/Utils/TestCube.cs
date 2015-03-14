using UnityEngine;
using System.Collections;

public class TestCube : MonoBehaviour {

	public void onLoadcastComplete(RaycastReceiver rr)
	{
		gameObject.SetActive (false);
	}
}
