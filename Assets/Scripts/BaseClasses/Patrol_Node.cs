using UnityEngine;
using System.Collections;

public class NPC_PatrolNode : MonoBehaviour {
	
    public NPC_PatrolNode nextNode;
/*	public override void DefineNode(){
	
	}
	public override void OnNPCEnter ()
	{

	}
	public override void OnNPCExit ()
	{

	}*/

    public Vector3 GetNextNodePosition(){
        return nextNode.GetPosition ();
    }
    public Vector3 GetPosition(){
        return transform.position;
    }

}