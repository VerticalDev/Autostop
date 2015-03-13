using UnityEngine;
using System.Collections;

public class RotationHelper : MonoBehaviour {


	Transform _trans;
	int _idCoroutine = 0;

	void Start()
	{
		_trans = this.transform;
	}


	public void rotateInTime(Vector3 angle, float time)
	{
		_idCoroutine++;
		StartCoroutine (rotateCoroutine (angle, time));

	}

	IEnumerator rotateCoroutine(Vector3 angle, float time)
	{
		int currentIdCoroutine = _idCoroutine;
		float timePast = 0f;

		Vector3 currentEulerAngle = _trans.localEulerAngles;
		Vector3 modifiedEulerAngle = currentEulerAngle;
		Vector3 targetEulerAngle = currentEulerAngle + angle;

		Vector3 velocity = Vector3.zero;

		while (timePast < time) {
			if(currentIdCoroutine != _idCoroutine) timePast = time;

			modifiedEulerAngle = Vector3.SmoothDamp(modifiedEulerAngle, targetEulerAngle, ref velocity, time/4f);
			_trans.localEulerAngles = modifiedEulerAngle;
			timePast += Time.deltaTime;
			yield return 0;
		}

		if(currentIdCoroutine == _idCoroutine)
			_trans.localEulerAngles = targetEulerAngle;
	}
}
