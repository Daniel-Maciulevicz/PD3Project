using System;
using UnityEngine;

public abstract class FiniteStateMachine
{
    public EventHandler ObjectStateChanged;

    public IState CurrentState
    {
        get { return _objectState; }
        set 
        {
            if (_objectState == value)
                return;

            CurrentState?.OnExit();
            _objectState = value;
            OnStateChanged();
            CurrentState?.OnEnter();
        }
    }
    private IState _objectState;

    public virtual void Update(float deltaTime)
    {
        CurrentState.Update(deltaTime);
    }

    protected virtual void OnStateChanged()
    {
        ObjectStateChanged?.Invoke(this, EventArgs.Empty);
    }
}