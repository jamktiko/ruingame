
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Enemy_StateMachine : MonoBehaviour
    {
	    //Gains information from the group
	    public AreaCheck areaInformation;
		
	    public float attackRange;
	    public bool alerted;
	    public Enemy_Group enemyGroup;
	    public bool checkingForPlayer;
	    public enum NPC_EnemyAction { NONE = 0, IDLE, PATROL, INSPECT, ATTACK, APPROACH}
	    public Animator npcAnimator;
	    public GameObject playerTarget;
	    public LayerMask hitTestLayer;
	
	Vector3 targetPos, startingPos;

	public NPC_PatrolNode patrolNode;

	public NPC_EnemyAction currentAction = NPC_EnemyAction.NONE;

	static Enemy_StateMachine rangedEnemy = null, casterEnemy = null;
	
	void Start()
	{
		startingPos = transform.position;
		GoToState(NPC_EnemyAction.PATROL);
	}
	
	void Update()
	{
		switch (currentAction)
		{
			case NPC_EnemyAction.APPROACH:
				ActionUpdate_Approach();
				break;
			case NPC_EnemyAction.PATROL:
				ActionUpdate_Patrol();
				break;
			case NPC_EnemyAction.ATTACK:
				ActionUpdate_Attack();
				break;
		}
	}

	public void GoToState(NPC_EnemyAction newState)
	{
		if (currentAction != NPC_EnemyAction.NONE)
		{
			switch (currentAction)
			{
				case NPC_EnemyAction.APPROACH:
					ActionEnd_Approach();
					break;
				case NPC_EnemyAction.PATROL:
					ActionEnd_Patrol();
					break;
				case NPC_EnemyAction.ATTACK:
					ActionEnd_Attack();
					break;
			}
		}
		currentAction = newState;
		switch (currentAction)
		{
			case NPC_EnemyAction.APPROACH:
				ActionInit_Approach();
				break;
			case NPC_EnemyAction.PATROL:
				ActionInit_Patrol();
				break;
			case NPC_EnemyAction.ATTACK:
				ActionInit_Attack();
				break;
		}
	}

	public string GetStateText()
	{
		string res = "";
		switch (currentAction)
		{
			case NPC_EnemyAction.IDLE:
				res = "IDLE";
				break;
			case NPC_EnemyAction.INSPECT:
				res = "INSPECT";
				break;
			case NPC_EnemyAction.PATROL:
				res = "PATROL";
				break;
			case NPC_EnemyAction.ATTACK:
				res = "ATTACK";
				break;
		}
		return res;
	}

	bool CanSeePlayer()
	{
		RaycastHit hit = new RaycastHit();
		int nRays = 100;
		for (int i = 0; i < nRays; i++)
		{
			float theta = (float)i / nRays * 2 * Mathf.PI;
			if (Physics.Raycast(transform.position, new Vector3(Mathf.Sin(theta), 0, Mathf.Cos(theta)), out hit, 50.0f, hitTestLayer) && hit.collider.tag == "Player")
			{
				SetTargetPos(hit.point);
				return true;
			}
		}
		return false;
	}

	void AttackAction()
	{
		Debug.Log("Attack!");
	}
	
	public bool HasReachedAttackRange()
	{
		float dist = Vector3.Distance(transform.position, playerTarget.transform.position);
		if (dist <= attackRange)
		{
			return true;
		}
		return false;
	}

	public void SetTargetPos(Vector3 newPos)
	{
		targetPos = newPos;
	}

	public void SetAlertPos(Vector3 newPos)
	{
		targetPos = newPos;
	}

	public void Damage()
	{
		
	}

	public void Die()
	{
		
	}

	////////////////////////////// Action: IDLE //////////////////////////////
	void ActionInit_Idle()
	{
		Debug.Log("Patrolling!");
	}

	void ActionUpdate_Idle()
	{
		GoToState(NPC_EnemyAction.PATROL);
	}

	void ActionEnd_Idle()
	{
		
	}
	//////////////////////////////////////////////////////////////////////////

	///////////////////////////// Action: ATTACK /////////////////////////////
	bool actionDone;
	void ActionInit_Attack()
	{
		StartCoroutine("AttackPlayerRoutine");
		/*
		npcAnimator.SetBool("Attack", true);
		//Stops previous attacks
		CancelInvoke("AttackAction");
		actionDone = false
		*/
	}
	void ActionUpdate_Attack()
	{
		//AFTER ATTACKING
		//GoToState(NPC_EnemyAction.APPROACH);
		
	}
	void ActionEnd_Attack()
	{
	}
	///////////////////////////// Action: ATTACK /////////////////////////////
	void ActionInit_Approach()
	{
		Debug.Log("Approaching!");
		alerted = true;
	}
	void ActionUpdate_Approach()
	{
		if (HasReachedAttackRange())
		{
			GoToState(NPC_EnemyAction.ATTACK);
		}
	}
	void ActionEnd_Approach()
	{
	}
	//////////////////////////////////////////////////////////////////////////

	///////////////////////////// Action: PATROL /////////////////////////////
	void ActionInit_Patrol()
	{
		if (!checkingForPlayer)
		{
			FindPlayer();
		}
	}
	void ActionUpdate_Patrol()
	{
		/*if (HasReachedPatrolDestination())
		{
			patrolNode = patrolNode.nextNode;
		}
		*/
	}
	void ActionEnd_Patrol() { }

	void AlertEnemies()
	{
		enemyGroup.AlertManager();
	}

	public void FindPlayer()
	{
		checkingForPlayer = true;
		StartCoroutine("FindPlayerRoutine");
	}

	public IEnumerator FindPlayerRoutine()
	{
		while (playerTarget == null)
		{
			yield return new WaitForSeconds(1f);
			if (CheckForPlayer())
			{
				AlertEnemies();
			}
		}
		checkingForPlayer = false;
	}

	public IEnumerator AttackPlayerRoutine()
	{
		AttackAction();
		yield return new WaitForSeconds(2f);
		GoToState(NPC_EnemyAction.APPROACH);
	}
	private bool CheckForPlayer()
	{
		Debug.Log("Sweeping!");
		RaycastHit[] hitTargets = areaInformation.RayCastAroundArea(hitTestLayer).hitInfo;
		foreach (RaycastHit hit in hitTargets)
		{
			if (hit.collider != null)
			{
				if (hit.collider.gameObject.tag == "Player")
				{
					Debug.Log("Player found!");
					return true;
				}
			}
		}
		return false;
	}
}

