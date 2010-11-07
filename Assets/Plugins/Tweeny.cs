//VERSION: 1.0.34

/*
 Copyright (c) 2010 Bob Berkebile(http://www.pixelplacement.com), C# port by Patrick Corkum(http://www.insquare.com)

 Permission is hereby granted, free of charge, to any person  obtaining a copy of this software and associated documentation  files (the "Software"), to deal in the Software without  restriction, including without limitation the rights to use,  copy, modify, merge, publish, distribute, sublicense, and/or sell  copies of the Software, and to permit persons to whom the  Software is furnished to do so, subject to the following conditions:
 The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

 THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
/* 
 
/*
TERMS OF USE - EASING EQUATIONS

Open source under the BSD License.

Copyright © 2001 Robert Penner
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

    * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
    * Neither the name of the author nor the names of contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TweenySupport;


    /// <summary>
    /// iTween is intended for creating simple animations on gameObjects within Unity.
    /// </summary>
public partial class Tweeny : MonoBehaviour
{


    /// <summary>
    /// The LoopType is used to cause certain types of tweening to loop without need for a callback to do it.
    /// </summary>
    public enum LoopType
    {
        /// <summary>
        /// Will not loop - default behavior
        /// </summary>
        none,
        /// <summary>
        /// Will animate from start -> end, start -> end, start -> end
        /// </summary>
        loop,
        /// <summary>
        /// Will animate from start -> end -> start -> end, etc...
        /// </summary>
        pingPong
    }

    /// <summary>
    /// The FunctionType is the type of tween that can occur
    /// </summary>
    public enum FunctionType
    {
        /// <summary>
        /// fade is any fade operation, (i.e. fadeFrom and fadeTo)
        /// </summary>
        fade,
        /// <summary>
        /// move is any move operation, (i.e. moveFrom, moveTo, moveBy and moveBezier)
        /// </summary>
        move,
        /// <summary>
        /// scale is any scale operation, (i.e. scaleFrom, scaleTo, and scaleBy)
        /// </summary>
        /// 
        scale,
        /// <summary>
        /// rotate is any rotate operation, (i.e. rotateFrom, rotateTo, and rotateBy)
        /// </summary>
        rotate,
        /// <summary>
        /// color is any operation that is changing the rgb color, (i.e. colorFrom and colorTo)
        /// </summary>
        color,
        /// <summary>
        /// audio is any operation that changes the audio's pitch and/or volume, (i.e. audioTo and audioFrom)
        /// </summary>
        audio,
        /// <summary>
        /// punchPosition is the punch Operation that punches the object via moving the coordinates of the object
        /// </summary>
        punchPosition,
        /// <summary>
        /// punchRotation is the punch Operation that punches the object via rotating the object.
        /// </summary>
        punchRotation,
        /// <summary>
        /// punchScale is the punch Operation that punches the object via scaling the object.
        /// </summary>
        punchScale,
        /// <summary>
        /// shake is the operation that will shake the object
        /// </summary>
        shake,
        /// <summary>
        /// stab is the operation that will play an audio clip.
        /// </summary>
        stab,
        /// <summary>
        /// look is the operation that will rotate an object to be oriented towards another.
        /// </summary>
        look
    }

    /// <summary>
    /// Defining which axis to take action on
    /// </summary>
    public enum AxisType
    {
        /// <summary>
        /// x-axis
        /// </summary>
        x = 1,
        /// <summary>
        /// y-axis
        /// </summary>
        y = 2,
       /* /// <summary>
        /// both x and y axis
        /// </summary>
        xy = 3,*/
        /// <summary>
        /// z axis
        /// </summary>
        z = 4,
   /*     /// <summary>
        /// x and z axis
        /// </summary>
        xz = 5,
        /// <summary>
        /// y and z axis
        /// </summary>
        yz = 6,
        /// <summary>
        /// All axis
        /// </summary>
        xyz = 7*/
    }

    //helper class
    private struct Arguments
    {
        public float time;
        public float delay;
        public EasingType transition;

        public bool isXRSet;
        public bool isYGSet;
        public bool isZBSet;
        public float xr;
        public float yg;
        public float zb;

        public float alpha;

        public float volume;
        public float pitch;
        public AudioClip clip;
        public AudioSource audioSource;

        public string onComplete;
        public object onComplete_params;
        public GameObject onComplete_target;

        public string onStart;
        public object onStart_params;
        public GameObject onStart_target;

        public string onUpdate;
        public object onUpdate_params;
        public GameObject onUpdate_target;

        public FunctionType type;
        public bool isReverse;
        public bool isBy;
        public bool isWorld;
        public bool isBezier;
        public bool isMultiply;
        public List<Vector3> beziers;
        public LoopType loopType;
        public Vector3? lookAt;
        public bool orientToPath;
        public bool includeChildren;

        public Transform transform;
        
        public Space space;
        public AxisType axis;
        public float lookSpeed;

        private Arguments(float? i_time, float i_defaultTime, float? i_delay, float i_defaultDelay, EasingType? i_transition, EasingType i_defaultTransition, 
            string i_onComplete, object i_onComplete_params, FunctionType i_type, bool i_reverse,
            GameObject i_onCompleteTarget, string i_onStart, object i_onStart_params, GameObject i_onStartTarget,string i_onUpdate, object i_onUpdate_params, GameObject i_onUpdateTarget)
        {
            time = i_time.HasValue ? i_time.Value : i_defaultTime;
            delay = i_delay.HasValue ? i_delay.Value : i_defaultDelay;
            transition = i_transition.HasValue ? i_transition.Value : i_defaultTransition;
            onComplete = i_onComplete;
            onComplete_params = i_onComplete_params;
            isReverse = i_reverse;
            type = i_type;
            clip = null;
            audioSource = null;
            pitch = 0;
            volume = 0;
            alpha = 0;
            xr = 0;
            yg = 0;
            zb = 0;
            isXRSet = false;
            isYGSet = false;
            isZBSet = false;
            isBy = false;
            isWorld = false;
            isBezier = false;
            isMultiply = false;
            beziers = null;
            onComplete_target = i_onCompleteTarget;
            loopType = LoopType.none;
            orientToPath = true;
            lookAt = null;
            includeChildren = true;
            space = Space.Self;

            onStart = i_onStart;
            onStart_params = i_onStart_params;
            onStart_target = i_onStartTarget;

            onUpdate = i_onUpdate;
            onUpdate_params = i_onUpdate_params;
            onUpdate_target = i_onUpdateTarget;

            transform = null;
            axis = AxisType.x | AxisType.y | AxisType.z;
            lookSpeed = 0;
        }
        public Arguments(float? i_time, float i_defaultTime, float? i_delay, float i_defaultDelay, EasingType? i_transition, EasingType i_defaultTransition, string i_onComplete, object i_onComplete_params, FunctionType i_type, bool i_reverse,
                        float? i_xr, float? i_yg, float? i_zb, bool i_isBy, bool i_isWorld, bool i_isMultiply, bool i_includeChildren, GameObject i_onCompleteTarget, LoopType i_loopType, Transform i_lookAtTransform, Vector3? i_lookAt, AxisType? i_axis, float? i_lookSpeed, float i_lookSpeedDefault,
            string i_onStart, object i_onStart_params, GameObject i_onStartTarget, string i_onUpdate, object i_onUpdate_params, GameObject i_onUpdateTarget)
            : this(i_time, i_defaultTime, i_delay, i_defaultDelay, i_transition, i_defaultTransition, i_onComplete, i_onComplete_params, i_type, i_reverse, i_onCompleteTarget, i_onStart, i_onStart_params, i_onStartTarget, i_onUpdate, i_onUpdate_params, i_onUpdateTarget)
        {
            isXRSet = i_xr.HasValue;
            isYGSet = i_yg.HasValue;
            isZBSet = i_zb.HasValue;

            xr = isXRSet ? i_xr.Value : 0;
            yg = isYGSet ? i_yg.Value : 0;
            zb = isZBSet ? i_zb.Value : 0;
            isBy = i_isBy;
            isWorld = i_isWorld;
            space = isWorld ? Space.World : Space.Self;
            isMultiply = i_isMultiply;
            loopType = i_loopType;
            includeChildren = i_includeChildren;

            loopType = i_loopType;

            transform = i_lookAtTransform;
            lookAt = i_lookAt;            

            lookSpeed = i_lookSpeed.HasValue ? i_lookSpeed.Value : i_lookSpeedDefault;
            axis = (i_axis.HasValue) ? axis = i_axis.Value : AxisType.x | AxisType.y | AxisType.z;
        }

        public Arguments(float? i_time, float i_defaultTime, float? i_delay, float i_defaultDelay, EasingType? i_transition, EasingType i_defaultTransition, string i_onComplete, object i_onComplete_params, FunctionType i_type, bool i_reverse,
                       List<Vector3> i_beziers, bool i_isWorld, GameObject i_onCompleteTarget, LoopType i_loopType, bool i_orientToPath, Vector3? i_lookAt, Transform i_lookAtTransform, AxisType? i_axis, float? i_lookSpeed, float i_lookSpeedDefault, string i_onStart, object i_onStart_params, GameObject i_onStartTarget, string i_onUpdate, object i_onUpdate_params, GameObject i_onUpdateTarget)
            : this(i_time, i_defaultTime, i_delay, i_defaultDelay, i_transition, i_defaultTransition, i_onComplete, i_onComplete_params, i_type, i_reverse, i_onCompleteTarget, i_onStart, i_onStart_params, i_onStartTarget, i_onUpdate, i_onUpdate_params, i_onUpdateTarget)
        {
            isWorld = i_isWorld;
            space = isWorld ? Space.World : Space.Self;
            isBezier = true;
            beziers = i_beziers;

            orientToPath = i_orientToPath && (!i_lookAt.HasValue) && (i_lookAtTransform == null);
            transform = i_lookAtTransform;
            lookAt = i_lookAt;
            loopType = i_loopType;
            lookSpeed = i_lookSpeed.HasValue ? i_lookSpeed.Value : i_lookSpeedDefault;
            axis = (i_axis.HasValue) ? axis = i_axis.Value : AxisType.x | AxisType.y | AxisType.z;
        }

        public Arguments(float? i_time, float i_defaultTime, float? i_delay, float i_defaultDelay, EasingType? i_transition, EasingType i_defaultTransition, string i_onComplete, object i_onComplete_params, FunctionType i_type, bool i_reverse,
                       float? i_alpha, float i_defaultAlpha, bool i_includeChildren, GameObject i_onCompleteTarget, LoopType i_loopType, string i_onStart, object i_onStart_params, GameObject i_onStartTarget, string i_onUpdate, object i_onUpdate_params, GameObject i_onUpdateTarget)
            : this(i_time, i_defaultTime, i_delay, i_defaultDelay, i_transition, i_defaultTransition, i_onComplete, i_onComplete_params, i_type, i_reverse, i_onCompleteTarget, i_onStart, i_onStart_params, i_onStartTarget, i_onUpdate, i_onUpdate_params, i_onUpdateTarget)
        {
            alpha = i_alpha.HasValue ? i_alpha.Value : i_defaultAlpha;
            loopType = i_loopType;
            includeChildren = i_includeChildren;
        }

        public Arguments(float? i_time, float i_defaultTime, float? i_delay, float i_defaultDelay, EasingType? i_transition, EasingType i_defaultTransition, string i_onComplete, object i_onComplete_params, FunctionType i_type, bool i_reverse,
                       float? i_volume, float i_defaultVolume, float? i_pitch, float i_defaultPitch, AudioClip i_clip, AudioSource i_audioSource, GameObject i_onCompleteTarget, string i_onStart, object i_onStart_params, GameObject i_onStartTarget, string i_onUpdate, object i_onUpdate_params, GameObject i_onUpdateTarget)
            : this(i_time, i_defaultTime, i_delay, i_defaultDelay, i_transition, i_defaultTransition, i_onComplete, i_onComplete_params, i_type, i_reverse, i_onCompleteTarget, i_onStart, i_onStart_params, i_onStartTarget, i_onUpdate, i_onUpdate_params, i_onUpdateTarget)
        {
            volume = i_volume.HasValue ? i_volume.Value : i_defaultVolume;
            pitch = i_pitch.HasValue ? i_pitch.Value : i_defaultPitch;
            clip = i_clip;
            audioSource = i_audioSource;
        }
    }

    //help class for bezierCurves
    private struct BezierPointInfo
    {
        public Vector3 starting;
        public Vector3 intermediate;
        public Vector3 end;
    }

    //This allows us to shortcut looking up which easing function to use for each tween on each frame
    private delegate float easingFunction(float start, float end, float value);

    //Stab Defaults
    /// <summary>
    /// The Default Delay time for stab operations. This can be modified in code.
    /// </summary>
    public static float stabDefaultDelay = 0;
    /// <summary>
    /// The Default volume that stab operations will play at. This can be modified in code.
    /// </summary>
    public static float stabDefaultVolume = 1;
    /// <summary>
    /// The Default pitch that stab operations will play at. This can be modified in code.
    /// </summary>
    public static float stabDefaultPitch = 1;


    //Audio Defaults
    /// <summary>
    /// The Default time that it will take to transition the audio. This can be modified in code.
    /// </summary>
    public static float audioDefaultTime = 1;
    /// <summary>
    /// The Defaulttdelay time to transition the audio. This can be modified in code.
    /// </summary>
    public static float audioDefaultDelay = 0;
    /// <summary>
    /// The Default transition type for audio "animation". This can be modified in code.
    /// </summary>
    public static EasingType audioDefaultTransition = EasingType.Linear;
    /// <summary>
    /// The Default volume that the audio will transition to for audio operations. This can be modified in code.
    /// </summary>
    public static float audioDefaultVolume = 1;
    /// <summary>
    /// The Default pitch that will be used to transition the audio to for audio operations. This can be modified in code.
    /// </summary>
    public static float audioDefaultPitch = 1;



    //Shake Defaults
    /// <summary>
    /// The Default time to shake objects for shake operations. This can be modified in code.
    /// </summary>
    public static float shakeDefaultTime = 1;
    /// <summary>
    /// The Default delay time to wait to perform shake operations. This can be modified in code.
    /// </summary>
    public static float shakeDefaultDelay = 0;



    //Punch Rotation Defaults
    /// <summary>
    /// The Default time that the punch rotation will take. This can be modified in code.
    /// </summary>
    public static float punchRotationDefaultTime = 1;
    /// <summary>
    /// The Default delay time before punch rotations will occur. This can be modified in code.
    /// </summary>
    public static float punchRotationDefaultDelay = 0;


    //Punch Position Defaults
    /// <summary>
    /// The Default time that the punch position will take. This can be modified in code.
    /// </summary>
    public static float punchPositionDefaultTime = 1;
    /// <summary>
    /// The Default delay time before punch positions will occur. This can be modified in code.
    /// </summary>
    public static float punchPositionDefaultDelay = 0;

    //Punch Scale Defaults
    /// <summary>
    /// The Default time that the punch scale will take. This can be modified in code.
    /// </summary>
    public static float punchScaleDefaultTime = 1;
    /// <summary>
    /// The Default delay time before punch scaling will occur. This can be modified in code.
    /// </summary>
    public static float punchScaleDefaultDelay = 0;

    //Fade Defaults
    /// <summary>
    /// The Default time that the fade operations will take to fade the object. This can be modified in code.
    /// </summary>
    public static float fadeDefaultTime = 1;
    /// <summary>
    /// The Default delay time before fade operations will occur. This can be modified in code.
    /// </summary>
    public static float fadeDefaultDelay = 0;
    /// <summary>
    /// The Default transition type for fade "animation". This can be modified in code.
    /// </summary>
    public static EasingType fadeDefaultTransition = EasingType.Linear;



    //Move Defaults
    /// <summary>
    /// The Default time that the move operations will take to move the object. This can be modified in code.
    /// </summary>
    public static float moveDefaultTime = 1;
    /// <summary>
    /// The Default delay time before move operations will occur. This can be modified in code.
    /// </summary>
    public static float moveDefaultDelay = 0;
    /// <summary>
    /// The Default transition type for move animation. This can be modified in code.
    /// </summary>
    public static EasingType moveDefaultTransition = EasingType.EaseInOutCubic;
    /// <summary>
    /// The Default look Speed for move animation. This can be modified in code.
    /// </summary>
    public static float moveDefaultLookSpeed = 8;

    /// <summary>
    /// The Default time that the look operations will take to rotate the object. This can be modified in code.
    /// </summary>
    public static float lookDefaultTime = 1;
    /// <summary>
    /// The Default delay time before look operations will occur. This can be modified in code.
    /// </summary>
    public static float lookDefaultDelay = 0;
    /// <summary>
    /// The Default transition type for look animation. This can be modified in code.
    /// </summary>
    public static EasingType lookDefaultTransition = EasingType.EaseInOutCubic;


    //Move Defaults
    /// <summary>
    /// The Default time that the move operations using Bezier Curves will take to move the object. This can be modified in code.
    /// </summary>
    public static float moveBezierDefaultTime = 1;
    /// <summary>
    /// The Default delay time before move operations using Bezier Curves will occur. This can be modified in code.
    /// </summary>
    public static float moveBezierDefaultDelay = 0;
    /// <summary>
    /// The Default transition type for move animation using Bezier Curves. This can be modified in code.
    /// </summary>
    public static EasingType moveBezierDefaultTransition = EasingType.EaseInOutCubic;

    /// <summary>
    /// The Default look Speed for move animation using Bezier Curves. This can be modified in code.
    /// </summary>
    public static float moveBezierDefaultLookSpeed = 8;


    //Scale Defaults
    /// <summary>
    /// The Default time that the scale operations will take to scale the object. This can be modified in code.
    /// </summary>
    public static float scaleDefaultTime = 1;
    /// <summary>
    /// The Default delay time before scale operations will occur. This can be modified in code.
    /// </summary>
    public static float scaleDefaultDelay = 0;
    /// <summary>
    /// The Default transition type for scale animation. This can be modified in code.
    /// </summary>
    public static EasingType scaleDefaultTransition = EasingType.EaseInOutCubic;


    //Rotate To Defaults
    /// <summary>
    /// The Default time that the rotating operations will take to rotate the object. (This is not used by rotateBy) This can be modified in code.
    /// </summary>
    public static float rotateDefaultTime = 1;
    /// <summary>
    /// The Default delay time rotating operations will rotate the object. (This is not used by rotateBy) This can be modified in code.
    /// </summary>
    public static float rotateDefaultDelay = 0;
    /// <summary>
    /// The Default transition type for rotate animation. (This is not used by rotateBy) This can be modified in code.
    /// </summary>
    public static EasingType rotateDefaultTransition = EasingType.EaseInOutCubic;


    //Color To Defaults
    /// <summary>
    /// The Default time that the rotating operations will take to rotate the object. (This is not used by rotateBy) This can be modified in code.
    /// </summary>
    public static float colorDefaultTime = 1;
    /// <summary>
    /// The Default delay time before color operations will occur. This can be modified in code.
    /// </summary>
    public static float colorDefaultDelay = 0;
    /// <summary>
    /// The Default transition type for color "animation". This can be modified in code.
    /// </summary>
    public static EasingType colorDefaultTransition = EasingType.Linear;

    /// <summary>
    /// The default speed that the lookToUpdate function will use
    /// </summary>
    public static float lookToUpdateDefaultSpeed = 3;

    /// <summary>
    /// The default time for a moveToUpdate call
    /// </summary>
    /// 
    public static float moveToUpdateDefaultTime = .05f;

    /// <summary>
    /// This property will inform you if the tween is currently animating. This will be true after the delay and false before. 
    /// </summary>
    public bool inProgress;

    //this is our initialization variable
    private Arguments args;

    //shortcuts
    private Transform _transform;

    private bool _stopTween = false;
    private bool _isKinematic = false;

