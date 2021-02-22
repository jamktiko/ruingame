using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public float FlatResistance { get => _flatResistance; set => _flatResistance = value; }
    public float PercentualResistance { get => _percentualResistance; set => _percentualResistance = value; }
    public override void Die()
    {
        PlayerManager pm = GetComponent<PlayerManager>();
        pm.Die();
    }
}
