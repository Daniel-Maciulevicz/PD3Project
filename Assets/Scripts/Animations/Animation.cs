using System;
using System.Collections;
using UnityEngine;

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

        public float Progress 
        { 
            get 
            { 
                float progress = TotallElapsed / Duration;

                if (EasingFunction != null)
                {
                    progress = EasingFunction.Invoke(progress, 0, 1, 1);
                }

                return progress;
            } 
        }
        public T ProgressValue { get { return LerpValue(From, To, Progress); } }

        public Func<T, T, float, T> LerpValue { get; private set; }
        public Ease EasingFunction { get; private set; }

        private AnimationFSM FSM;

        public IEnumerator Start()
        {
            if (FSM.CurrentState == FSM.Ended)
                FSM.Reset();
            if (FSM.CurrentState != FSM.Created)
                yield break;
            FSM.Start();
            while (FSM.CurrentState != FSM.Ended)
            {
                FSM.Update(Time.deltaTime);
                yield return null;
            }
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
        }

        public void ResetAnimation()
        {
            T previousProgressValue = ProgressValue;

            TotallElapsed = 0;

            OnValueChanged(previousProgressValue, ProgressValue, Progress);
        }

        public Animation(T from, T to, float duration, Func<T, T, float, T> lerpT, EaseStyle easeStyle)
        {
            From = from;
            To = to;
            Duration = duration;
            LerpValue = lerpT;
            FSM = new AnimationFSM(this);
            EasingFunction = EaseMethods.GetEase(easeStyle);
        }
        public Animation(T from, T to, float duration, Func<T, T, float, T> lerpT)
        {
            From = from;
            To = to;
            Duration = duration;
            LerpValue = lerpT;
            FSM = new AnimationFSM(this);
            EasingFunction = null;
        }
    }
}