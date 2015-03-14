using UnityEngine;
using System.Collections;

public class VacheSpawner : MonoBehaviour {

	public GameObject vache;

	public bool closetoplayer;

	public VacheBehaviour currentVache;

	public Transform player;

	void Start()
	{
		putVache ();
	}

	public void putVache()
	{
		if (currentVache == null) {
			GameObject vacheInst = Instantiate (vache, transform.position, transform.rotation) as GameObject;
			
			VacheBehaviour behaviour = vacheInst.GetComponent<VacheBehaviour> ();
			behaviour.player = player;
			behaviour.spawner = this;
		}
	}
}
