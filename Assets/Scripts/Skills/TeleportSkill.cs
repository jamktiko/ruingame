using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSkill : SkillExecute
{
    public float teleportDistance = 10f;
    
    public override void Execute()
    {
        var tr = skillUser.transform;
    }
}
