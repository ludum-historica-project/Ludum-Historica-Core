using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoStateMachine : MonoBehaviour
{
    protected MonoState _currentState;

    public void ChangeState(MonoState newState)
    {
        if (_currentState == newState) return;
        if (_currentState != null) _currentState.transitionOut.Raise();
        _currentState = newState;
        _currentState.transitionIn.Raise();
    }

    public void Update()
    {
        if (_currentState != null) _currentState.update.Raise();
    }
}
