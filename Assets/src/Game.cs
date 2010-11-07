using System;
using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public GameObject play;
	
	IEnumerator Start() {
		yield return new WaitForSeconds(8.0f);
		
		play.active = true;
	}
}
