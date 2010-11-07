using UnityEngine;
using System.Collections;

public class AlignedCompassRings : MonoBehaviour {
	public GameObject yRing;
	public GameObject north;
	public float zOffset = 0.0f;
	
	void Awake() {
		yRing.SetActiveRecursively(false);	
	}
	
	void Start() {
		yRing.SetActiveRecursively(true);
	}
	
	void Update() {
		yRing.transform.LookAt(north.transform.position);
		var localRotation = yRing.transform.localRotation.eulerAngles;
		yRing.transform.localRotation = Quaternion.Euler(localRotation.x, localRotation.y, zOffset);
	}
}
