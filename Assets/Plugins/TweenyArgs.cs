using System.ComponentModel;
	
namespace TweenySupport
{
	public class TweenyArgs
	{
		public float Duration { get; set; }
		public float Delay { get; set; }
		[DefaultValueAttribute(EasingType.EaseInOutCubic)]
	    public EasingType EasingType { get; set; }
		[DefaultValueAttribute(LoopType.None)]
		public LoopType LoopType { get; set; }
		public bool IsWorld { get; set; }
	}
}