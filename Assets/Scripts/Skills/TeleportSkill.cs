using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkill : SkillExecute
{
    public float teleportDistance = 10f;
    public float iFrameDuration = 1f;
    public override void Execute()
    {
        var tr = skillUser.transform;
        var cc = skillUser.gameObject.GetComponent<CharacterController>();
        cc.Move(tr.forward * teleportDistance);
        skillUser.AddInvulnerability(iFrameDuration);
    }
}
