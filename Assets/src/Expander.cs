using UnityEngine;
using System.Collections;

public class Expander : MonoBehaviour {
	public float expansionRate = 1.0f;
	const float radialExpansionRateToSpeedInKph = 41730000.0f;
	
	public float RadialExpasionSpeedInKph
	{
		get { return radialExpansionRateToSpeedInKph * expansionRate; }
	}
	
	void Update() {
		var rate = expansionRate * Time.deltaTime;
		var scale = transform.localScale;
		transform.localScale = new Vector3(rate + scale.x, rate + scale.y, rate + scale.z);
		if (transform.localScale.x > 1.9f && transform.localScale.x < 2.1f) Debug.Log(string.Format("Scale: {0} Time: {0} seconds", transform.localScale.x, Time.timeSinceLevelLoad));
	}
}