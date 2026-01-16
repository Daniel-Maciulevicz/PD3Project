using System;
using UnityEngine;
using PD3Animations;

public class LinearMoveInSpiral : MonoBehaviour, IAnimated<float>
{
    private Vector3 _endPosition;

    private void Awake()
    {
        _endPosition = transform.localPosition;
    }

    public void ValueChanged(object sender, ValueChangedArgs<float> args)
    {
        transform.localPosition = _endPosition * args.NewValue;
    }

    public void AnimationEnded(object sender, EventArgs args)
    { }
}
