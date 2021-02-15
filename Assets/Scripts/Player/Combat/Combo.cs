using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Combo", menuName = "Game/Combo")]
public class Combo : ScriptableObject
{
    public ComboAttack[] ComboAttacks;
}
