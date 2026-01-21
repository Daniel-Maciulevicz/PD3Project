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

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _moveLinearAnimation.TogglePaused();
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