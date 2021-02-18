using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkill : SkillExecute
{
    public float teleportDistance = 10f;
    
    public override void Execute(float duration)
    {
        var tr = skillUser.transform;
    }
}
