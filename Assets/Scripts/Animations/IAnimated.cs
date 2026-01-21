using System;

namespace PD3Animations
{
    public interface IAnimated<T>
    {
        public void ValueChanged(object sender, ValueChangedArgs<T> args);

        public void AnimationEnded(object sender, EventArgs args);
    }
}