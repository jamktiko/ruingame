
using DefaultNamespace;

public class DeathState : State
{
    public DeathState(Enemy_StateMachine enemy) : base(enemy)
    {
    }

    public override void OnStateEnter()
    {
        Name = "Dying";
        
    }
    public override void Tick()
    {
    }
}