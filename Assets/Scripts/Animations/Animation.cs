using System;

namespace PD3Animations
{
    public class ValueChangedArgs<T> : EventArgs
    {
        public T OldValue { get; private set; }
        public T NewValue { get; private set; }
        public float Progress { get; private set; }

        public ValueChangedArgs(T oldValue, T newValue, float progress)
        {
            OldValue = oldValue;
            NewValue = newValue;
            Progress = progress;
        }
    }

    public partial class Animation<T>
    {
        public event EventHandler<ValueChangedArgs<T>> ValueChanged;
        public event EventHandler AnimationEnd;

        public T From { get; set; }
        public T To { get; set; }

        public float Duration { get; set; }

        public float TotallElapsed { get; set; }

        public float Progress { get { return TotallElapsed / Duration; } }
        public T ProgressValue { get { return LerpValue(From, To, Progress); } }

        public Func<T, T, float, T> LerpValue { get; private set; }

        private AnimationFSM FSM;

        private bool _loop;

        public void Start()
        {
            FSM.Start();
        }
        public void Update(float deltaTime)
        {
            FSM.Update(deltaTime);
        }
        public void TogglePaused()
        {
            FSM.TogglePaused();
        }
        private void OnValueChanged(T oldValue, T newValue, float progress)
        {
            ValueChanged?.Invoke(this, new ValueChangedArgs<T>(oldValue, newValue, progress));
        }
        private void OnAnimationEnd()
        {
            AnimationEnd?.Invoke(this, EventArgs.Empty);

            if (_loop)
            {
                ResetAnimation();
                Start();
            }
        }

        public void ResetAnimation()
        {
            T previousProgressValue = ProgressValue;

            TotallElapsed = 0;

            OnValueChanged(previousProgressValue, ProgressValue, Progress);
        }

        public Animation(T from, T to, float duration, Func<T, T, float, T> lerpT, bool loop = false)
        {
            From = from;
            To = to;
            Duration = duration;
            LerpValue = lerpT;
            FSM = new AnimationFSM(this);
            _loop = loop;
        }
    }

}