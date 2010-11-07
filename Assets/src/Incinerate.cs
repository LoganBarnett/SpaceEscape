using UnityEngine;
using System.Collections;

public class Incinerate : MonoBehaviour {
	public AudioClip reentrySound;
	public float volume;
	
	IEnumerator OnTriggerEnter(Collider other) {
		if (!other.CompareTag("incinerator")) yield break;
		// play explode sound
		AudioSource.PlayClipAtPoint(reentrySound, gameObject.transform.position, volume);
		
		yield return new WaitForSeconds(3);
		
		Application.LoadLevel("Earth Orbit");
	}
}