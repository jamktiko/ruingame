
using System.Collections;
using UnityEngine;

    public interface IEnemy_StateMachine
    {
	    void Awake();
	    void Start();
	    void Update();
	    void GoToState();
	    string GetStateText();
	    bool CanSeePlayer();
	    void AttackAction();
	    bool HasReachedAttackRange();
	    void SetTargetPos();
	    void Alert();
	    void Damage();
	    void Die();
	    void AlertAllEnemies();
	    void FindPlayer();
		bool CheckForPlayer();
		void MoveTowardsDirection();
		void DecideDirection();
    }

