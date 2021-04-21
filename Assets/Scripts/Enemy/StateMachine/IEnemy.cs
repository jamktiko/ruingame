
using UnityEngine;

    public interface IEnemy
    {
	    State _currentState
	    {
		    get;
		    set;
	    }

	    void SetState(State state);
	    string GetStateText();
	    bool CanSeePlayer();
	    void AttackAction();
	    bool HasReachedAttackRange();
	    void SetTargetPos(Vector3 target);
	    void Alert();
	    void Die();
	    void AlertAllEnemies();
	    bool CheckForPlayer();
	    Vector3 DecidePatrolDirection();
		Vector3 DecidePathToPlayer();

    }