//    /// <summary>
//    /// This is called by Unity and should be ignored by anyone using the library
//    /// </summary>
//    /// <returns>must be an IEnumerator in order to yield, which it must to delay execution.</returns>
//    public IEnumerator Start()
//    {
//        _transform = transform;
//
//        if (!inProgress && args.includeChildren)
//        {
//            foreach (Transform child in _transform)
//            {
//                //Note, if Arguments was a class, this would not work. Since it is a struct, I get a copy.
//                Arguments newArgs = args;
//                newArgs.onComplete = null;
//                newArgs.onStart = null;
//                newArgs.onUpdate = null;
//                init(child.gameObject, newArgs);
//            }
//        }
//
//        //delay:
//        if (args.delay > 0)
//        {
//            yield return new WaitForSeconds(args.delay);
//        }
//
//        //don't destroy conflicts if we are not running yet.
//        checkForConflicts();
//        inProgress = true;
//
//        DoStartCallback();
//
//        switch (args.type)
//        {
//            case FunctionType.fade:
//                if (gameObject.GetComponent(typeof(Renderer)))
//                {
//                    StartCoroutine(tweenFade());
//                }
//                break;
//
//            case FunctionType.move:
//                enableKinematic();
//                //bezier is different enough to warrant it's own function here.
//                if (args.isBezier)
//                {
//                    StartCoroutine(tweenMoveBezier());
//                }
//                else
//                {
//                    StartCoroutine(tweenMove());
//                }
//                break;
//
//            case FunctionType.scale:
//                enableKinematic();
//                StartCoroutine(tweenScale());
//                break;
//
//            case FunctionType.rotate:
//                enableKinematic();
//                StartCoroutine(tweenRotate());
//                break;
//
//            case FunctionType.color:
//                StartCoroutine(tweenColor());
//                break;
//
//            case FunctionType.punchPosition:
//                enableKinematic();
//                StartCoroutine(tweenPunchPosition());
//                break;
//
//            case FunctionType.punchRotation:
//                enableKinematic();
//                StartCoroutine(tweenPunchRotation());
//                break;
//
//            case FunctionType.shake:
//                enableKinematic();
//                StartCoroutine(tweenShake());
//                break;
//
//            case FunctionType.audio:
//                StartCoroutine(tweenAudio());
//                break;
//
//            case FunctionType.stab:
//                StartCoroutine(tweenStab());
//                break;
//            case FunctionType.look:
//                tweenLook();
//                break;
//        }
//
//        yield return -1;
//    }


