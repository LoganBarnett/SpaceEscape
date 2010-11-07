using UnityEngine;
using System.Collections;

public class SynchronizedRotator : MonoBehaviour {
	public GameObject parent;
	
	void Update () {
		transform.LookAt(parent.transform.position);
	}
}
