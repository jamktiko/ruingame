
using UnityEngine;

[CreateAssetMenu(fileName = "ComboAttack", menuName = "Game/ComboAttack")]
public class ComboAttack : ScriptableObject
{
    public AnimationClip animationClip;
    public BaseAttack attackData;

}
