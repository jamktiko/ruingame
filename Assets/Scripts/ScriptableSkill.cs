using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="ScriptableSkill", menuName ="Game/ScriptableSKill")]
public class ScriptableSkill : ScriptableObject
{
    public string _skillName;
    public string _skillDescription;
    
    public SkillExecute _skillExecute;
    
}
