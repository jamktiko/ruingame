
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Enemy_StateMachine : MonoBehaviour
    {
	    //Gains information from the group
	    
	    public Enemy_Group enemyGroup;
	    public enum NPC_EnemyAction { NONE = 0, IDLE, PATROL, INSPECT, ATTACK, APPROACH}
	public enum NPC_WeaponType { KNIFE = 0, RIFLE, SHOTGUN }
	public Animator npcAnimator;

	public LayerMask hitTestLayer;
	
	Vector3 targetPos, startingPos;
	
	public NPC_WeaponType weaponType = NPC_WeaponType.KNIFE;
	public Transform firePoint;
	float weaponRange;
	float weaponActionTime, weaponTime;

	public NPC_PatrolNode patrolNode;

	public NPC_EnemyAction currentAction = NPC_EnemyAction.NONE;

	bool canHearPlayer = false;

	static Enemy_StateMachine rifleSolider = null, shotgunSolider = null;
	
	void Start()
	{
		startingPos = transform.position;
		GoToState(NPC_EnemyAction.IDLE);
	}
	
	void Update()
	{
		switch (currentAction)
		{
			case NPC_EnemyAction.IDLE:
				ActionUpdate_Idle();
				break;
			case NPC_EnemyAction.INSPECT:
				ActionUpdate_Inspect();
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
				case NPC_EnemyAction.IDLE:
					ActionEnd_Idle();
					break;
				case NPC_EnemyAction.INSPECT:
					ActionEnd_Inspect();
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
			case NPC_EnemyAction.IDLE:
				ActionInit_Idle();
				break;
			case NPC_EnemyAction.INSPECT:
				ActionInit_Inspect();
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

	}
	
	public bool HasReachedMyDestination()
	{
		/*float dist = Vector3.Distance(transform.position, );
		if (dist <= 1.5f)
		{
			return true;
		}
*/
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

	void AssignWeapons()
	{
		
	}

	////////////////////////////// Action: IDLE //////////////////////////////
	void ActionInit_Idle()
	{
		
	}

	void ActionUpdate_Idle()
	{
		if (CanSeePlayer() || rifleSolider != null || shotgunSolider != null)
		{
			GoToState(NPC_EnemyAction.INSPECT);
		}
	}

	void ActionEnd_Idle()
	{
		
	}
	//////////////////////////////////////////////////////////////////////////

	//////////////////////////// Action: INSPECT /////////////////////////////
	bool inspectWait;
	void ActionInit_Inspect()
	{
	}

	void ActionUpdate_Inspect()
	{
		if (HasReachedMyDestination() && !inspectWait)
		{
			inspectWait = true;
		}
		
		else if (CanSeePlayer())
		{
			transform.forward = targetPos - transform.position;
		}
	}
	void ActionEnd_Inspect()
	{
	}
	//////////////////////////////////////////////////////////////////////////

	///////////////////////////// Action: ATTACK /////////////////////////////
	bool actionDone;
	void ActionInit_Attack()
	{
		npcAnimator.SetBool("Attack", true);
		CancelInvoke("AttackAction");
		Invoke("AttackAction", weaponActionTime);

		actionDone = false;
	}
	void ActionUpdate_Attack()
	{
		//AFTER ATTACKING 
		GoToState(NPC_EnemyAction.INSPECT);
		//}
	}
	void ActionEnd_Attack()
	{
	}
	///////////////////////////// Action: ATTACK /////////////////////////////
	void ActionInit_Approach()
	{
	}
	void ActionUpdate_Approach()
	{
		RaycastHit hit;
		Physics.Raycast(transform.position, transform.forward, out hit, weaponRange, hitTestLayer);
		if (hit.collider != null && hit.collider.tag == "Player")
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
	}
	void ActionUpdate_Patrol()
	{
		if (HasReachedMyDestination())
		{
			patrolNode = patrolNode.nextNode;
		}
	}
	void ActionEnd_Patrol() { }

	void AlertEnemies()
	{
		enemyGroup.AlertManager();
	}
	//////////////////////////////////////////////////////////////////////////
}

