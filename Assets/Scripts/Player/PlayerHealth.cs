
public class PlayerHealth : Health
{

    public float flatResistance;
    public float percentualResistance;
    public override void Die()
    {
        PlayerManager pm = GetComponent<PlayerManager>();
        pm.Die();
    }
}
