using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TweenySupport;

//namespace TweenySupport
//{
	public class MoveTo : TweenyBehavior
	{
		private MoveToArgs moveToArgs;
		public MoveToArgs Args 
		{
			get { return moveToArgs; }
			set { tweenyArgs = moveToArgs = value; }
		}
	    
		protected override void TweenyStart()
		{
			EnableKinematic();
			StartCoroutine(TweenMove());
		}

	    private IEnumerator TweenMove()
	    {
	        Vector3 end;
	        Vector3 startingPosition;
	
//	        //define targets:
//	        if (args.isBy)
//	        {
//	            end = new Vector3(args.xr,args.yg, args.zb);            
//	            startingPosition = Vector3.zero;
//	        }
//	        else
	        {
	            //can not do this is we are movingBy, because I need the non-moving axis to be 0.
//	            if (args.isWorld)
//	            {
//	                DefaultArgsToStartingPosition(_transform.position);
//	            }
//	            else
//	            {
//	                DefaultArgsToStartingPosition(transform.localPosition);
//	            }
//	            if (args.isReverse)
//	            {
//	                
//	                setMoveFrom();
//	                if (args.transform != null)
//	                {
//	                    iTween.lookTo(_transform.gameObject, 0, 0, args.transform.position);
//	                }
//	                else if (args.lookAt.HasValue)
//	                {
//	                    iTween.lookTo(_transform.gameObject, 0, 0, args.lookAt.Value);
//	                }
//	            }
	
				end = Args.Destination;
//	            startingPosition = args.isWorld ? _transform.position : _transform.localPosition;            
				startingPosition = transform.localPosition;
	        }
	
	        //run tween
	        //don't force a divide by zero, just set it to the end value
	        Vector3 lastValues = startingPosition;
	        var easing = Easing.MakeEasing(Args.EasingType);
	        if (Args.Duration > 0)
	        {
	            float currentTime = 0.0f;            
	            while ((currentTime < 1) && (!stopTween))
	            {
	                //when we do a by or an add, we want to be able to do more than one independently, so we will do a translate instead of setting the position.                
	                Vector3 newVector;
	                newVector.x = easing.Ease(startingPosition.x, end.x, currentTime);
	                newVector.y = easing.Ease(startingPosition.y, end.y, currentTime);
	                newVector.z = easing.Ease(startingPosition.z, end.z, currentTime);
	
//	                if (args.isBy)
//	                {
//	                    /*Debug.Log("S");
//	                    printVector("New", newVector);
//	                    printVector("Last", lastValues);*/
//	                    printVector("t", newVector - lastValues);
//	                    transform.Translate(newVector - lastValues, args.space);
//	                }
//	                else
//	                {
//	                    if (args.isWorld)
//	                    {
//	                        transform.position = newVector;
//	                    }
//	                    else
//	                    {
//	                        transform.localPosition = newVector;
//	                    }
						transform.localPosition = newVector;
//	                }
	                
	                lastValues = newVector;
	
//	                if (args.transform != null)
//	                {
//	                    iTween.lookToUpdate(_transform.gameObject, args.transform.position, args.lookSpeed, args.axis);
//	                }
//	                else if (args.lookAt.HasValue)
//	                {
//	                    iTween.lookToUpdate(_transform.gameObject, args.lookAt.Value, args.lookSpeed, args.axis);
//	                }
	
//	                DoUpdateCallback();
	                yield return 0;
	                currentTime += Time.deltaTime / Args.Duration;
	            }
	        }
	
	        if (!stopTween)
	        {
	            //make sure we end up where we're supposed to.
//	            if (args.isBy)
//	            {
//	                Vector3 newVector;
//	                newVector.x = easing(startingPosition.x, end.x, 1);
//	                newVector.y = easing(startingPosition.y, end.y, 1);
//	                newVector.z = easing(startingPosition.z, end.z, 1);
//	
//	                transform.Translate(newVector - lastValues, args.space);
//	            }
//	            else
	            {
//	                if (args.isWorld)
//	                {
//	                    _transform.position = end;
//	                }
//	                else
//	                {
//	                    _transform.localPosition = end;
//	                }
					transform.localPosition = end;
	            }
	
//	            DoCallback();
	
	            //Loop?
	            if (Args.Duration > 0)
	            {
	                switch (Args.LoopType)
	                {
	                    case LoopType.None:
	                        Destroy(this);
	                        break;
	                    case LoopType.Loop:
	                        //go back to the beginning
//	                        if (args.isWorld)
//	                        {
//	                            _transform.position = startingPosition;
//	                        }
//	                        else
//	                        {
//	                            _transform.localPosition = startingPosition;
//	                        }
							transform.localPosition = startingPosition;
	                        StartCoroutine(Start());
	                        break;
	                    case LoopType.PingPong:
//	                        args.xr = startingPosition.x;
//	                        args.yg = startingPosition.y;
//	                        args.zb = startingPosition.z;
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
	            //cleanup
	            Destroy(this);
	        }
	    }

	}
//}