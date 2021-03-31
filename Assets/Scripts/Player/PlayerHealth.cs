
public class PlayerHealth : EntityHealth
{
    public override void Die()
    {
        PlayerManager pm = GetComponent<PlayerManager>();
        pm.Die();
    }
}
