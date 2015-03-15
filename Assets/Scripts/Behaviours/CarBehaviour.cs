using UnityEngine;
using System.Collections;

public class CarBehaviour : MonoBehaviour {

	public Transform dest;
	public Transform limit;
	NavMeshAgent agent;

	public GameManager.carType type;

	public bool pulled = false;
	bool goToStop = false;
	Transform targetStop;
	bool soundPlayed = false;

	public AudioClip soundFrein;

	private Collider colliderCar;

	void Awake(){
		agent = GetComponent<NavMeshAgent> ();
		colliderCar = GetComponent<Collider> ();
	}

	// Use this for initialization
	void Start () {
		if (dest != null) {
			agent.SetDestination (dest.position);
			if(dest.name.Contains("Target1")) colliderCar.enabled = false;
		}
		GameObject player = GameObject.Find ("CardboardMain");
		if (player != null) {
			limit = player.transform;
		}
	}

	public void moveToDest(){
		agent.SetDestination (dest.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (!pulled && agent.remainingDistance>0 && (agent.remainingDistance <= agent.stoppingDistance))
			Destroy (gameObject);

		if (!pulled && limit != null && transform.position.x > limit.position.x && colliderCar.enabled) {
			colliderCar.enabled = false;
			if(Random.Range(0f, 100f) < 50f)
			{
				GetComponent<AudioSource>().PlayOneShot(GameManager.instance.klaxons[Random.Range(0, GameManager.instance.klaxons.Count)]);
			}
		}

		if (goToStop && !soundPlayed && Vector3.Distance (targetStop.position, transform.position) < 4f) {
			soundPlayed = true;
			GetComponent<AudioSource>().PlayOneShot(soundFrein);
			transform.FindChild("StopObject").gameObject.SetActive(true);
			Invoke ("leaveScene", 1.3f);
		}
			
	}

	public void onLoadcastComplete(){
		if (!goToStop && (VacheSpawner.instance==null || !VacheSpawner.instance.closetoplayer)) {
			pulled = true;
			targetStop = GameObject.Find("carStop").transform;	
			agent.SetDestination (targetStop.position); 
			CardboxRaycaster.instance.off = true;
			goToStop = true;
		}


	}

	public void leaveScene(){
		GameManager.instance.gotoCar(type);
	}
}
