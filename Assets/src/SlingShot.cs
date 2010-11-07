using System.Collections;
using UnityEngine;

public class SlingShot : MonoBehaviour {
	public GameObject earth;
	/// <summary>
	/// How close for boost to kick in
	/// </summary>
	public float boostDistanceThreshold;
	/// <summary>
	/// Ratio of distance vs. boost
	/// </summary>
	public float boostToDistanceRatio;
	
	/// <summary>
	/// How far away to calculate ratio with (use to account for earth's radius)
	/// </summary>
	public float distanceOffset;
	
	/// <summary>
	/// Ratio at which the screen stretches when boosting.
	/// </summary>
	public float boostToFieldOfViewRatio = 0.5f;
	
	public float Boost { get; set; }
	
	public GameObject gravityDrive;
	
	AudioSource gravityDriveAudio;
	
	void Awake() {
		enabled = false;
		
		Boost = 1.0f;
		gravityDriveAudio = gravityDrive.audio;
	}
	
	void FixedUpdate() {
		
//		Camera.main.fieldOfView = 60;
		
		var distance = Vector3.Distance(transform.position, earth.transform.position);
		if (distance > boostDistanceThreshold) return;
		
		Boost = Mathf.Max(Boost, Mathf.Floor(Mathf.Abs((distance - distanceOffset) * boostToDistanceRatio)));
		Debug.Log(string.Format("Boosting {0}", Boost));
		// TODO: Visually indicate boost
		
		Camera.main.fieldOfView = 60 + (Boost * boostToFieldOfViewRatio);
		
		gravityDriveAudio.enabled = true;
		gravityDriveAudio.pitch = Boost / 45.0f;
	}
}
