
public class PlayerHealth : Health
{
    public float FlatResistance;
    public float PercentualResistance;
    public override void Die()
    {
        PlayerManager pm = GetComponent<PlayerManager>();
        pm.Die();
    }
}
