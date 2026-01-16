
namespace PD3Animations
{
    public partial class Animation<T>
    {
        private class AnimationEnded : AnimationState
        {
            public override void OnEnter()
            {
                FSM.Context.OnAnimationEnd();
            }

            public AnimationEnded(AnimationFSM fSM) : base(fSM) { }
        }
    }
}