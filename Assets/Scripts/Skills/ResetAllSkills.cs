using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllSkills : SkillExecute
{    
    public override void Execute()
    {
        skillUser.ResetAllSkills();
    }
}
