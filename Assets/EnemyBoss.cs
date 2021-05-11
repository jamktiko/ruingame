
public class EnemyBoss : Enemy_StateMachine
{
    public override void AttackAction()
    {
        throw new System.NotImplementedException();
    }

    public override void Start()
    {
        base.Start();
        transform.parent = null;
    }
}
