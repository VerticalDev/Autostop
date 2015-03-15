using UnityEngine;
using System.Collections;

public class VacheBehaviour : MonoBehaviour {

	float lastTimeStuck = 0f;
	bool stuck = false;
	bool goback = false;
	bool tooclose = false;

	Animation anim;

	NavMeshAgent agent;

	AudioSource asource;
	public AudioClip meuh;

	public Transform player;
	public VacheSpawner spawner;

	public float minDistance;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animation> ();
		asource = GetComponent<AudioSource> ();
		agent.SetDestination (player.position);
		anim["Jump"].wrapMode = WrapMode.Loop;
		anim.Play("Jump");
	}
	
	// Update is called once per frame
	void Update () {
		if (!goback && stuck && Time.time > lastTimeStuck + 0.1f) {
			agent.Resume();
			anim["Jump"].wrapMode = WrapMode.Loop;
			anim.Play("Jump");
			stuck = false;
		}

		if (!goback && Vector3.Distance(player.position, transform.position) <= minDistance) {
			if(!tooclose)
			{
				anim["Jump"].wrapMode = WrapMode.Once;
				anim.PlayQueued("Jump");
				UIManager.instance.displayDialog("PERSONNE NE VOUS PRENDRA TROP PRES D'UNE VACHE !", 7f);
			}
			tooclose = true;
			spawner.closetoplayer = true;
		}

		if (goback && Vector3.Distance (transform.position, spawner.transform.position) <= minDistance) {
			Destroy (this.gameObject);
		}
	}

	public void onRaycastLoad(RaycastReceiver rr)
	{
		if (!stuck) {
			agent.Stop ();
			anim["Jump"].wrapMode = WrapMode.Once;
			anim.PlayQueued("Jump");
		}
		stuck = true;
		lastTimeStuck = Time.time;
	}

	public void onLoadcastComplete(RaycastReceiver rr)
	{
		UIManager.instance.destroyDialog ();
		agent.Resume();
		agent.SetDestination (spawner.transform.position);
		anim["Jump"].wrapMode = WrapMode.Loop;
		anim.Play("Jump");
		goback = true;
		asource.PlayOneShot(meuh);
		spawner.closetoplayer = false;
	}
}
