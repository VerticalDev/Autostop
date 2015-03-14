using UnityEngine;
using System.Collections;

public class VacheSpawner : MonoBehaviour {

	public GameObject vache;

	public bool closetoplayer;

	public VacheBehaviour currentVache;

	public Transform player;

	public void putVache()
	{
		if (currentVache == null) {
			GameObject vacheInst = Instantiate (vache, transform.position, transform.rotation) as GameObject;
			
			currentVache = vacheInst.GetComponent<VacheBehaviour> ();
			currentVache.player = player;
			currentVache.spawner = this;
		}
	}

	public void deputVache()
	{
		if (currentVache != null) {
			currentVache.GetComponent<Collider> ().enabled = false;
			currentVache.onLoadcastComplete (null);
		}

	}
}
