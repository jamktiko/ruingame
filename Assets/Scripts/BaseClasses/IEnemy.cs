
using System.Collections;
using UnityEngine;

    public interface IEnemy
    {
	    void GoToState();
	    string GetStateText();
	    bool CanSeePlayer();
	    void AttackAction();
	    bool HasReachedAttackRange();
	    void SetTargetPos();
	    void Alert();
	    void Die();
	    void AlertAllEnemies();
	    void FindPlayer();
		bool CheckForPlayer();
		void MoveTowardsDirection();
		void DecideDirection();
    }

