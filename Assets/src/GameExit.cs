using System.Collections;
using UnityEngine;

public class GameExit : MonoBehaviour {
	public GameObject ship;
	public GameObject play;
	public GameObject sun;
	public GameObject shipCameraNode;
	public GameObject earth;
	
	bool victory;
	bool showSpeed;
	bool showSpeedFromSun;
	bool showSunSpeed;
	bool showRelativeSpeed;
	bool showRestart;
	
	float speed;
	float speedFromSun;
	float sunSpeed;
	float relativeSpeed;
	
	void OnTriggerEnter(Collider c) {
		victory = victory || c.gameObject.CompareTag("Player");
//		victory = true;
	}
	
	void Update() {
		if (!victory) return;
		StartCoroutine(ShowVictory());
	}
	
	IEnumerator ShowVictory() {
		victory = false;
		play.active = false;
		// Deactivate user input
		ship.GetComponent<ShipInput>().enabled = false;
		speed = ship.rigidbody.velocity.magnitude;
		speedFromSun = -ship.rigidbody.velocity.x;
		sunSpeed = sun.GetComponent<Expander>().RadialExpasionSpeedInKph;
		relativeSpeed = speedFromSun - sunSpeed;
		
		// look at Earth
		Tweeny.LookTo( new LookToArgs {
			Target = shipCameraNode,
			Destination = earth.transform.eulerAngles,
			Duration = 4.0f,
			IsWorld = false
		});
		
//		Tweeny.LookTo( new LookToArgs {
//			Target = Camera.main.gameObject,
//			Destination = earth.transform.eulerAngles,
//			Duration = 4.0f,
//		});
		
		yield return new WaitForSeconds(0.75f);		
		// Display maximum speed
		showSpeed = true;
		
		yield return new WaitForSeconds(0.75f);
		showSpeedFromSun = true;
		
		// Calculate speed from sun
		yield return new WaitForSeconds(0.75f);
		showSunSpeed = true;
		
		yield return new WaitForSeconds(0.75f);
		showRelativeSpeed = true;
		
		
		// Post to high score list
		// Return to menu
		yield return new WaitForSeconds(10.0f);
//		victory = false;
		showRestart = true;
	}
	
	void OnGUI() {
//		if (!victory) return;
//		GUILayout.Label("You Win!");
		GUILayout.BeginVertical();
		var spacerWidth = (Screen.height) / 2.0f;
		GUILayout.Space(spacerWidth);
		if (showSpeed) GUILayout.Label(string.Format("Speed: {0} kph", speed * 1000.0f * 400.0f / 60.0f));
		if (showSpeedFromSun) GUILayout.Label(string.Format("Speed from the nova: {0} kph", speedFromSun * 1000.0f * 400.0f / 60.0f));
		if (showSunSpeed) GUILayout.Label(string.Format("Speed of the nova: {0} kph", sunSpeed));
		if (showRelativeSpeed) GUILayout.Label(string.Format("Speed relative to the nova: {0} kph", relativeSpeed));
		if (showRestart && GUILayout.Button("Restart")) Application.LoadLevel("Earth Orbit");
		GUILayout.EndVertical();
	}
}
