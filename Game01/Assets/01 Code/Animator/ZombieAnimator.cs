using UnityEngine;

[System.Serializable]
public class ZombieAnimations
{
    public enum Names
    {
        Walk,
        Attack,
        Idle,
        // Add other zombie animations as needed
    }
}
public class ZombieAnimator : BaseAnimator<ZombieAnimations.Names>
{

    protected override void InitializeDefaultAnimation()
    {
        SetAnimation(ZombieAnimations.Names.Walk);
    }
}