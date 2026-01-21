using System;
using UnityEngine;
using PD3Animations;
using UnityEngine.InputSystem;

public class SpiralLogic : MonoBehaviour, IAnimated<float>
{
    [SerializeField]
    private float _duration;

    private Vector3 _endEuler;

    private Animation<float> _moveLinearAnimation;

    private bool _animationRunning = false;

    private void Awake()
    {
        _moveLinearAnimation = new Animation<float>(10, 1, _duration, Mathf.Lerp, EaseStyle.BackEaseOut);

        _endEuler = transform.localEulerAngles;
        foreach (LinearMoveInSpiral subscriber in GetComponentsInChildren<LinearMoveInSpiral>())
        {
            subscriber.EndPosition = subscriber.transform.position;
            _moveLinearAnimation.ValueChanged += subscriber.ValueChanged;
        }
        _moveLinearAnimation.ValueChanged += ValueChanged;
        _moveLinearAnimation.AnimationEnd += AnimationEnded;

        StartCoroutine(_moveLinearAnimation.Start());
        _animationRunning = true;
    }

    public void ValueChanged(object sender, ValueChangedArgs<float> args)
    {
        gameObject.transform.localEulerAngles = _endEuler + new Vector3(0, -720 * args.Progress, 0);
    }
    public void AnimationEnded(object sender, EventArgs args)
    {
        _animationRunning = false;

        Debug.Log("Spiral animation ended");
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_animationRunning)
            {
                _moveLinearAnimation.TogglePaused();
            }
            else
            {
                StartCoroutine(_moveLinearAnimation.Start());
                _animationRunning = true;
            }
        }
    }

    #region Inputs
    private bool _started = false;

    private void Start()
    {
        EnableInputs();

        _started = true;
    }
    private void OnEnable()
    {
        if (_started)
            EnableInputs();
    }
    private void EnableInputs()
    {
        InputActionMap map = InputHandler.Input.actions.FindActionMap("Player");

        map.FindAction("Jump").started += OnJumpInput;
        map.FindAction("Jump").performed += OnJumpInput;
        map.FindAction("Jump").canceled += OnJumpInput;
    }
    private void OnDisable()
    {
        if (InputHandler.Input != null)
            DisableInputs();
    }
    private void DisableInputs()
    {
        InputActionMap map = InputHandler.Input.actions.FindActionMap("Player");

        map.FindAction("Jump").started -= OnJumpInput;
        map.FindAction("Jump").performed -= OnJumpInput;
        map.FindAction("Jump").canceled -= OnJumpInput;
    }
    #endregion Inputs
}