
using UnityEngine;

[CreateAssetMenu(fileName = "ComboAttack", menuName = "Game/ComboAttack")]
public class ComboAttack : ScriptableObject
{
    public AnimationClip animationClip;
    public BaseAttack AttackData;
    public BaseAttack attackData
    { get { return AttackData;}
    set { AttackData = value; }
    }

}
