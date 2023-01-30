public class BossHealth : BotHealth
{
    private const string HitTrigger = "Hit";

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        Animator.SetTrigger(HitTrigger);
    }
}
