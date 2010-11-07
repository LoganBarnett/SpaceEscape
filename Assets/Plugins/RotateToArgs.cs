using TweenySupport;
using UnityEngine;

public class RotateToArgs : TweenyArgs
{
	public GameObject Target { get; set; }
	public Vector3 Destination { get; set; }
	public bool Reverse { get; set; }
}
