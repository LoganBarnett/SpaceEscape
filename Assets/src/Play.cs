using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {
	public GameObject earth;
	public GameObject ship;
	public GameObject playCameraNode;
	
	
	void Start () {
		earth.GetComponent<GravityWell>().enabled = true;
		ship.GetComponent<ShipInput>().enabled = true;
		ship.GetComponent<SlingShot>().enabled = true;
		ship.GetComponent<AlignedCompassRings>().enabled = true;
//		Camera.main.GetComponent<SmoothFollow>().target = ship.transform;
//		Camera.main.transform.parent = ship.transform;
		Camera.main.transform.parent = playCameraNode.transform;
	}
	
	void OnGUI() {
		GUILayout.Label(string.Format("Speed: {0} kph", ship.rigidbody.velocity.magnitude * 1000.0f * 400.0f / 60.0f));
	}
}
