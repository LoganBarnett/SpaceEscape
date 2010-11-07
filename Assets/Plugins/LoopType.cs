namespace TweenySupport
{
	/// <summary>
    /// The LoopType is used to cause certain types of tweening to loop without need for a callback to do it.
    /// </summary>
    public enum LoopType
    {
        /// <summary>
        /// Will not loop - default behavior
        /// </summary>
        None,
        /// <summary>
        /// Will animate from start -> end, start -> end, start -> end
        /// </summary>
        Loop,
        /// <summary>
        /// Will animate from start -> end -> start -> end, etc...
        /// </summary>
        PingPong
    }
}
