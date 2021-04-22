
    public class Enemy_Melee : Enemy_StateMachine
    {
        public override void Awake()
        {
            base.Awake();
            enemyType = EnemyType.Melee;
        }

        public override void AttackAction()
        {
            throw new System.NotImplementedException();
        }
    }