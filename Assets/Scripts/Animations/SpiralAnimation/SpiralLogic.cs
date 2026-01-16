using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PD3Animations;

public class SpiralLogic : MonoBehaviour, IAnimated<float>
{
    [SerializeField]
    private float _duration;

    private Vector3 _endEuler;

    private Animation<float> _moveLinearAnimation;

    private void Awake()
    {
        _moveLinearAnimation = new Animation<float>(10, 1, _duration, Mathf.Lerp);

        _endEuler = transform.localEulerAngles;
        foreach (IAnimated<float> subscriber in GetComponentsInChildren<IAnimated<float>>())
        {
            _moveLinearAnimation.ValueChanged += subscriber.ValueChanged;
        }
        _moveLinearAnimation.AnimationEnd += AnimationEnded;
    }

    private void FixedUpdate()
    {
        _moveLinearAnimation.Update(Time.fixedDeltaTime);
    }

    public void ValueChanged(object sender, ValueChangedArgs<float> args)
    {
        gameObject.transform.localEulerAngles = _endEuler + new Vector3(0, -720 * args.Progress, 0);
    }
    public void AnimationEnded(object sender, EventArgs args)
    {
        Debug.Log("Spiral animation ended");
    }

    public void OnSpaceInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveLinearAnimation.TogglePaused();
        }
    }
}