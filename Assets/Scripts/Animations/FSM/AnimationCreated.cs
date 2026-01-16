
namespace PD3Animations
{
    public partial class Animation<T>
    {
        private class AnimationCreated : AnimationState
        {
            public override void OnEnter()
            {
                FSM.Context.ResetAnimation();
            }

            public AnimationCreated(AnimationFSM fSM) : base(fSM) { }
        }
    }
}