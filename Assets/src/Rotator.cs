using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {
	public float rotationSpeedX = 0.0f;
	public float rotationSpeedY = 1.0f;
	public float rotationSpeedZ = 0.0f;
	
	void Update () {
		transform.Rotate(rotationSpeedX * Time.deltaTime, rotationSpeedY * Time.deltaTime, rotationSpeedZ * Time.deltaTime);
	}
}
