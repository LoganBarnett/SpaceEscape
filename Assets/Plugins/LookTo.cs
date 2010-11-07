using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TweenySupport;

//namespace TweenySupport
//{
	public class LookTo : TweenyBehavior
	{
		private LookToArgs lookToArgs;
		public LookToArgs Args 
		{
			get { return lookToArgs; }
			set { tweenyArgs = lookToArgs = value; }
		}
	    
		protected override void TweenyStart()
		{
			EnableKinematic();
//			StartCoroutine(TweenLook());
			TweenLook();
		}
	
		private void TweenLook()
	    {
	        //look point:
	        Vector3 lookPoint = Args.Destination;
//	        if (args.transform == null)
//	        {
//	            lookPoint = args.Target.Value;
//	        }
//	        else
//	        {
//	            lookPoint = args.transform.position;
//	        }
	
	        //startRotation:	
//	        Vector3 startRotation = args.isWorld ? _transform.eulerAngles : _transform.localEulerAngles;
			Vector3 startRotation = Args.IsWorld ? transform.eulerAngles : transform.localEulerAngles;
	        //endRotation:
			Args.Target.transform.LookAt(lookPoint);
//	        _transform.LookAt(lookPoint);
	
//	        Vector3 endRotation = args.isWorld ? _transform.eulerAngles : _transform.localEulerAngles;
			Vector3 endRotation = Args.IsWorld ? transform.eulerAngles : transform.localEulerAngles;
			
//	        if (args.isWorld)
//	        {
	            Args.Target.transform.localEulerAngles = startRotation;
//	        }
//	        else
//	        {
//	            Args.Target.transform.localEulerAngles = startRotation;
//	        }
	
	
	        //Brute force axis back to previous value if user wants single axis usage:
//	        if ((args.axis & AxisType.x) == 0)
//	        {
//	            endRotation.x = startRotation.x;
//	        }
//	        if ((args.axis & AxisType.y) == 0)
//	        {
//	            endRotation.y = startRotation.y;
//	        }
//	        if ((args.axis & AxisType.z) == 0)
//	        {
//	            endRotation.z = startRotation.z;
//	        }
	
	        //copy the args and reinit
//	        Arguments newArgs = args;
//	        newArgs.delay = 0;
//	        newArgs.type = FunctionType.rotate;
//	        newArgs.xr = endRotation.x;
//	        newArgs.yg = endRotation.y;
//	        newArgs.zb = endRotation.z;
//	        newArgs.isXRSet = true;
//	        newArgs.isYGSet = true;
//	        newArgs.isZBSet = true;
//	
//	        init(_transform.gameObject, newArgs);
			
			Tweeny.RotateTo(new RotateToArgs {
				Destination = endRotation,	
				Target = Args.Target,
				Reverse = Args.Reverse,
				IsWorld = Args.IsWorld,
				Duration = Args.Duration
				// TODO: Add other args
			});
	        Destroy(this);
	    }

	   

	}
//}