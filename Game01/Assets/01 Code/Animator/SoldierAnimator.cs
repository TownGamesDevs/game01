using UnityEngine;

[System.Serializable]
public class SoldierAnimations
{
    public enum Names
    {
        Idle
        // Add more  animations as needed
    }
}


public class SoldierAnimator : BaseAnimator<SoldierAnimations.Names>
{
    protected override void InitializeDefaultAnimation()
    {
        SetAnimation(SoldierAnimations.Names.Idle);
    }
}
