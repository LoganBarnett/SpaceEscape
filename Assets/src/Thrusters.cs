using UnityEngine;

public class Thrusters : MonoBehaviour {
	public float thrustRate = 10.0f;
	public GameObject[] thrusterEmitters;
	public GameObject[] thrusterLights;
	public GameObject thrusterSound;
	
	bool hasThrustedThisUpdate = false;
	SlingShot slingShot;
	
	void Start() {
		slingShot = GetComponent<SlingShot>();
	}
	
	void Awake() {
		foreach (var emitter in thrusterEmitters)
		{
			emitter.particleEmitter.emit = false;	
		}
		
		foreach (var light in thrusterLights)
		{
			light.light.intensity = 0.0f;
		}
		thrusterSound.audio.volume = 0.0f;
	}
	
	public void Thrust() {
		hasThrustedThisUpdate = true;
	}
	
	void FixedUpdate() {
		var thrust = hasThrustedThisUpdate ? Input.GetAxis("Thrust") : 0.0f;
		
		thrusterSound.audio.volume = thrust * 0.25f;
		
		foreach (var light in thrusterLights)
		{
			light.light.intensity = thrust;
		}
		
		foreach (var emitter in thrusterEmitters)
		{
			emitter.particleEmitter.emit = hasThrustedThisUpdate;
		}
		
		rigidbody.AddForce(transform.forward * thrustRate * thrust * slingShot.Boost * Time.fixedDeltaTime);
		
		hasThrustedThisUpdate = false;
	}
}
