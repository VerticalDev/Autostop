using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;

	public GameObject dialogPrefab;
	public GameObject arrowPrefab;

	public Transform panelLeft;
	public Transform panelRight;

	private GameObject dialogInstLeft;
	private GameObject dialogInstRight;

	private GameObject arrowInstLeft;
	private GameObject arrowInstRight;

	public Text pointLeft;
	public Text pointRight;

	public int numberPoint;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		}
	}

	void Start()
	{
		updatePoints (0);
	}

	public void displayDialog(string text, float time)
	{
		if (dialogInstLeft != null)
			Destroy (dialogInstLeft);
		if (dialogInstRight != null)
			Destroy (dialogInstRight);

		instanceDialog (ref dialogInstLeft, panelLeft, text, time);
		instanceDialog (ref dialogInstRight, panelRight, text, time);
	}

	void instanceDialog(ref GameObject obj, Transform parent, string text, float time)
	{
		obj = Instantiate (dialogPrefab, dialogPrefab.transform.position, dialogPrefab.transform.rotation) as GameObject;
		obj.transform.SetParent(parent);
		obj.transform.localPosition = Vector3.zero;
		obj.transform.GetComponent<RectTransform> ().anchoredPosition3D = Vector3.zero;
		obj.transform.GetComponent<RectTransform> ().sizeDelta = Vector3.zero;
		obj.transform.localScale = Vector3.one;
		
		obj.transform.GetChild (0).GetComponent<Text> ().text = text;
		Destroy (obj, time);
	}

	public void displayArrow(bool left, bool right, bool up, bool down, float time)
	{
		if (arrowInstLeft != null)
			Destroy (arrowInstLeft);
		if (arrowInstRight != null)
			Destroy (arrowInstRight);

		instanceArrow (ref arrowInstLeft, panelLeft, left, right, up, down, time);
		instanceArrow (ref arrowInstRight, panelRight, left, right, up, down, time);
	}

	void instanceArrow(ref GameObject obj, Transform parent, bool left, bool right, bool up, bool down, float time)
	{
		obj = Instantiate (arrowPrefab, arrowPrefab.transform.position, arrowPrefab.transform.rotation) as GameObject;
		obj.transform.SetParent(parent);
		obj.transform.localPosition = Vector3.zero;
		obj.transform.GetComponent<RectTransform> ().anchoredPosition3D = Vector3.zero;
		obj.transform.GetComponent<RectTransform> ().sizeDelta = Vector3.zero;
		obj.transform.localScale = Vector3.one;
		
		UIArrow arrow = obj.GetComponent<UIArrow> ();
		arrow.left.SetActive (left);
		arrow.up.SetActive (up);
		arrow.down.SetActive (down);
		arrow.right.SetActive (right);

		Destroy (obj, time);
	}

	public void updatePoints(int value)
	{
		numberPoint += value;
		pointLeft.text = "POINTS : " + numberPoint.ToString ("### ### ##0").Trim (' ');
		pointRight.text = "POINTS : " + numberPoint.ToString ("### ### ##0").Trim (' ');
	}

	public void animPoints()
	{
		pointLeft.GetComponent<Animator> ().SetTrigger ("anim");
		pointRight.GetComponent<Animator> ().SetTrigger ("anim");
	}

	public void unanimPoints()
	{
		pointLeft.GetComponent<Animator> ().SetTrigger ("exit");
		pointRight.GetComponent<Animator> ().SetTrigger ("exit");
	}

}
