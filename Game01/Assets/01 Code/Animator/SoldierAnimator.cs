[System.Serializable]
public class SoldierAnimations
{
    public enum Names
    {
        Idle,
        Moving
        // Add more  animations as needed
    }
}


public class SoldierAnimator : BaseAnimator<SoldierAnimations.Names>
{
    public static SoldierAnimator instance;
    private void Awake() => instance ??= this;
    protected override void InitializeDefaultAnimation() => SetAnimation(SoldierAnimations.Names.Idle);
}
