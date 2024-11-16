using System;
using UnityEngine;

public class StartGameSM : BaseState
{
    public static event Action OnKeyPressed;
    public static event Action StartCountDown;
    public override void EnterState(StateManager state)
    {
    }

    public override void ExitState(StateManager state)
    {
    }

    public override void UpdateState(StateManager state)
    {
        if (Input.anyKeyDown)
        {
            // Hide keysUI
            OnKeyPressed?.Invoke();

            // Start countdown
            StartCountDown?.Invoke();


        }
    }


}
