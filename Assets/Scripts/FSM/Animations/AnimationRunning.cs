
namespace PD3Animations
{
    public partial class Animation<T>
    {
        private class AnimationRunning : AnimationState
        {
            public override void Update(float deltaTime)
            {
                T previousProgressValue = FSM.Context.ProgressValue;
                FSM.Context.TotallElapsed += deltaTime;

                if (FSM.Context.TotallElapsed > FSM.Context.Duration)
                    FSM.Context.TotallElapsed = FSM.Context.Duration;

                FSM.Context.OnValueChanged(previousProgressValue, FSM.Context.ProgressValue, FSM.Context.Progress);

                if (FSM.Context.TotallElapsed == FSM.Context.Duration)
                {
                    FSM.CurrentState = FSM.Ended;
                }
            }

            public AnimationRunning(AnimationFSM fSM) : base(fSM) { }
        }
    }
}