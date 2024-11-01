public class Bullets
{
    public enum Names
    {
        Rifle,
        SniperRifle,
        // Add other bullets as needed
    }
}

public class BulletAnimator : BaseAnimator<Bullets.Names>
{
    protected override void InitializeDefaultAnimation()
    {
        SetAnimation(Bullets.Names.Rifle);
    }
}
