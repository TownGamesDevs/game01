using System;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private BaseState _currentState;
    public StartGameSM _startGame = new();
    public NextWaveSM _nextWave = new();

    private void Start() => SetState(_startGame);


    private void Update() => _currentState.UpdateState(this);

    public void SetState(BaseState state)
    {
        _currentState = state;
        _currentState.EnterState(this);
    }
}
