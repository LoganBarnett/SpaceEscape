using UnityEngine;
using System.Collections;

namespace TweenySupport
{
	public abstract class TweenyBehavior : MonoBehaviour
	{
		
		/// <summary>
	    /// This property will inform you if the tween is currently animating. This will be true after the delay and false before. 
	    /// </summary>
	    public bool inProgress;
		bool isKinematic;
		protected bool stopTween;
		protected TweenyArgs tweenyArgs;
//		private delegate float easingFunction(float start, float end, float value);
		
		public IEnumerator Start()
	    {
//	        if (!inProgress && args.includeChildren)
//	        {
//	            foreach (Transform child in _transform)
//	            {
//	                //Note, if Arguments was a class, this would not work. Since it is a struct, I get a copy.
//	                Arguments newArgs = args;
//	                newArgs.onComplete = null;
//	                newArgs.onStart = null;
//	                newArgs.onUpdate = null;
//	                init(child.gameObject, newArgs);
//	            }
//	        }
	
	        //delay:
	        if (tweenyArgs.Delay > 0)
	        {
	            yield return new WaitForSeconds(tweenyArgs.Delay);
	        }
	
	        //don't destroy conflicts if we are not running yet.
	        CheckForConflicts();
	        inProgress = true;
	
//	        DoStartCallback();
			
			TweenyStart();
//	        switch (args.type)
//	        {
//	            case FunctionType.fade:
//	                if (gameObject.GetComponent(typeof(Renderer)))
//	                {
//	                    StartCoroutine(tweenFade());
//	                }
//	                break;
//	
//	            case FunctionType.move:
//	                enableKinematic();
//	                //bezier is different enough to warrant it's own function here.
//	                if (args.isBezier)
//	                {
//	                    StartCoroutine(tweenMoveBezier());
//	                }
//	                else
//	                {
//	                    StartCoroutine(tweenMove());
//	                }
//	                break;
//	
//	            case FunctionType.scale:
//	                enableKinematic();
//	                StartCoroutine(tweenScale());
//	                break;
//	
//	            case FunctionType.rotate:
//	                enableKinematic();
//	                StartCoroutine(tweenRotate());
//	                break;
//	
//	            case FunctionType.color:
//	                StartCoroutine(tweenColor());
//	                break;
//	
//	            case FunctionType.punchPosition:
//	                enableKinematic();
//	                StartCoroutine(tweenPunchPosition());
//	                break;
//	
//	            case FunctionType.punchRotation:
//	                enableKinematic();
//	                StartCoroutine(tweenPunchRotation());
//	                break;
//	
//	            case FunctionType.shake:
//	                enableKinematic();
//	                StartCoroutine(tweenShake());
//	                break;
//	
//	            case FunctionType.audio:
//	                StartCoroutine(tweenAudio());
//	                break;
//	
//	            case FunctionType.stab:
//	                StartCoroutine(tweenStab());
//	                break;
//	            case FunctionType.look:
//	                tweenLook();
//	                break;
//	        }
	
	        yield return -1;
	    }
		
		protected abstract void TweenyStart();
		
		public void StopTween()
	    {
	        stopTween = true;
	    }
		
		//Check for and remove running tweens of same type:
	    private void CheckForConflicts()
	    {
	        var scripts = GetComponents(GetType());
	        if (scripts.Length > 1)
	        {
	            //this is okay to cast as isharpTween because we are only getting that type
	            foreach (TweenyBehavior tween in scripts)
	            {
					// && IsTweenSameType(tween.args.type, args.type)
	                if ((tween != this) && tween.inProgress && GetType() == tween.GetType())
	                {
	                    Destroy(tween);
	                }
	            }
	        }
	    }
		
		 //I know that this function does not need to be a seperate function, but it is possible
	    //that in the future we might want to have 2 different enums be considered equivalent
//	    private static bool IsTweenSameType(FunctionType functionType, FunctionType type)
//	    {
//	        return (functionType == type);
//	    }
		
		public void OnDisable()
	    {
	        //kinematic restoration
	        if (isKinematic)
	        {
	            rigidbody.isKinematic = false;
	        }
	    }
		
	    protected void EnableKinematic()
	    {
	        if (transform.gameObject.GetComponent(typeof(Rigidbody)))
	        {
	            if (!rigidbody.isKinematic)
	            {
	                rigidbody.isKinematic = true;
	                isKinematic = true;
	            }
	        }
	    }
		
//		private easingFunction getEasingFunction(EasingType easingType)
//	    {
//			var function = Easing.GetFunction(easingType);
//			return new easingFunction(function);
//	    }
	}
}