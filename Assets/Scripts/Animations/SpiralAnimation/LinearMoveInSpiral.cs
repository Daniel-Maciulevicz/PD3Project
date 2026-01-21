using System;
using UnityEngine;
using PD3Animations;

public class LinearMoveInSpiral : MonoBehaviour, IAnimated<float>
{
    public Vector3 EndPosition { get; set; }

    public void ValueChanged(object sender, ValueChangedArgs<float> args)
    {
        transform.localPosition = EndPosition * args.NewValue;
    }

    public void AnimationEnded(object sender, EventArgs args)
    { }
}