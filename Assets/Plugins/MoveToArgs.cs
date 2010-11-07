using UnityEngine;
using System.Linq;
using TweenySupport;

//GameObject target,
//float? time,
//float? delay,
//Vector3 vector,
//EasingType? transition,
//LoopType loopType,
////Vector3? lookAt,
////AxisType? lookAtAxis,
////float? lookSpeed,
//string onComplete,
//object oncomplete_params,
//GameObject oncomplete_target,
//string onStart,
//object onStart_params,
//GameObject onStart_target,
//string onUpdate,
//object onUpdate_params,
//GameObject onUpdate_target

public class MoveToArgs : TweenyArgs
{

	public Vector3 Destination { get; set; }
	public GameObject Target { get; set; }
	public delegate void OnComplete();
	public delegate void OnUpdate();
}