//    /// <summary>
//    /// This is called by Unity and should be ignored by anyone using the library
//    /// </summary>
//    public void OnDisable()
//    {
//        //kinematic restoration
//        if (_isKinematic)
//        {
//            rigidbody.isKinematic = false;
//        }
//    }
//
//    #region private non-static helper function
//
//    private void enableKinematic()
//    {
//        if (_transform.gameObject.GetComponent(typeof(Rigidbody)))
//        {
//            if (!rigidbody.isKinematic)
//            {
//                rigidbody.isKinematic = true;
//                _isKinematic = true;
//            }
//        }
//    }


    
    private void DoCallback()
    {
        //callbacks?
        if (args.onComplete != null)
        {
            if (args.onComplete_target == null)
            {
                SendMessage(args.onComplete, args.onComplete_params, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                args.onComplete_target.SendMessage(args.onComplete, args.onComplete_params, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void DoStartCallback()
    {
        //callbacks?
        if (args.onStart != null)
        {
            if (args.onStart_target == null)
            {
                SendMessage(args.onStart, args.onStart_params, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                args.onStart_target.SendMessage(args.onStart, args.onStart_params, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void DoUpdateCallback()
    {
        //callbacks?
        if (args.onUpdate != null)
        {
            if (args.onUpdate_target == null)
            {
                SendMessage(args.onUpdate, args.onUpdate_params, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                args.onUpdate_target.SendMessage(args.onUpdate, args.onUpdate_params, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private static void printVector(string caption, Vector3 v)
    {
        Debug.Log(caption + " - {" + v.x.ToString("f4") + ", " + v.y.ToString("f4") + ", " + v.z.ToString("f4") + "}");
    }

    private static void printVector(string caption, Quaternion v)
    {
        Debug.Log(caption + " - {" + v.x.ToString("f4") + ", " + v.y.ToString("f4") + ", " + v.z.ToString("f4") + "}");
    }
//    //Check for and remove running tweens of same type:
//    private void checkForConflicts()
//    {
//        Component[] scripts = GetComponents(typeof(iTween));
//        if (scripts.Length > 1)
//        {
//            //this is okay to cast as isharpTween because we are only getting that type
//            foreach (iTween tween in scripts)
//            {
//                if ((tween != this) && tween.inProgress && IsTweenSameType(tween.args.type, args.type))
//                {
//                    Destroy(tween);
//                }
//            }
//        }
//    }

//    //I know that this function does not need to be a seperate function, but it is possible
//    //that in the future we might want to have 2 different enums be considered equivalent
//    private static bool IsTweenSameType(FunctionType functionType, FunctionType type)
//    {
//        return (functionType == type);
//    }

    private easingFunction getEasingFunction(EasingType easing)
    {
        return null;
    }

    private Vector3 ReverseDirection(Vector3 currentVector)
    {
        Vector3 newVector = new Vector3(args.xr, args.yg, args.zb);
        args.xr = currentVector.x;
        args.yg = currentVector.y;
        args.zb = currentVector.z;

        return newVector;
    }

    private void DefaultArgsToStartingPosition(Vector3 defaultVector)
    {
        if (!args.isXRSet)
        {            
            args.xr = defaultVector.x;
        }
        if (!args.isYGSet)
        {
            args.yg = defaultVector.y;
        }
        if (!args.isZBSet)
        {
            args.zb = defaultVector.z;
        }
    }


    //Helper method for translating control points into bezier information.
    private List<BezierPointInfo> ParseBeziers(List<Vector3> points)
    {
        List<BezierPointInfo> returnPoints = new List<BezierPointInfo>();

        if (points.Count > 2)
        {

            //the first item is the starting position of the current point for that axis. So, we are storing off the following values:
            //The starting position for the current point for the axis
            //A smoothing point for the curve
            //The next major point			
            for (int iCurPoint = 0; iCurPoint < points.Count - 1; iCurPoint++)
            {
                Vector3 curPoint = points[iCurPoint];

                //I know I am going to store exactly 3, the starting, intermediate and end.
                BezierPointInfo curSetofPoints;

                curSetofPoints.starting = curPoint;
                if (iCurPoint == 0)
                {
                    curSetofPoints.intermediate = points[1] - ((points[2] - curPoint) / 4);
                }
                else
                {
                    //double the current point minus the prior point's intermediate position
                    curSetofPoints.intermediate = 2 * curPoint - returnPoints[iCurPoint - 1].intermediate;
                }

                //This is fine because we end at the next to last item.
                curSetofPoints.end = points[iCurPoint + 1];

                returnPoints.Add(curSetofPoints);
            }
        }
        else
        {
            BezierPointInfo curSetofPoints;
            curSetofPoints.starting = points[0];
            curSetofPoints.intermediate = ((points[0] + points[1]) / 2);
            curSetofPoints.end = points[1];
            returnPoints.Add(curSetofPoints);
        }

        return returnPoints;
    }


    
    private void setFadeFrom()
    {
        if (guiTexture)
        {
            Color currentColor = guiTexture.color;

            guiTexture.color = new Color(currentColor.r, currentColor.g, currentColor.b, args.alpha);
            args.alpha = currentColor.a;
        }
        else
        {
            Color currentColor = renderer.material.color;

            renderer.material.color = new Color(currentColor.r, currentColor.g, currentColor.b, args.alpha);
            args.alpha = currentColor.a;
        }
    }

//    ////Fade to application:
//    private IEnumerator tweenFade()
//    {
//        if (args.isReverse)
//        {
//            setFadeFrom();
//        }
//
//
//        //define targets:
//        float endA = args.alpha;
//
//        //run tween:
//        //don't force a divide by zero, just set it to the end value
//        float startingAlpha = 0;
//        if (args.time > 0)
//        {
//
//            if (_transform.guiTexture)
//            {
//                startingAlpha = _transform.guiTexture.color.a;
//            }
//            else
//            {
//                startingAlpha = _transform.renderer.material.color.a;
//            }
//
//            easingFunction easing = getEasingFunction(args.transition);
//
//            float curTime = 0.0f;
//            while ((curTime < 1) && (!_stopTween))
//            {
//                float newAlpha = easing(startingAlpha, endA, curTime);
//
//                //move
//                if (_transform.guiTexture)
//                {
//                    Color newColor = _transform.guiTexture.color;
//                    newColor.a = newAlpha;
//                    _transform.guiTexture.color = newColor;
//                }
//                else
//                {
//                    Color newColor = _transform.renderer.material.color;
//                    newColor.a = newAlpha;
//                    _transform.renderer.material.color = newColor;
//                }
//
//                DoUpdateCallback();
//                yield return 0;
//                curTime += Time.deltaTime * (1 / args.time);
//            }
//        }
//
//        if (!_stopTween)
//        {
//            //make sure we end up where we are supposed to
//            if (_transform.guiTexture)
//            {
//                Color endColor = _transform.guiTexture.color;
//                endColor.a = endA;
//                _transform.guiTexture.color = endColor;
//            }
//            else
//            {
//                Color endColor = _transform.renderer.material.color;
//                endColor.a = endA;
//                _transform.renderer.material.color = endColor;
//            }
//
//            DoCallback();
//
//            //Loop?
//            if (args.time > 0)
//            {
//                switch (args.loopType)
//                {
//                    case LoopType.none:
//                        Destroy(this);
//                        break;
//                    case LoopType.loop:
//                        //go back to the beginning
//                        if (_transform.guiTexture)
//                        {
//                            Color newColor = _transform.guiTexture.color;
//                            newColor.a = startingAlpha;
//                            _transform.guiTexture.color = newColor;
//                        }
//                        else
//                        {
//                            Color newColor = _transform.renderer.material.color;
//                            newColor.a = startingAlpha;
//                            _transform.renderer.material.color = newColor;
//                        }
//                        StartCoroutine(Start());
//                        break;
//                    case LoopType.pingPong:
//                        args.alpha = startingAlpha;
//                        StartCoroutine(Start());
//                        break;
//                    default:
//                        Destroy(this);
//                        break;
//                }
//            }
//            else
//            {
//                Destroy(this);
//            }
//        }
//        else
//        {
//            Destroy(this);
//        }
//    }

    private void setMoveFrom()
    {
        //move it
        if (args.isWorld)
        {
            _transform.position = ReverseDirection(_transform.position);
        }
        else
        {
            _transform.localPosition = ReverseDirection(_transform.localPosition);
        }
    }

//    //Move to application:
//    private IEnumerator tweenMove()
//    {
//        Vector3 end;
//        Vector3 startingPosition;
//
//        //define targets:
//        if (args.isBy)
//        {
//            end = new Vector3(args.xr,args.yg, args.zb);            
//            startingPosition = Vector3.zero;
//        }
//        else
//        {
//            //can not do this is we are movingBy, because I need the non-moving axis to be 0.
//            if (args.isWorld)
//            {
//                DefaultArgsToStartingPosition(_transform.position);
//            }
//            else
//            {
//                DefaultArgsToStartingPosition(_transform.localPosition);
//            }
//            if (args.isReverse)
//            {
//                
//                setMoveFrom();
//                if (args.transform != null)
//                {
//                    iTween.lookTo(_transform.gameObject, 0, 0, args.transform.position);
//                }
//                else if (args.lookAt.HasValue)
//                {
//                    iTween.lookTo(_transform.gameObject, 0, 0, args.lookAt.Value);
//                }
//            }
//
//            end = new Vector3(args.xr, args.yg, args.zb);
//            startingPosition = args.isWorld ? _transform.position : _transform.localPosition;            
//        }
//
//        //run tween
//        //don't force a divide by zero, just set it to the end value
//        Vector3 lastValues = startingPosition;
//        easingFunction easing = getEasingFunction(args.transition);
//        if (args.time > 0)
//        {
//            float curTime = 0.0f;            
//            while ((curTime < 1) && (!_stopTween))
//            {
//                //when we do a by or an add, we want to be able to do more than one independently, so we will do a translate instead of setting the position.                
//                Vector3 newVector;
//                newVector.x = easing(startingPosition.x, end.x, curTime);
//                newVector.y = easing(startingPosition.y, end.y, curTime);
//                newVector.z = easing(startingPosition.z, end.z, curTime);
//
//                if (args.isBy)
//                {
//                    /*Debug.Log("S");
//                    printVector("New", newVector);
//                    printVector("Last", lastValues);*/
//                    printVector("t", newVector - lastValues);
//                    _transform.Translate(newVector - lastValues, args.space);
//                }
//                else
//                {
//                    if (args.isWorld)
//                    {
//                        _transform.position = newVector;
//                    }
//                    else
//                    {
//                        _transform.localPosition = newVector;
//                    }
//                }
//                
//                lastValues = newVector;
//
//                if (args.transform != null)
//                {
//                    iTween.lookToUpdate(_transform.gameObject, args.transform.position, args.lookSpeed, args.axis);
//                }
//                else if (args.lookAt.HasValue)
//                {
//                    iTween.lookToUpdate(_transform.gameObject, args.lookAt.Value, args.lookSpeed, args.axis);
//                }
//
//                DoUpdateCallback();
//                yield return 0;
//                curTime += Time.deltaTime * (1 / args.time);
//            }
//        }
//
//        if (!_stopTween)
//        {
//            //make sure we end up where we're supposed to.
//            if (args.isBy)
//            {
//                Vector3 newVector;
//                newVector.x = easing(startingPosition.x, end.x, 1);
//                newVector.y = easing(startingPosition.y, end.y, 1);
//                newVector.z = easing(startingPosition.z, end.z, 1);
//
//                _transform.Translate(newVector - lastValues, args.space);
//            }
//            else
//            {
//                if (args.isWorld)
//                {
//                    _transform.position = end;
//                }
//                else
//                {
//                    _transform.localPosition = end;
//                }
//            }
//
//            DoCallback();
//
//            //Loop?
//            if (args.time > 0)
//            {
//                switch (args.loopType)
//                {
//                    case LoopType.none:
//                        Destroy(this);
//                        break;
//                    case LoopType.loop:
//                        //go back to the beginning
//                        if (args.isWorld)
//                        {
//                            _transform.position = startingPosition;
//                        }
//                        else
//                        {
//                            _transform.localPosition = startingPosition;
//                        }
//                        StartCoroutine(Start());
//                        break;
//                    case LoopType.pingPong:
//                        args.xr = startingPosition.x;
//                        args.yg = startingPosition.y;
//                        args.zb = startingPosition.z;
//                        StartCoroutine(Start());
//                        break;
//                    default:
//                        Destroy(this);
//                        break;
//                }
//            }
//            else
//            {
//                Destroy(this);
//            }
//        }
//        else
//        {
//            //cleanup
//            Destroy(this);
//        }
//    }

//    //Bezier move to application - Thank you David Bardos
//    private IEnumerator tweenMoveBezier()
//    {
//
//        //Add the starting position to the curve info.
//        if (args.isWorld)
//        {
//            args.beziers.Insert(0, _transform.position);
//        }
//        else
//        {
//            args.beziers.Insert(0, _transform.localPosition);
//        }
//
//        //help to get midpoints and other helpful info
//        List<BezierPointInfo> _beziers = ParseBeziers(args.beziers);
//        int iNumPoints = _beziers.Count;
//
//        //get the easing function
//        easingFunction easing = getEasingFunction(args.transition);
//
//        //now tween
//        float curTime = 0.0f;
//        while ((curTime < 1) && (!_stopTween))
//        {
//            //get the easing as a percentage... not * 100, of course
//            float virtTimePart = easing(0, 1, curTime);
//
//            //get the array position of the intermediate point that we want
//            int iCurAxisPoint;
//            if (virtTimePart <= 0)
//            {
//                //first array position
//                iCurAxisPoint = 0;
//            }
//            else if (virtTimePart >= 1)
//            {
//                //last array position
//                iCurAxisPoint = iNumPoints - 1;
//            }
//            else
//            {
//                //the transition is > 0 and less than 1. get the position we're looking for.
//                iCurAxisPoint = (int)Mathf.Floor(iNumPoints * virtTimePart);
//            }
//
//            //we are getting how far past the current point we are.
//            float timeFract = iNumPoints * virtTimePart - iCurAxisPoint;
//
//            //get the point info that we are interested in dealing with.
//            BezierPointInfo bpi = _beziers[iCurAxisPoint];
//
//            //get the new vector... I love vector math!
//            Vector3 newVector = bpi.starting + timeFract * (2 * (1 - timeFract) * (bpi.intermediate - bpi.starting) + timeFract * (bpi.end - bpi.starting));
//
//            //orientToPath - cutting off outer ends of curve percentage to avoid lookAt jitters:
//            if (args.lookAt.HasValue || (args.orientToPath && curTime < .99 && curTime > .01) || args.transform != null)
//            {
//                Vector3 lookAtTarget;
//                if (args.transform != null)
//                {
//                    lookAtTarget = args.transform.position;
//                }
//                else if (args.lookAt.HasValue)
//                {
//                    lookAtTarget = args.lookAt.Value;
//                }
//                else
//                {
//                    lookAtTarget = newVector;
//                }
//
//                if (args.isWorld)
//                {
//                    iTween.lookToUpdateWorld(_transform.gameObject, lookAtTarget, args.lookSpeed, args.axis);
//                }
//                else
//                {
//                    iTween.lookToUpdate(_transform.gameObject, lookAtTarget, args.lookSpeed, args.axis);
//                }
//
//                //_transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(newVector - _transform.position, Vector3.up), Time.deltaTime * moveBezierDefaultLookSpeed);
//            }
//
//            //look at target
//            if (args.lookAt.HasValue)
//            {
//                _transform.LookAt(args.lookAt.Value);
//            }
//
//            //move object
//            if (args.isWorld)
//            {
//                _transform.position = newVector;
//            }
//            else
//            {
//                _transform.localPosition = newVector;
//            }
//
//            DoUpdateCallback();
//            //wait until next frame
//            yield return 0;
//            curTime += Time.deltaTime * (1 / args.time);
//        }
//
//        if (!_stopTween)
//        {
//            //get the object to it's final resting position
//            if (args.isWorld)
//            {
//                _transform.position = _beziers[_beziers.Count - 1].end;
//            }
//            else
//            {
//                _transform.localPosition = _beziers[_beziers.Count - 1].end;
//            }
//
//
//            DoCallback();
//
//
//            //Loop?
//            if (args.time > 0)
//            {
//                switch (args.loopType)
//                {
//                    case LoopType.none:
//                        Destroy(this);
//                        break;
//                    case LoopType.loop:
//                        //go back to the beginning
//                        if (args.isWorld)
//                        {
//                            _transform.position = args.beziers[0];
//                        }
//                        else
//                        {
//                            _transform.localPosition = args.beziers[0];
//                        }
//                        //get rid of the one we added at the beginning
//                        args.beziers.RemoveAt(0);
//                        StartCoroutine(Start());
//                        break;
//                    case LoopType.pingPong:
//                        args.beziers.Reverse();
//
//                        //get rid of the one we are now at
//                        args.beziers.RemoveAt(0);
//                        StartCoroutine(Start());
//                        break;
//                    default:
//                        Destroy(this);
//                        break;
//                }
//            }
//            else
//            {
//                Destroy(this);
//            }
//        }
//        else
//        {
//            Destroy(this);
//        }
//
//    }



    private void setScaleFrom()
    {
        _transform.localScale = ReverseDirection(_transform.localScale);
    }

//    //Scale to application:
//    private IEnumerator tweenScale()
//    {
//        Vector3 end;
//
//        if (args.isBy)
//        {
//            if (args.isMultiply)
//            {
//                float x = args.isXRSet ? args.xr : 1;
//                float y = args.isYGSet ? args.yg : 1;
//                float z = args.isZBSet ? args.zb : 1;
//
//                end = new Vector3(_transform.localScale.x * x, _transform.localScale.y * y, _transform.localScale.z * z);
//            }
//            else
//            {
//                end = new Vector3(_transform.localScale.x + args.xr, _transform.localScale.y + args.yg, _transform.localScale.z + args.zb);
//            }
//        }
//        else
//        {
//            DefaultArgsToStartingPosition(_transform.localScale);
//
//            if (args.isReverse)
//            {
//                setScaleFrom();
//            }
//
//            //define targets:
//            end = new Vector3(args.xr, args.yg, args.zb);
//        }
//
//        //run tween:
//        //don't force a divide by zero, just set it to the end value
//        Vector3 start = _transform.localScale;
//        if (args.time > 0)
//        {
//            //define object:                
//            easingFunction easing = getEasingFunction(args.transition);
//
//            float curTime = 0.0f;
//            while ((curTime < 1) && (!_stopTween))
//            {
//                Vector3 newVector;
//                newVector.x = easing(start.x, end.x, curTime);
//                newVector.y = easing(start.y, end.y, curTime);
//                newVector.z = easing(start.z, end.z, curTime);
//
//                //move
//                _transform.localScale = newVector;
//
//                DoUpdateCallback();
//                yield return 0;
//                curTime += Time.deltaTime * (1 / args.time);
//            }
//        }
//
//        if (!_stopTween)
//        {
//            //make sure we end up where we're supposed to.
//            _transform.localScale = end;
//
//            DoCallback();
//
//            //Loop?
//            if (args.time > 0)
//            {
//                switch (args.loopType)
//                {
//                    case LoopType.none:
//                        Destroy(this);
//                        break;
//                    case LoopType.loop:
//                        //go back to the beginning
//                        if (args.isWorld)
//                        {
//                            _transform.localScale = start;
//                        }
//                        else
//                        {
//                            _transform.localScale = start;
//                        }
//                        StartCoroutine(Start());
//                        break;
//                    case LoopType.pingPong:
//                        args.xr = start.x;
//                        args.yg = start.y;
//                        args.zb = start.z;
//                        StartCoroutine(Start());
//                        break;
//                    default:
//                        Destroy(this);
//                        break;
//                }
//            }
//            else
//            {
//                Destroy(this);
//            }
//        }
//        else
//        {
//            Destroy(this);
//        }
//    }


    private void setRotateFrom()
    {
        if (args.isWorld)
        {
            _transform.eulerAngles = ReverseDirection(_transform.eulerAngles);
        }
        else
        {
            _transform.localEulerAngles = ReverseDirection(_transform.localEulerAngles);            
        }        
    }

//    //Rotate application:
//    private IEnumerator tweenRotate()
//    {
//        Vector3 end;
//        Vector3 e = args.isWorld ? _transform.eulerAngles : _transform.localEulerAngles;
//        
//        if (args.isBy)
//        {
//            if (args.isMultiply)
//            {
//                end = new Vector3(360 * args.xr, 360 * args.yg, 360 * args.zb);
//            }
//            else
//            {
//                end = new Vector3(args.xr, args.yg, args.zb);
//            }
//        }
//        else
//        {
//            DefaultArgsToStartingPosition(e);
//
//            if (args.isReverse)
//            {
//                setRotateFrom();
//            }
//
//            end = new Vector3(clerp(e.x, args.xr, 1), clerp(e.y, args.yg, 1), clerp(e.z, args.zb, 1));
//        }
//        
//        
//        //cannot jus assign to e because rotatefrom modifies what the start is.
//        Vector3 start = args.isWorld ? _transform.eulerAngles : _transform.localEulerAngles;
//
//        Quaternion starte = Quaternion.Euler(start);
//        Quaternion ende = Quaternion.Euler(end);
//        
//        //don't force a divide by zero, just set it to the end value
//        easingFunction easing = getEasingFunction(args.transition);
//        Vector3 oldRotation = Vector3.zero;
//        if (args.time > 0)
//        {
//            //define object:                
//            float curTime = 0;
//                        
//            while ((curTime < 1) && (!_stopTween))
//            {
//                
//                if (args.isBy)
//                {
//                    Vector3 newRotation = new Vector3(easing(0, end.x, curTime), easing(0, end.y, curTime),easing(0, end.z, curTime));
//
//                    _transform.Rotate(newRotation - oldRotation, args.space);
//                    oldRotation = newRotation;
//
//                }
//                else
//                {
//                    float movetime = easing(0, 1, curTime);
//
//                    if (args.isWorld)
//                    {
//                        _transform.rotation = Quaternion.Slerp(starte, ende, movetime);
//                    }
//                    else
//                    {
//                        _transform.localRotation = Quaternion.Slerp(starte, ende, movetime);
//                    }
//                }
//
//                DoUpdateCallback();
//                yield return 0;
//                curTime += Time.deltaTime * (1 / args.time);
//            }
//        }
//
//        if (!_stopTween)
//        {
//            if (args.isBy)
//            {
//                Vector3 newVector;
//                newVector.x = easing(0, end.x, 1);
//                newVector.y = easing(0, end.y, 1);
//                newVector.z = easing(0, end.z, 1);
//
//                _transform.Rotate(newVector - oldRotation, args.space);
//            }
//            else
//            {
//                //make sure we end up where we're supposed to.
//                if (args.isWorld)
//                {
//                    _transform.rotation = Quaternion.Euler(end);
//                }
//                else
//                {
//                    _transform.localRotation = Quaternion.Euler(end);
//                }
//            }
//            DoCallback();
//
//            //Loop?
//            if (args.time > 0)
//            {
//                switch (args.loopType)
//                {
//                    case LoopType.none:
//                        Destroy(this);
//                        break;
//                    case LoopType.loop:
//                        //go back to the beginning                       
//                        _transform.localRotation = starte;
//
//                        StartCoroutine(Start());
//                        break;
//                    case LoopType.pingPong:
//                        args.xr = start.x;
//                        args.yg = start.y;
//                        args.zb = start.z;
//
//                        args.isXRSet = true;
//                        args.isYGSet = true;
//                        args.isZBSet = true;
//                        StartCoroutine(Start());
//                        break;
//                    default:
//                        Destroy(this);
//                        break;
//                }
//            }
//            else
//            {
//                Destroy(this);
//            }
//        }
//        else
//        {
//            Destroy(this);
//        }
//    }
//
//   //look application
//    private void tweenLook()
//    {
//        //look point:
//        Vector3 lookPoint;
//        if (args.transform == null)
//        {
//            lookPoint = args.lookAt.Value;
//        }
//        else
//        {
//            lookPoint = args.transform.position;
//        }
//
//        //startRotation:	
//        Vector3 startRotation = args.isWorld ? _transform.eulerAngles : _transform.localEulerAngles;
//
//        //endRotation:
//        _transform.LookAt(lookPoint);
//
//        Vector3 endRotation = args.isWorld ? _transform.eulerAngles : _transform.localEulerAngles;
//
//        if (args.isWorld)
//        {
//            _transform.eulerAngles = startRotation;
//        }
//        else
//        {
//            _transform.localEulerAngles = startRotation;
//        }
//
//
//        //Brute force axis back to previous value if user wants single axis usage:
//        if ((args.axis & AxisType.x) == 0)
//        {
//            endRotation.x = startRotation.x;
//        }
//        if ((args.axis & AxisType.y) == 0)
//        {
//            endRotation.y = startRotation.y;
//        }
//        if ((args.axis & AxisType.z) == 0)
//        {
//            endRotation.z = startRotation.z;
//        }
//
//        //copy the args and reinit
//        Arguments newArgs = args;
//        newArgs.delay = 0;
//        newArgs.type = FunctionType.rotate;
//        newArgs.xr = endRotation.x;
//        newArgs.yg = endRotation.y;
//        newArgs.zb = endRotation.z;
//        newArgs.isXRSet = true;
//        newArgs.isYGSet = true;
//        newArgs.isZBSet = true;
//
//        init(_transform.gameObject, newArgs);
//
//        Destroy(this);
//    }

    private void setColorFrom()
    {
        //we need to swap 
        Color oldColor = guiTexture ? guiTexture.color : renderer.material.color;

        Color newColor = new Color(args.xr, args.yg, args.zb, oldColor.a);

        //now update the target's color
        if (guiTexture)
        {
            guiTexture.color = newColor;
        }
        else
        {
            renderer.material.color = newColor;
        }

        args.xr = oldColor.r;
        args.yg = oldColor.g;
        args.zb = oldColor.b;
    }

//    //Color to application:
//    private IEnumerator tweenColor()
//    {
//        if (args.isReverse)
//        {
//            setColorFrom();
//        }
//
//        //define targets:
//        Color end = new Color(args.xr, args.yg, args.zb);
//
//        Color start;
//        if (_transform.guiTexture)
//        {
//            start= _transform.guiTexture.color;
//        }
//        else if (_transform.light)
//        {
//            start = _transform.light.color;
//        }
//        else
//        {
//            start = _transform.renderer.material.color;
//        }
//
//        //run tween:
//        //don't force a divide by zero, just set it to the end value
//        if (args.time > 0)
//        {
//            easingFunction easing = getEasingFunction(args.transition);
//
//            float curTime = 0.0f;
//            while ((curTime < 1) && (!_stopTween))
//            {
//
//                Color newColor = new Color(start.r, start.g, start.b, start.a);
//                newColor.r = easing(start.r, end.r, curTime);
//                newColor.g = easing(start.g, end.g, curTime);
//                newColor.b = easing(start.b, end.b, curTime);
//
//                //move, make sure the alpha is done seperately from the color.
//                if (_transform.guiTexture)
//                {
//                    newColor.a = _transform.guiTexture.color.a;
//                    _transform.guiTexture.color = newColor;
//                }
//                else if (_transform.light)
//                {
//                    newColor.a = _transform.light.color.a;
//                    _transform.light.color = newColor;
//                }
//                else
//                {
//                    newColor.a = _transform.renderer.material.color.a;
//                    _transform.renderer.material.color = newColor;
//                }
//
//                DoUpdateCallback();
//                yield return 0;
//                curTime += Time.deltaTime * (1 / args.time);
//            }
//        }
//
//        //make sure we end up where we're supposed to.
//        if (_transform.guiTexture)
//        {
//            end.a = _transform.guiTexture.color.a;
//            _transform.guiTexture.color = end;
//            start.a = end.a;
//        }
//        else if (_transform.light)
//        {
//            end.a = _transform.light.color.a;
//            _transform.light.color = end;
//            start.a = end.a;
//        }
//        else
//        {
//            end.a = _transform.renderer.material.color.a;
//            _transform.renderer.material.color = end;
//            start.a = end.a;
//        }
//
//        if (!_stopTween)
//        {
//            DoCallback();
//
//            //Loop?
//            if (args.time > 0)
//            {
//                switch (args.loopType)
//                {
//                    case LoopType.none:
//                        Destroy(this);
//                        break;
//                    case LoopType.loop:
//                        //go back to the beginning                       
//
//                        if (_transform.guiTexture)
//                        {
//                            _transform.guiTexture.color = start;
//                        }
//                        else if (_transform.light)
//                        {
//                            _transform.light.color = start;
//                        }
//                        else
//                        {
//                            _transform.renderer.material.color = start;
//                        }
//
//                        StartCoroutine(Start());
//                        break;
//                    case LoopType.pingPong:
//                        args.xr = start.r;
//                        args.yg = start.g;
//                        args.zb = start.b;
//                        StartCoroutine(Start());
//                        break;
//                    default:
//                        Destroy(this);
//                        break;
//                }
//            }
//            else
//            {
//                Destroy(this);
//            }
//        }
//        else
//        {
//            //cleanup
//            Destroy(this);
//        }
//    }

    private void setAudioFrom()
    {
        AudioSource sound = args.audioSource;

        float destinationVolume = sound.volume;
        float destinationPitch = sound.pitch;

        sound.volume = args.volume;
        sound.pitch = args.pitch;

        args.volume = destinationVolume;
        args.pitch = destinationPitch;
    }

//    //Audio to application:
//    private IEnumerator tweenAudio()
//    {
//        //construct args:        
//        if (args.audioSource == null)
//        {
//            args.audioSource = audio;
//        }
//
//        if (args.isReverse)
//        {
//            setAudioFrom();
//        }
//
//        AudioSource sound = args.audioSource;
//
//        //define targets:
//        float endV = args.volume;
//        float endP = args.pitch;
//
//        //run tween:
//        //don't force a divide by zero, just set it to the end value
//        if (args.time > 0)
//        {
//            //define start:            
//            float startV = sound.volume;
//            float startP = sound.pitch;
//
//            easingFunction easing = getEasingFunction(args.transition);
//
//            float curTime = 0.0f;
//            while ((curTime < 1) && (!_stopTween))
//            {
//                //move
//                sound.volume = easing(startV, endV, curTime);
//                sound.pitch = easing(startP, endP, curTime);
//
//                yield return 0;
//                curTime += Time.deltaTime * (1 / args.time);
//            }
//        }
//
//        if (!_stopTween)
//        {
//            //make sure we get all the way there
//            sound.volume = endV;
//            sound.pitch = endP;
//
//            DoCallback();
//        }
//        //cleanup
//        Destroy(this);
//    }
//
//
//
//    //Punch Position application:
//    private IEnumerator tweenPunchPosition()
//    {
//        //run tween:
//        //don't force a divide by zero, just set it to the end value
//        if (args.time > 0)
//        {
//            Vector3 startPosition = args.isWorld ? _transform.position : _transform.localPosition;
//            float curTime = 0.0f;
//            while ((curTime < 1) && (!_stopTween))
//            {
//                Vector3 newVector;
//
//                newVector.x = punch(Mathf.Abs(args.xr), curTime) * Mathf.Sign(args.xr);
//                newVector.y = punch(Mathf.Abs(args.yg), curTime) * Mathf.Sign(args.yg);
//                newVector.z = punch(Mathf.Abs(args.zb), curTime) * Mathf.Sign(args.zb);
//
//                //move
//                _transform.Translate(newVector, args.space);
//
//                DoUpdateCallback();
//                yield return 0;
//                curTime += Time.deltaTime * (1 / args.time);
//            }
//
//
//            //put us where we are supposed to be
//            //NOTE: Since in the case where there is no time alotment, there can be no movement because the start is the end
//            if (args.isWorld)
//            {
//                _transform.position = startPosition;
//            }
//            else
//            {
//                _transform.localPosition = startPosition;
//            }
//        }
//
//        if (!_stopTween)
//        {
//            DoCallback();
//        }
//
//        //cleanup
//        Destroy(this);
//    }
//
//    //Punch Position application:
//    private IEnumerator tweenPunchScale()
//    {
//        //run tween:
//        //don't force a divide by zero, just set it to the end value
//        if (args.time > 0)
//        {
//            Vector3 startScale = _transform.localScale;
//
//            float curTime = 0.0f;
//            while ((curTime < 1) && (!_stopTween))
//            {
//                Vector3 newVector;
//
//                newVector.x = punch(Mathf.Abs(args.xr), curTime) * Mathf.Sign(args.xr);
//                newVector.y = punch(Mathf.Abs(args.yg), curTime) * Mathf.Sign(args.yg);
//                newVector.z = punch(Mathf.Abs(args.zb), curTime) * Mathf.Sign(args.zb);
//
//                //move
//                _transform.localScale = startScale + newVector;
//
//                DoUpdateCallback();
//                yield return 0;
//                curTime += Time.deltaTime * (1 / args.time);
//            }
//
//            //put us where we are supposed to be
//            //NOTE: Since in the case where there is no time alotment, there can be no movement because the start is the end
//            _transform.localScale = startScale;
//        }
//
//        if (!_stopTween)
//        {
//            DoCallback();
//        }
//
//        //cleanup
//        Destroy(this);
//    }
//
//    //Punch rotation application:
//    private IEnumerator tweenPunchRotation()
//    {
//        //run tween:
//        //don't force a divide by zero, just set it to the end value
//        if (args.time > 0)
//        {
//            //The args were number of rotations
//            float x = args.xr * 360;
//            float y = args.yg * 360;
//            float z = args.zb * 360;
//
//            //define object:
//            Vector3 pos = _transform.localEulerAngles;
//
//            float curTime = 0.0f;
//            Vector3 prevValues = Vector3.zero;
//            while ((curTime < 1) && (!_stopTween))
//            {
//                Vector3 posAug;
//                posAug.x = punch(Mathf.Abs(x), curTime) * Mathf.Sign(x);
//                posAug.y = punch(Mathf.Abs(y), curTime) * Mathf.Sign(y);
//                posAug.z = punch(Mathf.Abs(z), curTime) * Mathf.Sign(z);
//
//                //move
//                _transform.Rotate(posAug - prevValues, args.space);
//                
//                prevValues = posAug;
//                
//                DoUpdateCallback();
//                yield return 0;
//                curTime += Time.deltaTime * (1 / args.time);                
//            }
//
//
//            //Make sure we end up where we are supposed to
//            //NOTE: Since in the case where there is no time alotment, there can be no movement because the start is the end
//            _transform.localRotation = Quaternion.Euler(pos.x, pos.y, pos.z);
//        }
//
//        if (!_stopTween)
//        {
//            DoCallback();
//        }
//
//        //cleanup
//        Destroy(this);
//    }
//
//
//    //Shake application:
//    private IEnumerator tweenShake()
//    {
//        //run tween:
//        //don't force a divide by zero, just set it to the end value
//        if (args.time > 0)
//        {
//            //define targets:
//            Vector3 shakeMagnitude = new Vector3(args.xr, args.yg, args.zb);
//            Vector3 startingPosition = args.isWorld ? _transform.position : _transform.localPosition;
//
//            //run tween:
//            float curTime = 0.0f;
//            while ((curTime < 1) && (!_stopTween))
//            {
//                Vector3 newVector;
//                //if it is the first iteration, we make the impact, otherwize, we make it random
//                if (curTime > 0)
//                {
//                    newVector.x = startingPosition.x + Random.Range(-shakeMagnitude.x, shakeMagnitude.x);
//                    newVector.y = startingPosition.y + Random.Range(-shakeMagnitude.y, shakeMagnitude.y);
//                    newVector.z = startingPosition.z + Random.Range(-shakeMagnitude.z, shakeMagnitude.z);
//                }
//                else
//                {
//                    newVector.x = startingPosition.x + shakeMagnitude.x;
//                    newVector.y = startingPosition.y + shakeMagnitude.y;
//                    newVector.z = startingPosition.z + shakeMagnitude.z;
//                }
//
//                if (args.isWorld)
//                {
//                    _transform.position = newVector;
//                }
//                else
//                {
//                    _transform.localPosition = newVector;
//                }
//                                
//
//                DoUpdateCallback();
//                yield return 0;
//
//                curTime += Time.deltaTime * (1 / args.time);
//
//                //setup for next iteration, reduce the maximum displacement Linearly by time. (doing it this way is equivalent to the JS IMPACT
//                shakeMagnitude.x = args.xr - (curTime * args.xr);
//                shakeMagnitude.y = args.yg - (curTime * args.yg);
//                shakeMagnitude.z = args.zb - (curTime * args.zb);
//            }
//
//            //Make sure we end up where we are supposed to
//            //NOTE: Since in the case where there is no time alotment, there can be no movement because the start is the end
//            if (args.isWorld)
//            {
//                _transform.position = startingPosition;
//            }
//            else
//            {
//                _transform.localPosition = startingPosition;
//            }
//        }
//
//        if (!_stopTween)
//        {
//            DoCallback();
//        }
//        //cleanup
//        Destroy(this);
//    }
//
//    //Stab application:
//    private IEnumerator tweenStab()
//    {
//        //make sure we have audio to play!
//        if (audio == null)
//        {
//            gameObject.AddComponent("AudioSource");
//            audio.playOnAwake = false;
//        }
//
//        //construct args:       
//        if (args.clip == null)
//        {
//            args.clip = gameObject.audio.clip;
//        }
//
//        //target:
//        AudioSource obj = gameObject.audio;
//        obj.clip = args.clip;
//        obj.volume = args.volume;
//        obj.pitch = args.pitch;
//
//        //move
//        obj.PlayOneShot(obj.clip);
//
//        //wait until the clip is played
//        yield return new WaitForSeconds(obj.clip.length / args.pitch);
//
//        if (!_stopTween)
//        {
//            DoCallback();
//        }
//
//        //cleanup
//        Destroy(this);
//    }

	private static void lookToUpdateApply(GameObject target, Vector3 lookAtTarget, float lookSpeed, AxisType axis, bool isLocal)
    {
        Vector3 startingRotation = isLocal ? target.transform.localEulerAngles : target.transform.eulerAngles;
        Quaternion targetRotation = isLocal ? Quaternion.LookRotation(lookAtTarget - target.transform.localPosition, Vector3.up) : Quaternion.LookRotation(lookAtTarget - target.transform.position, Vector3.up);

        target.transform.rotation = Quaternion.Slerp(target.transform.rotation, targetRotation, Time.deltaTime * lookSpeed);

        //Brute force axis back to previous value if user wants single axis usage:
        Vector3 currentRotaion = isLocal ? target.transform.localEulerAngles : target.transform.eulerAngles;
        if ((axis & AxisType.x) == 0)
        {
            currentRotaion.x = startingRotation.x;
        }

        if ((axis & AxisType.y) == 0)
        {
            currentRotaion.y = startingRotation.y;
        }

        if ((axis & AxisType.z) == 0)
        {
            currentRotaion.z = startingRotation.z;
        }

        if (isLocal)
        {
            target.transform.localEulerAngles = currentRotaion;
        }
        else
        {
            target.transform.eulerAngles = currentRotaion;
        }
    }


    //Registers and preps for static to component swap:
    private static void init(GameObject target, Arguments args)
    {
//        iTween obj = (iTween)target.AddComponent(typeof(iTween));
//        obj.args = args;
    }
	
	public static void MoveTo(MoveToArgs args)
	{
		var moveTo = args.Target.AddComponent<MoveTo>();
		moveTo.Args = args;
	}
	
	public static void RotateTo(RotateToArgs args)
	{
		var rotateTo = args.Target.AddComponent<RotateTo>();
		rotateTo.Args = args;
	}
	
	public static void LookTo(LookToArgs args)
	{
		var lookTo = args.Target.AddComponent<LookTo>();
		lookTo.Args = args;
	}

    /// <summary>
    /// Stops (and removes) tweening on an object:
    /// </summary>
    /// <param name="obj">The GameObject that you want to stop all tweening on</param>
    public static void Stop(GameObject obj)
    {
        var scripts = obj.GetComponents(typeof(TweenyBehavior));
        foreach (TweenyBehavior tween in scripts)
        {
            tween.StopTween();
        }
    }

    /// <summary>
    /// Stops (and removes) tweening of a certain type on an object derived from the root of the type (i.e. "moveTo" = "move").
    /// Any move, rotate, fade, color or scale will match any of the same type.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="type"></param>
    public static void stopType(GameObject obj, System.Type type)
    {
        var scripts = obj.GetComponents(typeof(TweenyBehavior));

        foreach (TweenyBehavior tween in scripts)
        {
            if (tween.GetType() == type)
            {
                tween.StopTween();
            }
        }
    }


    /// <summary>
    /// Finds the number of iTween scripts attached to the gameobject
    /// </summary>
    /// <param name="obj">The GameObject that you wish to know the count of iTween scripts</param>
    /// <returns>The number of iTween scripts attached to the given GameObject</returns>
    public static int tweenCount(GameObject obj)
    {
        return obj.GetComponents(typeof(TweenyBehavior)).Length;
    }

    /// <summary>
    /// Will change the audio of the object to the volume, pitch and audio source specified
    /// </summary>
    /// <param name="target">The GameObject that will have the audio changed</param>
    /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
    /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
    /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
    /// <param name="audioSource">The audio source object that will be used for playing the audio. If null, the audio propery of the target will be used.</param>
    /// <param name="transition">The method in which you wish it to transition the audio. If null, it will use audioDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void audioFrom(GameObject target, float? time, float? delay, float? volume, float? pitch, AudioSource audioSource, EasingType? transition,
        string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, audioDefaultTime, delay, audioDefaultDelay, transition, audioDefaultTransition, onComplete, oncomplete_params,
                 FunctionType.audio, true, volume, audioDefaultVolume, pitch, audioDefaultPitch, null, audioSource, oncomplete_target, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will change the audio of the object to the volume, pitch and audio source specified
    /// </summary>
    /// <param name="target">The GameObject that will have the audio changed</param>
    /// <param name="time">The length of time it will take to do the audio change. If null, it will use the audioDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the audioDefaultDelay</param>
    /// <param name="volume">The volume that the audio will be changed to. If null, it will use the audioDefaultVolume</param>
    /// <param name="pitch">The pitch that the audio will be changed to.  If null, it will use the audioDefaultPitch</param>
    /// <param name="audioSource">The audio source object that will be used for playing the audio. If null, the audio propery of the target will be used.</param>
    /// <param name="transition">The method in which you wish it to transition the audio. If null, it will use audioDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void audioTo(GameObject target, float? time, float? delay, float? volume, float? pitch, AudioSource audioSource, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, audioDefaultTime, delay, audioDefaultDelay, transition, audioDefaultTransition, onComplete, oncomplete_params,
            FunctionType.audio, false, volume, audioDefaultVolume, pitch, audioDefaultPitch, null, audioSource, oncomplete_target, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will change the color of the object to the color specified and transition to the starting color at tween time
    /// </summary>
    /// <param name="target">The GameObject that will be colored</param>
    /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
    /// <param name="r">The red value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
    /// <param name="g">The green value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
    /// <param name="b">The blue value of the rgb color to change the color from. If null, 0 will be used for the value.</param>
    /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="includeChildren">If true (default), all children will be colored in the same fashion. Note that the callback will only be done for the top level tween.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void colorFrom(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, bool includeChildren, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, colorDefaultTime, delay, colorDefaultDelay, transition, colorDefaultTransition, onComplete, oncomplete_params,
           FunctionType.color, true, r, g, b, false, false, false, includeChildren, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will change the color of the object to the color specified by the r,g and b parameters
    /// </summary>
    /// <param name="target">The GameObject that will be colored</param>
    /// <param name="time">The length of time it will take to do the coloration. If null, it will use the colorDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the colorDefaultDelay</param>
    /// <param name="r">The red value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
    /// <param name="g">The green value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
    /// <param name="b">The blue value of the rgb color to change the color to. If null, 0 will be used for the value.</param>
    /// <param name="transition">The method in which you wish it to transition the color. If null, it will use colorDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
    /// <param name="includeChildren">If true (default), all children will be colored in the same fashion. Note that the callback will only be done for the top level tween.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void colorTo(GameObject target, float? time, float? delay, float? r, float? g, float? b, EasingType? transition,
        string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, bool includeChildren, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, colorDefaultTime, delay, colorDefaultDelay, transition, colorDefaultTransition, onComplete, oncomplete_params,
           FunctionType.color, false, r, g, b, false, false, false, includeChildren, oncomplete_target, loopType, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will fade from the given alpha to the current alpha of the given object
    /// </summary>
    /// <param name="target">The GameObject that will be faded</param>
    /// <param name="time">The length of time it will take to do the fade from. If null, it will use the fadeDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
    /// <param name="alpha">The alpha that you wish to fade from to the current color. If null, it will fade from 0.</param>
    /// <param name="transition">The method in which you wish it to fade. If null, it will use fadeDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="includeChildren">If true (default), all children will be faded in the same fashion. Note that the callback will only be done for the top level tween.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void fadeFrom(GameObject target, float? time, float? delay, float? alpha, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, bool includeChildren, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, fadeDefaultTime, delay, fadeDefaultDelay, transition, fadeDefaultTransition, onComplete, oncomplete_params,
            FunctionType.fade, true, alpha, 0, includeChildren, oncomplete_target, LoopType.none, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will take the current color and fade it to the given alpha channel 
    /// </summary>
    /// <param name="target">The GameObject that will be faded</param>
    /// <param name="time">The length of time it will take to do the fade. If null, it will use the fadeDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the fadeDefaultDelay</param>
    /// <param name="alpha">The alpha that you wish to fade to. If null, it will fade to 0.</param>
    /// <param name="transition">The method in which you wish it to fade. If null, it will use fadeDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
    /// <param name="includeChildren">If true (default), all children will be faded in the same fashion. Note that the callback will only be done for the top level tween.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void fadeTo(GameObject target, float? time, float? delay, float? alpha, EasingType? transition, string onComplete, object oncomplete_params,
        GameObject oncomplete_target, LoopType loopType, bool includeChildren, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, fadeDefaultTime, delay, fadeDefaultDelay, transition, fadeDefaultTransition, onComplete, oncomplete_params,
            FunctionType.fade, false, alpha, 0, includeChildren, oncomplete_target, loopType, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will look to the given vector. 
    /// </summary>
    /// <param name="target">The GameObject that will be reoriented</param>
    /// <param name="time">The length of time it will take to do the look to. If null, it will use the lookDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the lookDefaultDelay</param>
    /// <param name="lookAt">The vector that the object will be oriented towards</param>
    /// <param name="axis">The axis that the rotation will be constrained to. </param>
    /// <param name="transition">The method in which you wish it to look. If null, it will use lookDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void lookFrom(GameObject target, float? time, float? delay, Vector3 lookAt, AxisType? axis, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, lookDefaultTime, delay, lookDefaultDelay, transition, rotateDefaultTransition, onComplete, oncomplete_params,
            FunctionType.look, true, null, null, null, false, false, false, false, oncomplete_target, LoopType.none, null, lookAt, axis, 1, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will look to the given vector. 
    /// </summary>
    /// <param name="target">The GameObject that will be reoriented</param>
    /// <param name="time">The length of time it will take to do the look to. If null, it will use the lookDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the lookDefaultDelay</param>
    /// <param name="lookAt">The vector that the object will be oriented towards</param>
    /// <param name="axis">The axis that the rotation will be constrained to. </param>
    /// <param name="transition">The method in which you wish it to look. If null, it will use lookDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void lookTo(GameObject target, float? time, float? delay, Vector3 lookAt, AxisType? axis, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, lookDefaultTime, delay, lookDefaultDelay, transition, rotateDefaultTransition, onComplete, oncomplete_params,
            FunctionType.look, false, null, null, null, false, false, false, false, oncomplete_target, LoopType.none, null, lookAt, axis, 1, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
        
    }

    /// <summary>
    /// Will move the object on each axis the distance specified
    /// If any of x, y, or z are null then the value would be 0.
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
    /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    /// <param name="lookAt">The position that the given object will be oriented towards as it moves.</param>
    /// <param name="lookAtAxis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    public static void moveBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, Vector3? lookAt, AxisType? lookAtAxis, float? lookSpeed,
        string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, false, x, y, z, true, false, false, false, oncomplete_target, LoopType.none, null, lookAt, lookAtAxis, lookSpeed, moveDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will move the object on each axis the distance specified
    /// If any of x, y, or z are null then the value would be 0.
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
    /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    /// <param name="lookAtTransform">The transform (that may move during animation) that the target object will orient towards.</param>
    /// <param name="lookAtAxis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    public static void moveBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, Transform lookAtTransform, AxisType? lookAtAxis, float? lookSpeed,
        string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, false, x, y, z, true, false, false, false, oncomplete_target, LoopType.none, lookAtTransform, null, lookAtAxis, lookSpeed, moveDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }


    /// <summary>
    /// Will move the object on each axis the distance specified
    /// If any of x, y, or z are null then the value would be 0.
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
    /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    /// <param name="lookAt">The position that the given object will be oriented towards as it moves.</param>
    /// <param name="lookAtAxis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    public static void moveByWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, Vector3? lookAt, AxisType? lookAtAxis, float? lookSpeed,
        string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, false, x, y, z, true, true, false, false, oncomplete_target, LoopType.none, null, lookAt, lookAtAxis, lookSpeed, moveDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will move the object on each axis the distance specified
    /// If any of x, y, or z are null then the value would be 0.
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
    /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    /// <param name="lookAtTransform">The transform (that may move during animation) that the target object will orient towards.</param>
    /// <param name="lookAtAxis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    public static void moveByWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, Transform lookAtTransform, AxisType? lookAtAxis, float? lookSpeed,
        string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, false, x, y, z, true, true, false, false, oncomplete_target, LoopType.none, lookAtTransform, null, lookAtAxis, lookSpeed, moveDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }
   
    /// <summary>
    /// Will move the object from the given location to its starting position.
    /// If any of x, y, or z are null then the object will not move on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
    /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    /// <param name="lookAtTransform">The transform (that may move during animation) that the target object will orient towards.</param>
    /// <param name="lookAtAxis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    public static void moveFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, Transform lookAtTransform, AxisType? lookAtAxis, float? lookSpeed,
        string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        
        init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, true, x, y, z, false, false, false, false, oncomplete_target, LoopType.none, lookAtTransform, null, lookAtAxis, lookSpeed, moveDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

   
    /// <summary>
    /// Will move the object from the given location in World Space to its starting position in World Space.
    /// If any of x, y, or z are null then the object will not move on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
    /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    /// <param name="lookAt">The position that the given object will be oriented towards as it moves.</param>
    /// <param name="lookAtAxis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    public static void moveFromWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, Vector3? lookAt, AxisType? lookAtAxis, float? lookSpeed,
        string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, true, x, y, z, false, true, false, false, oncomplete_target, LoopType.none, null, lookAt, lookAtAxis, lookSpeed, moveDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will move the object from the given location in World Space to its starting position in World Space.
    /// If any of x, y, or z are null then the object will not move on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
    /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    /// <param name="lookAtTransform">The transform (that may move during animation) that the target object will orient towards.</param>
    /// <param name="lookAtAxis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    public static void moveFromWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, Transform lookAtTransform, AxisType? lookAtAxis, float? lookSpeed,
        string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, true, x, y, z, false, true, false, false, oncomplete_target, LoopType.none, lookAtTransform, null, lookAtAxis, lookSpeed, moveDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will move to the given coordinates in world space. 
    /// If any of x, y, or z are null then the object will not move on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
    /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    /// <param name="lookAt">The position that the given object will be oriented towards as it moves.</param>
    /// <param name="lookAtAxis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
    public static void moveToWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, LoopType loopType, Vector3? lookAt, AxisType? lookAtAxis, float? lookSpeed,
        string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, false, x, y, z, false, false, false, false, oncomplete_target, loopType, null, lookAt, lookAtAxis, lookSpeed, moveDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }


    /// <summary>
    /// Will move to the given coordinates in world space. 
    /// If any of x, y, or z are null then the object will not move on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
    /// <param name="x">The distance to move to on the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The distance to move to on the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The distance to move to on the z axis. If null, it will not move on the z axis.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    /// <param name="lookAtTransform">The transform (that may move during animation) that the target object will orient towards.</param>
    /// <param name="lookAtAxis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
    public static void moveToWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, LoopType loopType, Transform lookAtTransform, AxisType? lookAtAxis, float? lookSpeed,
        string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, false, x, y, z, false, false, false, false, oncomplete_target, loopType, lookAtTransform, null, lookAtAxis, lookSpeed, moveDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }
	
	/// <summary>
    /// Will move to the given vector values. 
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveDefaultDelay</param>
    /// <param name="vector">The distance to move. Don't make this null.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    /// <param name="lookAt">The position that the given object will be oriented towards as it moves.</param>
    /// <param name="lookAtAxis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
//    public static void MoveTo(GameObject target, float? time, float? delay, Vector3 vector, EasingType? transition, LoopType loopType, Vector3? lookAt, AxisType? lookAtAxis, float? lookSpeed,
//        string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
//    {
//        init(target, new Arguments(time, moveDefaultTime, delay, moveDefaultDelay, transition, moveDefaultTransition, onComplete, oncomplete_params,
//           FunctionType.move, false, vector.x, vector.y, vector.z, false, false, false, false, oncomplete_target, loopType, lookAtTransform, null, lookAtAxis, lookSpeed, moveDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
//    }

    /// <summary>
    /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
    /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
    /// <param name="orientToPath">If true, the object will be oriented to the path during the animation. If false, the object's orientation will remain the same through the animation</param>
    /// <param name="lookAt">If this value is set, orientToPath will be ignored and the object will constantly be oriented toward this vector during the animation.</param>
    /// <param name="axis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void moveToBezier(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, bool orientToPath, Vector3? lookAt, AxisType? axis, float? lookSpeed, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, moveBezierDefaultTime, delay, moveBezierDefaultDelay, transition, moveBezierDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, false, points, false, oncomplete_target, loopType, orientToPath, lookAt, null, axis, lookSpeed, moveBezierDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates. 
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
    /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
    /// <param name="orientToPath">If true, the object will be oriented to the path during the animation. If false, the object's orientation will remain the same through the animation</param>
    /// <param name="lookAtTransform">The transform (that may move during animation) that the target object will orient towards.</param>
    /// <param name="axis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void moveToBezier(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, bool orientToPath, Transform lookAtTransform, AxisType? axis, float? lookSpeed, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, moveBezierDefaultTime, delay, moveBezierDefaultDelay, transition, moveBezierDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, false, points, false, oncomplete_target, loopType, orientToPath, null, lookAtTransform, axis, lookSpeed, moveBezierDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }
    
    /// <summary>
    /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property.  
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
    /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
    /// <param name="orientToPath">If true, the object will be oriented to the path during the animation. If false, the object's orientation will remain the same through the animation</param>
    /// <param name="lookAt">The position that the given object will be oriented towards as it moves.</param>
    /// <param name="axis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void moveToBezierWorld(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, bool orientToPath, Vector3? lookAt, AxisType? axis, float? lookSpeed, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, moveBezierDefaultTime, delay, moveBezierDefaultDelay, transition, moveBezierDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, false, points, true, oncomplete_target, loopType, orientToPath, lookAt, null, axis, lookSpeed, moveBezierDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will move to the final coordinates in the list using a bezier curve as defined by the coordinates using the gameObject's transform's position property.  
    /// </summary>
    /// <param name="target">The GameObject that will be moved</param>
    /// <param name="time">The length of time it will take to do the move. If null, it will use the moveBezierDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the moveBezierDefaultDelay</param>        
    /// <param name="points">The collection of points in which to calculate the bezier curve. The last point is the ending point.</param>
    /// <param name="transition">The method in which you wish it to move. If null, it will use moveBezierDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
    /// <param name="orientToPath">If true, the object will be oriented to the path during the animation. If false, the object's orientation will remain the same through the animation</param>
    /// <param name="lookAtTransform">If this value is set, orientToPath will be ignored and the object will constantly be oriented toward this vector during the animation.</param>
    /// <param name="axis">The axis which rotation will occur. If not specified, it will rotate freely on x, y and z</param>
    /// <param name="lookSpeed">The speed at which the object will rotate to look at the position. If it is not specified, moveLookSpeeDefault will be used.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void moveToBezierWorld(GameObject target, float? time, float? delay, List<Vector3> points, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, bool orientToPath, Transform lookAtTransform, AxisType? axis, float? lookSpeed, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, moveBezierDefaultTime, delay, moveBezierDefaultDelay, transition, moveBezierDefaultTransition, onComplete, oncomplete_params,
           FunctionType.move, false, points, true, oncomplete_target, loopType, orientToPath, null,lookAtTransform, axis, lookSpeed, moveBezierDefaultLookSpeed, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will punch the object's position with an amplitude per axis as specified by the x, y, and z parameters
    /// the object will use a sine curve to determine the speed given the amplitude.
    /// The object will move and then return to it's starting position
    /// If any of x, y, or z are null then it will not move on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be punched</param>
    /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchPositionDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchPositionDefaultDelay</param>
    /// <param name="x">The amplitude to punch on the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The amplitude to punch on the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The amplitude to punch on the z axis. If null, it will not move on the z axis.</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void punchPosition(GameObject target, float? time, float? delay, float? x, float? y, float? z, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        //NOTE: the easing type is set to any random value because it is not used
        init(target, new Arguments(time, punchPositionDefaultTime, delay, punchPositionDefaultDelay, EasingType.EaseInExpo, EasingType.EaseInExpo, onComplete, oncomplete_params,
          FunctionType.punchPosition, false, x, y, z, false, false, false, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }


    /// <summary>
    /// Will punch the object's position with an amplitude per axis as specified by the x, y, and z parameters
    /// the object will use a sine curve to determine the speed given the amplitude.
    /// The object will move and then return to it's starting position
    /// If any of x, y, or z are null then it will not move on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be punched</param>
    /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchPositionWorldDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchPositionWorldDefaultDelay</param>
    /// <param name="x">The amplitude to punch on the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The amplitude to punch on the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The amplitude to punch on the z axis. If null, it will not move on the z axis.</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void punchPositionWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        //NOTE: the easing type is set to any random value because it is not used
        init(target, new Arguments(time, punchPositionDefaultTime, delay, punchPositionDefaultDelay, EasingType.EaseInExpo, EasingType.EaseInExpo, onComplete, oncomplete_params,
          FunctionType.punchPosition, false, x, y, z, false, true, false, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will punch the object's rotation with an amplitude per axis as specified by the x, y, and z parameters
    /// the object will use a sine curve to determine the speed given the amplitude.
    /// The object will rotate and then return to it's starting position
    /// If any of x, y, or z are null then it will not rotate on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be punched</param>
    /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchRotationDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchRotationDefaultDelay</param>
    /// <param name="x">The amplitude to punch on the x axis (The number of rotations). If null, it will not rotate on the x axis.</param>
    /// <param name="y">The amplitude to punch on the y axis (The number of rotations). If null, it will not rotate on the y axis.</param>
    /// <param name="z">The amplitude to punch on the z axis (The number of rotations). If null, it will not rotate on the z axis.</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void punchRotation(GameObject target, float? time, float? delay, float? x, float? y, float? z, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        //NOTE: the easing type is set to any random value because it is not used
        init(target, new Arguments(time, punchRotationDefaultTime, delay, punchRotationDefaultDelay, EasingType.EaseInExpo, EasingType.EaseInExpo, onComplete, oncomplete_params,
          FunctionType.punchRotation, false, x, y, z, false, false, false, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will punch the object's rotation with an amplitude per axis as specified by the x, y, and z parameters
    /// the object will use a sine curve to determine the speed given the amplitude.
    /// The object will rotate and then return to it's starting position
    /// If any of x, y, or z are null then it will not rotate on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be punched</param>
    /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchRotationWorldDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchRotationWorldDefaultDelay</param>
    /// <param name="x">The amplitude to punch on the x axis (The number of rotations). If null, it will not rotate on the x axis.</param>
    /// <param name="y">The amplitude to punch on the y axis (The number of rotations). If null, it will not rotate on the y axis.</param>
    /// <param name="z">The amplitude to punch on the z axis (The number of rotations). If null, it will not rotate on the z axis.</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void punchRotationWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        //NOTE: the easing type is set to any random value because it is not used
//        init(target, new Arguments(time, punchRotationDefaultTime, delay, punchRotationDefaultDelay, EasingType.EaseInExpo, EasingType.EaseInExpo, onComplete, oncomplete_params,
//          FunctionType.punchRotation, false, x, y, z, false, true, false, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will punch the object's scale with an amplitude per axis as specified by the x, y, and z parameters
    /// the object will use a sine curve to determine the speed given the amplitude.
    /// The object will move and then return to it's starting position
    /// If any of x, y, or z are null then it will not move on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be punched</param>
    /// <param name="time">The length of time it will take to do the punch. If null, it will use the punchScaleDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the punchScaleDefaultDelay</param>
    /// <param name="x">The amplitude to punch on the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The amplitude to punch on the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The amplitude to punch on the z axis. If null, it will not move on the z axis.</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void punchScale(GameObject target, float? time, float? delay, float? x, float? y, float? z, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        //NOTE: the easing type is set to any random value because it is not used
//        init(target, new Arguments(time, punchScaleDefaultTime, delay, punchScaleDefaultDelay, EasingType.EaseInExpo, EasingType.EaseInExpo, onComplete, oncomplete_params,
//          FunctionType.punchScale, false, x, y, z, false, false, false, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will rotate the object by adding the degrees in the axis as given.
    /// If any of x, y, or z are null then there will be no rotation on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be rotated</param>
    /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateDefaultDelay</param>
    /// <param name="x">The number of times to rotate on the x axis. If null, it will not rotate on the x axis.</param>
    /// <param name="y">The number of times to rotate on the y axis. If null, it will not rotate on the y axis.</param>
    /// <param name="z">The number of times to rotate on the z axis. If null, it will not rotate on the z axis.</param>
    /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void rotateAdd(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        if (target.guiTexture || target.guiText)
        {
            //TODO: Why not throw a real error?
            Debug.LogError("ERROR: GUITextures cannot be rotated!");
            return;
        }

        init(target, new Arguments(time, rotateDefaultTime, delay, rotateDefaultDelay, transition, rotateDefaultTransition, onComplete, oncomplete_params,
            FunctionType.rotate, false, x, y, z, true, false, false, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will rotate the object by adding the degrees in the axis as given. The rotation will be done relative to world space.
    /// If any of x, y, or z are null then there will be no rotation on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be rotated</param>
    /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateDefaultDelay</param>
    /// <param name="x">The number of times to rotate on the x axis. If null, it will not rotate on the x axis.</param>
    /// <param name="y">The number of times to rotate on the y axis. If null, it will not rotate on the y axis.</param>
    /// <param name="z">The number of times to rotate on the z axis. If null, it will not rotate on the z axis.</param>
    /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void rotateAddWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        if (target.guiTexture || target.guiText)
        {
            //TODO: Why not throw a real error?
            Debug.LogError("ERROR: GUITextures cannot be rotated!");
            return;
        }

        init(target, new Arguments(time, rotateDefaultTime, delay, rotateDefaultDelay, transition, rotateDefaultTransition, onComplete, oncomplete_params,
            FunctionType.rotate, false, x, y, z, true, true, false, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
    /// If any of x, y, or z are null then there will be no rotation on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be rotated</param>
    /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateDefaultDelay</param>
    /// <param name="x">The number of times to rotate on the x axis. If null, it will not rotate on the x axis.</param>
    /// <param name="y">The number of times to rotate on the y axis. If null, it will not rotate on the y axis.</param>
    /// <param name="z">The number of times to rotate on the z axis. If null, it will not rotate on the z axis.</param>
    /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void rotateBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        if (target.guiTexture || target.guiText)
        {
            //TODO: Why not throw a real error?
            Debug.LogError("ERROR: GUITextures cannot be rotated!");
            return;
        }

        init(target, new Arguments(time, rotateDefaultTime, delay, rotateDefaultDelay, transition, rotateDefaultTransition, onComplete, oncomplete_params,
            FunctionType.rotate, false, x, y, z, true, false, true, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will rotate the object The number of rotations defined by each axis. 1 means rotate 360 degrees.
    /// If any of x, y, or z are null then there will be no rotation on that axis.
    /// </summary>
    /// <param name="target">The GameObject that will be rotated</param>
    /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateDefaultDelay</param>
    /// <param name="x">The number of times to rotate on the x axis. If null, it will not rotate on the x axis.</param>
    /// <param name="y">The number of times to rotate on the y axis. If null, it will not rotate on the y axis.</param>
    /// <param name="z">The number of times to rotate on the z axis. If null, it will not rotate on the z axis.</param>
    /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void RotateByWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        if (target.guiTexture || target.guiText)
        {
            //TODO: Why not throw a real error?
            Debug.LogError("ERROR: GUITextures cannot be rotated!");
            return;
        }

        init(target, new Arguments(time, rotateDefaultTime, delay, rotateDefaultDelay, transition, rotateDefaultTransition, onComplete, oncomplete_params,
            FunctionType.rotate, false, x, y, z, true, true, true, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will rotate the object using the shortest path from the specified rotation to the current rotation. 
    /// If any of x, y, or z are null then the current value at tween time of the object will be used.
    /// </summary>
    /// <param name="target">The GameObject that will be rotated</param>
    /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
    /// <param name="x">The rotation on the x axis that will be rotated from. If null, it will not rotate on the x axis.</param>
    /// <param name="y">The rotation on the y axis that will be rotated from. If null, it will not rotate on the y axis.</param>
    /// <param name="z">The rotation on the z axis that will be rotated from. If null, it will not rotate on the z axis.</param>
    /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void rotateFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        if (target.guiTexture || target.guiText)
        {
            //TODO: Why not throw a real error?
            Debug.LogError("ERROR: GUITextures cannot be rotated!");
            return;
        }

        init(target, new Arguments(time, rotateDefaultTime, delay, rotateDefaultDelay, transition, rotateDefaultTransition, onComplete, oncomplete_params,
          FunctionType.rotate, true, x, y, z, false, false, false, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will rotate the object using the shortest path from the specified rotation to the current rotation. 
    /// If any of x, y, or z are null then the current value at tween time of the object will be used.
    /// </summary>
    /// <param name="target">The GameObject that will be rotated</param>
    /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
    /// <param name="x">The rotation on the x axis that will be rotated from. If null, it will not rotate on the x axis.</param>
    /// <param name="y">The rotation on the y axis that will be rotated from. If null, it will not rotate on the y axis.</param>
    /// <param name="z">The rotation on the z axis that will be rotated from. If null, it will not rotate on the z axis.</param>
    /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void RotateFromWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        if (target.guiTexture || target.guiText)
        {
            //TODO: Why not throw a real error?
            Debug.LogError("ERROR: GUITextures cannot be rotated!");
            return;
        }

        init(target, new Arguments(time, rotateDefaultTime, delay, rotateDefaultDelay, transition, rotateDefaultTransition, onComplete, oncomplete_params,
          FunctionType.rotate, true, x, y, z, false, true, false, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }
	
    /// <summary>
    /// Will rotate the object using the shortest path to the target rotation. 
    /// If any of x, y, or z are null then the current value at tween time of the object will be used.
    /// </summary>
    /// <param name="target">The GameObject that will be rotated</param>
    /// <param name="time">The length of time it will take to do the rotation. If null, it will use the rotateToDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the rotateToDefaultDelay</param>
    /// <param name="x">The rotation on the x axis that will be rotated to. If null, it will not rotate on the x axis.</param>
    /// <param name="y">The rotation on the y axis that will be rotated to. If null, it will not rotate on the y axis.</param>
    /// <param name="z">The rotation on the z axis that will be rotated to. If null, it will not rotate on the z axis.</param>
    /// <param name="transition">The method in which you wish it to rotate. If null, it will use rotateToDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void rotateTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        if (target.guiTexture || target.guiText)
        {
            //TODO: Why not throw a real error?
            Debug.LogError("ERROR: GUITextures cannot be rotated!");
            return;
        }

        init(target, new Arguments(time, rotateDefaultTime, delay, rotateDefaultDelay, transition, rotateDefaultTransition, onComplete, oncomplete_params,
          FunctionType.rotate, false, x, y, z, false, false, false, false, oncomplete_target, loopType, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will rotate the object using the shortest path to the target rotation. 
    /// If any of x, y, or z are null then the current value at tween time of the object will be used.
    /// </summary>
    /// <param name="target">The GameObject that will be rotated</param>
    /// <param name="time">The length of time it will take to do the rotation. If null, it will use the RotateToWorldDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the RotateToWorldDefaultDelay</param>
    /// <param name="x">The rotation on the x axis that will be rotated to. If null, it will not rotate on the x axis.</param>
    /// <param name="y">The rotation on the y axis that will be rotated to. If null, it will not rotate on the y axis.</param>
    /// <param name="z">The rotation on the z axis that will be rotated to. If null, it will not rotate on the z axis.</param>
    /// <param name="transition">The method in which you wish it to rotate. If null, it will use RotateToWorldDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void RotateToWorld(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        if (target.guiTexture || target.guiText)
        {
            //TODO: Why not throw a real error?
            Debug.LogError("ERROR: GUITextures cannot be rotated!");
            return;
        }

        init(target, new Arguments(time, rotateDefaultTime, delay, rotateDefaultDelay, transition, rotateDefaultTransition, onComplete, oncomplete_params,
          FunctionType.rotate, false, x, y, z, false, true, false, false, oncomplete_target, loopType, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will scale by adding the given dimensions to the current dimensions. 
    /// If any of x, y, or z are null then it will not scale that dimension
    /// </summary>
    /// <param name="target">The GameObject that will be scaled</param>
    /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
    /// <param name="x">The size to scale by on the x axis. If null, it will not scale on the x axis.</param>
    /// <param name="y">The size to scale by on the y axis. If null, it will not scale on the y axis.</param>
    /// <param name="z">The size to scale by on the z axis. If null, it will not scale on the z axis.</param>
    /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void scaleAdd(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, scaleDefaultTime, delay, scaleDefaultDelay, transition, scaleDefaultTransition, onComplete, oncomplete_params,
          FunctionType.scale, false, x, y, z, true, false, false, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will scale by multiplying the given dimensions to the current dimensions. 
    /// If any of x, y, or z are null then it will not scale that dimension
    /// </summary>
    /// <param name="target">The GameObject that will be scaled</param>
    /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
    /// <param name="x">The size to scale by on the x axis. If null, it will not scale on the x axis.</param>
    /// <param name="y">The size to scale by on the y axis. If null, it will not scale on the y axis.</param>
    /// <param name="z">The size to scale by on the z axis. If null, it will not scale on the z axis.</param>
    /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void scaleBy(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, scaleDefaultTime, delay, scaleDefaultDelay, transition, scaleDefaultTransition, onComplete, oncomplete_params,
          FunctionType.scale, false, x, y, z, true, true, true, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will scale from the given size back to the current size. 
    /// If any of x, y, or z are null then the current value at tween time of the object will be used.
    /// </summary>
    /// <param name="target">The GameObject that will be scaled</param>
    /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
    /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
    /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
    /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>
    /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void scaleFrom(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, scaleDefaultTime, delay, scaleDefaultDelay, transition, scaleDefaultTransition, onComplete, oncomplete_params,
          FunctionType.scale, true, x, y, z, false, false, false, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// Will scale to the given size. 
    /// If any of x, y, or z are null then the current value at tween time of the object will be used.
    /// </summary>
    /// <param name="target">The GameObject that will be scaled</param>
    /// <param name="time">The length of time it will take to do the scaling. If null, it will use the scaleDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the scaleDefaultDelay</param>
    /// <param name="x">The size to scale to on the x axis. If null, it will not scale on the x axis.</param>
    /// <param name="y">The size to scale to on the y axis. If null, it will not scale on the y axis.</param>
    /// <param name="z">The size to scale to on the z axis. If null, it will not scale on the z axis.</param>
    /// <param name="transition">The method in which you wish it to scale. If null, it will use scaleDefaultTransition</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="loopType">The type of Loop that is desired to continue animation indefinitely</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void scaleTo(GameObject target, float? time, float? delay, float? x, float? y, float? z, EasingType? transition, string onComplete, object oncomplete_params, GameObject oncomplete_target, LoopType loopType, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        init(target, new Arguments(time, scaleDefaultTime, delay, scaleDefaultDelay, transition, scaleDefaultTransition, onComplete, oncomplete_params,
          FunctionType.scale, false, x, y, z, false, false, false, false, oncomplete_target, loopType, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// This will shake the object, with an initial impact of the full displacement as specified by the x, y and z parameters
    /// The object will then have a random shaking with a maximum displacement represented by the x, y and z parameters along those axis.
    /// The shake displacement will dissipate Linearly across time from the initial maximum displacement to 0.
    /// The object will at the end return to where it started.
    /// </summary>
    /// <param name="target">The GameObject that will be shaken</param>
    /// <param name="time">The length of time to shake the object. If null, it will use the shakeDefaultTime</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the shakeDefaultDelay</param>
    /// <param name="x">The maximum displacement and initial displacement along the x axis. If null, it will not move on the x axis.</param>
    /// <param name="y">The maximum displacement and initial displacement along the y axis. If null, it will not move on the y axis.</param>
    /// <param name="z">The maximum displacement and initial displacement along the z axis. If null, it will not move on the z axis.</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void shake(GameObject target, float? time, float? delay, float? x, float? y, float? z, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        //NOTE: the easing type is set to any random value because it is not used
//        init(target, new Arguments(time, shakeDefaultTime, delay, shakeDefaultDelay, EasingType.EaseInExpo, EasingType.EaseInExpo, onComplete, oncomplete_params,
//          FunctionType.shake, false, x, y, z, false, false, false, false, oncomplete_target, LoopType.none, null, null, null, null, 1, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }

    /// <summary>
    /// This will play the given audio clip from the audio source of the specified game object.
    /// If the object does not have an audio source attached to it, one will be created and attached to it.
    /// </summary>
    /// <param name="target">The GameObject that will have the audio played from it</param>
    /// <param name="delay">The time in which to wait to begin tweening. This is basically a start time. If null, it will use the stabDefaultDelay</param>
    /// <param name="volume">The volume that the clip will be played. If null, it will use the stabDefaultVolume</param>
    /// <param name="pitch">The pitch that the clip will be played. If null, it will use the stabDefaultPitch</param>
    /// <param name="clip">The audio clip that will be played.</param>
    /// <param name="onComplete">The name of the function that will be called back to upon completion of the tween.</param>
    /// <param name="oncomplete_params">The parameter(s) that will be used with the callback when the tween is complete.</param>
    /// <param name="oncomplete_target">The gameobject that the complete callback will callback to.</param>
    /// <param name="onStart">The name of the function that will be called back to upon start of the tween.</param>
    /// <param name="onStart_params">The parameter(s) that will be used with the callback when the tween is started.</param>
    /// <param name="onStart_target">The gameobject that the start callback will callback to.</param>
    /// <param name="onUpdate">The name of the function that will be called back to upon update of the tween.</param>
    /// <param name="onUpdate_params">The parameter(s) that will be used with the callback when the tween is updated.</param>
    /// <param name="onUpdate_target">The gameobject that the update callback will callback to.</param>
    public static void stab(GameObject target, float? delay, float? volume, float? pitch, AudioClip clip, string onComplete, object oncomplete_params, GameObject oncomplete_target, string onStart, object onStart_params, GameObject onStart_target, string onUpdate, object onUpdate_params, GameObject onUpdate_target)
    {
        //NOTE: the easing type and time are set to any random value because they are not used
//        init(target, new Arguments(1, shakeDefaultTime, delay, stabDefaultDelay, EasingType.EaseInExpo, EasingType.EaseInExpo, onComplete, oncomplete_params,
//          FunctionType.stab, false, volume, stabDefaultVolume, pitch, stabDefaultPitch, clip, null, oncomplete_target, onStart, onStart_params, onStart_target, onUpdate, onUpdate_params, onUpdate_target));
    }
	
	/// <summary>
    /// Will smoothly Look at the vector3.
    /// It will use the speed of lookToUpdateDefaultSpeed
    /// </summary>
    /// <param name="target">The object that will have its orientation changed</param>
    /// <param name="lookAtTarget">The object that target will be oriented towards</param>
    public static void lookToUpdate(GameObject target, Vector3 lookAtTarget)
    {
        lookToUpdateApply(target, lookAtTarget, lookToUpdateDefaultSpeed, AxisType.x | AxisType.y | AxisType.z, true);
    }

    /// <summary>
    /// Will smoothly Look at the vector3.
    /// </summary>
    /// <param name="target">The object that will have its orientation changed</param>
    /// <param name="lookAtTarget">The object that target will be oriented towards</param>
    /// <param name="lookSpeed">The speed at which the animation will take place</param>
    public static void lookToUpdate(GameObject target, Vector3 lookAtTarget, float lookSpeed)
    {
        lookToUpdateApply(target, lookAtTarget, lookSpeed, AxisType.x | AxisType.y | AxisType.z, true);
    }

    /// <summary>
    /// Will smoothly Look at the vector3.
    /// It will use the speed of lookToUpdateDefaultSpeed
    /// </summary>
    /// <param name="target">The object that will have its orientation changed</param>
    /// <param name="lookAtTarget">The object that target will be oriented towards</param>
    /// <param name="axis">The axis that the orientation will be limited to changing on.</param>
    public static void lookToUpdate(GameObject target, Vector3 lookAtTarget, AxisType axis)
    {
        lookToUpdateApply(target, lookAtTarget, lookToUpdateDefaultSpeed, axis, true);
    }

    /// <summary>
    /// Will smoothly Look at the vector3.
    /// It will use the speed of lookToUpdateDefaultSpeed
    /// </summary>
    /// <param name="target">The object that will have its orientation changed</param>
    /// <param name="lookAtTarget">The object that target will be oriented towards</param>
    /// <param name="lookSpeed">The speed at which the animation will take place</param>
    /// <param name="axis">The axis that the orientation will be limited to changing on.</param>
    public static void lookToUpdate(GameObject target, Vector3 lookAtTarget, float lookSpeed, AxisType axis)
    {
        lookToUpdateApply(target, lookAtTarget, lookSpeed, axis, true);
    }
	
    /// <summary>
    /// Will smoothly Look at the vector3 using world space.
    /// It will use the speed of lookToUpdateDefaultSpeed
    /// </summary>
    /// <param name="target">The object that will have its orientation changed</param>
    /// <param name="lookAtTarget">The object that target will be oriented towards</param>
    public static void lookToUpdateWorld(GameObject target, Vector3 lookAtTarget)
    {
        lookToUpdateApply(target, lookAtTarget, lookToUpdateDefaultSpeed, AxisType.x | AxisType.y | AxisType.z, false);
    }

    /// <summary>
    /// Will smoothly Look at the vector3 using world space.
    /// </summary>
    /// <param name="target">The object that will have its orientation changed</param>
    /// <param name="lookAtTarget">The object that target will be oriented towards</param>
    /// <param name="lookSpeed">The speed at which the animation will take place</param>
    public static void lookToUpdateWorld(GameObject target, Vector3 lookAtTarget, float lookSpeed)
    {
        lookToUpdateApply(target, lookAtTarget, lookSpeed, AxisType.x | AxisType.y | AxisType.z, false);
    }

    /// <summary>
    /// Will smoothly Look at the vector3 using world space.
    /// It will use the speed of lookToUpdateDefaultSpeed
    /// </summary>
    /// <param name="target">The object that will have its orientation changed</param>
    /// <param name="lookAtTarget">The object that target will be oriented towards</param>
    /// <param name="axis">The axis that the orientation will be limited to changing on.</param>
    public static void lookToUpdateWorld(GameObject target, Vector3 lookAtTarget, AxisType axis)
    {
        lookToUpdateApply(target, lookAtTarget, lookToUpdateDefaultSpeed, axis, false);
    }

    /// <summary>
    /// Will smoothly Look at the vector3 using world space.
    /// It will use the speed of lookToUpdateDefaultSpeed
    /// </summary>
    /// <param name="target">The object that will have its orientation changed</param>
    /// <param name="lookAtTarget">The object that target will be oriented towards</param>
    /// <param name="lookSpeed">The speed at which the animation will take place</param>
    /// <param name="axis">The axis that the orientation will be limited to changing on.</param>
    public static void lookToUpdateWorld(GameObject target, Vector3 lookAtTarget, float lookSpeed, AxisType axis)
    {
        lookToUpdateApply(target, lookAtTarget, lookSpeed, axis, false);
    }

    /// <summary>
    /// This method is continuously callable for the caller to be able to control their own move To.
    /// </summary>
    /// <param name="target">The object that will be moved</param>
    /// <param name="time">The time in which the animation will occur over</param>
    /// <param name="position">The position that the object will be moved over.</param>
    /// <param name="axis">The axis by which to allow movement on. Other axis will be ignored.</param>
    public static void moveToUpdate(GameObject target, float? time, Vector3 position, AxisType axis)
    {
        float? x = null;
        float? y = null;
        float? z = null;
        if ((axis & AxisType.x) > 0)
        {
            x = position.x;
        }
        if ((axis & AxisType.y) > 0)
        {
            y = position.y;
        }
        if ((axis & AxisType.z) > 0)
        {
            z = position.z;
        }

        moveToUpdateApply(target, time, x, y, z, true);
    }

    private static void moveToUpdateApply(GameObject target, float? time, float? x, float? y, float? z, bool isLocal)
    {
        float tempVelocityX = 0;
        float tempVelocityY = 0;
        float tempVelocityZ = 0;

        float smoothTime = time.HasValue ? time.Value : moveToUpdateDefaultTime;

        Vector3 positions = isLocal? target.transform.localPosition : target.transform.position;
        Vector3 startingPosition = positions;
        if (x.HasValue)
        {
            positions.x = x.Value;
        }
        if (y.HasValue)
        {
            positions.y = y.Value;
        }
        if (z.HasValue)
        {
            positions.z = z.Value;
        }

        float newX = Mathf.SmoothDamp(startingPosition.x, positions.x, ref tempVelocityX, smoothTime);
        float newY = Mathf.SmoothDamp(startingPosition.y, positions.y, ref tempVelocityY, smoothTime);
        float newZ = Mathf.SmoothDamp(startingPosition.z, positions.z, ref tempVelocityZ, smoothTime);

        if (isLocal)
        {
            target.transform.localPosition = new Vector3(newX, newY, newZ);
        }
        else
        {
            target.transform.position = new Vector3(newX, newY, newZ);
        }
    }

    /// <summary>
    /// This method is continuously callable for the caller to be able to control their own move To in world space.
    /// </summary>
    /// <param name="target">The object that will be moved</param>
    /// <param name="time">The time in which the animation will occur over</param>
    /// <param name="position">The position that the object will be moved over.</param>
    /// <param name="axis">The axis by which to allow movement on. Other axis will be ignored.</param>
    public static void moveToUpdateWorld(GameObject target, float? time, Vector3 position, AxisType axis)
    {
        float? x = null;
        float? y = null;
        float? z = null;
        if ((axis & AxisType.x) > 0)
        {
            x = position.x;
        }
        if ((axis & AxisType.y) > 0)
        {
            y = position.y;
        }
        if ((axis & AxisType.z) > 0)
        {
            z = position.z;
        }

        moveToUpdateWorldApply(target, time, x, y, z, false);
    }

    /// <summary>
    /// This method is continuously callable for the caller to be able to control their own move To in world space.
    /// </summary>
    /// <param name="target">The object that will be moved</param>
    /// <param name="time">The time in which the animation will occur over</param>
    /// <param name="x">The x parameter defining the position to move the object to. If null, the object will not mvoe on that axis.</param>
    /// <param name="y">The y parameter defining the position to move the object to. If null, the object will not mvoe on that axis.</param>
    /// <param name="z">The z parameter defining the position to move the object to. If null, the object will not mvoe on that axis.</param>
    public static void moveToUpdateWorld(GameObject target, float? time, float? x, float? y, float? z)
    {
        moveToUpdateWorldApply(target, time, x, y, z, true);
    }

    private static void moveToUpdateWorldApply(GameObject target, float? time, float? x, float? y, float? z, bool isLocal)
    {
        float tempVelocityX = 0;
        float tempVelocityY = 0;
        float tempVelocityZ = 0;

        float smoothTime = time.HasValue ? time.Value : moveDefaultTime;

        Vector3 positions = isLocal ? target.transform.localPosition : target.transform.position;
        Vector3 startingPosition = positions;
        if (x.HasValue)
        {
            positions.x = x.Value;
        }
        if (y.HasValue)
        {
            positions.y = y.Value;
        }
        if (z.HasValue)
        {
            positions.z = z.Value;
        }

        float newX = Mathf.SmoothDamp(startingPosition.x, positions.x, ref tempVelocityX, smoothTime);
        float newY = Mathf.SmoothDamp(startingPosition.y, positions.y, ref tempVelocityY, smoothTime);
        float newZ = Mathf.SmoothDamp(startingPosition.z, positions.z, ref tempVelocityZ, smoothTime);

        if (isLocal)
        {
            target.transform.localPosition = new Vector3(newX, newY, newZ);
        }
        else
        {
            target.transform.position = new Vector3(newX, newY, newZ);
        }
    }

}