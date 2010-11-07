using TweenySupport;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class RotateTo : TweenyBehavior
{
	private RotateToArgs rotateToArgs;
	public RotateToArgs Args 
	{
		get { return rotateToArgs; }
		set { tweenyArgs = rotateToArgs = value; }
	}
	
	protected override void TweenyStart()
	{
		EnableKinematic();
		StartCoroutine(TweenRotate());
	}
	
	private IEnumerator TweenRotate()
    {
        Vector3 end;
//        Vector3 e = args.isWorld ? transform.eulerAngles : transform.localEulerAngles;
		Vector3 e = transform.localEulerAngles;
        
//        if (args.isBy)
//        {
//            if (args.isMultiply)
//            {
//                end = new Vector3(360.0f * Args.Destination.x, 360.0f * Args.Destination.y, 360.0f * Args.Destination.z);
//            }
//            else
//            {
//                end = new Vector3(Args.Destination.x, Args.Destination.y, Args.Destination.z);
//            }
//        }
//        else
        {
//            DefaultArgsToStartingPosition(e);

            if (Args.Reverse)
            {
//				if (args.isWorld)
//		        {
//		            Args.Target.transform.eulerAngles = ReverseDirection(Args.Target.transform.eulerAngles);
//		        }
//		        else
//		        {
//		            _transform.localEulerAngles = ReverseDirection(_transform.localEulerAngles);            
//		        }
            }
			
  			var clerp = new Clerp();
			end = new Vector3(clerp.Ease(e.x, Args.Destination.x, 1.0f), clerp.Ease(e.y, Args.Destination.y, 1.0f), clerp.Ease(e.z, Args.Destination.z, 1.0f));
        }
        
        
        //cannot jus assign to e because rotatefrom modifies what the start is.
//        Vector3 start = args.isWorld ? _transform.eulerAngles : _transform.localEulerAngles;
		Vector3 start = transform.localEulerAngles;

        Quaternion starte = Quaternion.Euler(start);
        Quaternion ende = Quaternion.Euler(end);
        
        //don't force a divide by zero, just set it to the end value
        var easing = Easing.MakeEasing(Args.EasingType);
		
        Vector3 oldRotation = Vector3.zero;
        if (Args.Duration > 0.0f)
        {
            //define object:                
            float currentTime = 0.0f;
                        
            while ((currentTime < 1.0f) && (!stopTween))
            {
                
//                if (args.isBy)
//                {
//                    Vector3 newRotation = new Vector3(easing.Ease(0, end.x, currentTime), easing.Ease(0, end.y, currentTime), easing.Ease(0, end.z, currentTime));
//
//                    transform.Rotate(newRotation - oldRotation, args.space);
//                    oldRotation = newRotation;
//
//                }
//                else
//                {
                    var movetime = easing.Ease(0.0f, 1.0f, currentTime);

//                    if (args.isWorld)
//                    {
//                        transform.rotation = Quaternion.Slerp(starte, ende, movetime);
//                    }
//                    else
                    {
                        transform.localRotation = Quaternion.Slerp(starte, ende, movetime);
                    }
//                }

//                DoUpdateCallback();
                yield return 0;
                currentTime += Time.deltaTime / Args.Duration;
            }
        }

        if (!stopTween)
        {
//            if (args.isBy)
//            {
//                Vector3 newVector;
//                newVector.x = easing(0, end.x, 1);
//                newVector.y = easing(0, end.y, 1);
//                newVector.z = easing(0, end.z, 1);
//
//                transform.Rotate(newVector - oldRotation, args.space);
//            }
//            else
//            {
                //make sure we end up where we're supposed to.
//                if (args.isWorld)
//                {
//                    transform.rotation = Quaternion.Euler(end);
//                }
//                else
                {
                    transform.localRotation = Quaternion.Euler(end);
                }
//            }
//            DoCallback();

            //Loop?
            if (Args.Duration > 0.0f)
            {
                switch (Args.LoopType)
                {
                    case LoopType.None:
                        Destroy(this);
                        break;
                    case LoopType.Loop:
                        //go back to the beginning                       
                        transform.localRotation = starte;

                        StartCoroutine(Start());
                        break;
                    case LoopType.PingPong:
//                        args.xr = start.x;
//                        args.yg = start.y;
//                        args.zb = start.z;
//
//                        args.isXRSet = true;
//                        args.isYGSet = true;
//                        args.isZBSet = true;
                        StartCoroutine(Start());
                        break;
                    default:
                        Destroy(this);
                        break;
                }
            }
            else
            {
                Destroy(this);
            }
        }
        else
        {
            Destroy(this);
        }
    }
}
