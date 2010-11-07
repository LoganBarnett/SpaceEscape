using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class GravityWell : MonoBehaviour {
	public float gravity = 10.0f;
	public GameObject ship;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		var distance = Vector3.Distance(ship.transform.position, transform.position);
		var force = ship.rigidbody.mass * rigidbody.mass / Mathf.Pow(distance, 2);
		var facingVector = (transform.position - ship.transform.position).normalized;
		
		ship.rigidbody.AddForce(facingVector * force * Time.fixedDeltaTime * gravity, ForceMode.Impulse);
	}
}
