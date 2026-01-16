
namespace PD3Animations
{
    public partial class Animation<T>
    {
        private class AnimationPaused : AnimationState
        {
            public AnimationPaused(AnimationFSM fSM) : base(fSM) { }
        }
    }
}