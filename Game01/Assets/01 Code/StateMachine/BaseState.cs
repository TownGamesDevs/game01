using System;

public abstract class BaseState
{
    public abstract void EnterState(StateManager state);
    public abstract void ExitState(StateManager state);
    public abstract void UpdateState(StateManager state);
}
