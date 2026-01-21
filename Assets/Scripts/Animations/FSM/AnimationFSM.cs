
namespace PD3Animations
{
    public partial class Animation<T>
    {
        private class AnimationFSM : FiniteStateMachine
        {
            public Animation<T> Context { get; set; }

            public AnimationCreated Created { get; set; }
            public AnimationRunning Running { get; set; }
            public AnimationEnded Ended { get; set; }
            public AnimationPaused Paused { get; set; }

            public void Start()
            {
                CurrentState = Running;
            }
            public void TogglePaused()
            {
                if (CurrentState == Running)
                    CurrentState = Paused;
                else if (CurrentState == Paused)
                    CurrentState = Running;
            }
            public void Reset()
            {
                CurrentState = Created;
            }

            public AnimationFSM(Animation<T> context)
            {
                Context = context;
                Created = new AnimationCreated(this);
                Running = new AnimationRunning(this);
                Ended = new AnimationEnded(this);
                Paused = new AnimationPaused(this);
                CurrentState = Created;
            }
        }
    }
}