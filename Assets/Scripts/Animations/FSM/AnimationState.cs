
namespace PD3Animations
{
    public partial class Animation<T>
    {
        private abstract class AnimationState : IState
        {
            protected AnimationFSM FSM;

            public virtual void OnEnter()
            { }

            public virtual void OnExit()
            { }

            public virtual void Update(float deltaTime)
            { }

            public AnimationState(AnimationFSM fSM)
            {
                FSM = fSM;
            }
        }
    }
}