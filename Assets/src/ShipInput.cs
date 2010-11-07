using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ShipInput : MonoBehaviour {
	public float turnRate = 1.0f;
	
	void Start() {
		GetComponent<Thrusters>().enabled = true;	
	}
	
	void Awake() {
		enabled = false;
	}
	
	void FixedUpdate()
	{
		var h = Input.GetAxis("Horizontal") * turnRate * Time.fixedDeltaTime;
		var v = Input.GetAxis("Vertical") * turnRate * Time.fixedDeltaTime;
		
		var torque = transform.TransformDirection(v, h, 0.0f);
		rigidbody.AddTorque(torque);
		
		if (Input.GetButton("Thrust"))
		{
			GetComponent<Thrusters>().Thrust();
		}
	}
}
