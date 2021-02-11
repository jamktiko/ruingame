using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboAttack", menuName = "Game/ComboAttack")]
public class ComboAttack : ScriptableObject
{
    public AnimationClip animationClip;
    public float Damage;
    public float Speed;
    
}
