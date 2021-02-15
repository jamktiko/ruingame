using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void Die()
    {
        PlayerManager pm = GetComponent<PlayerManager>();
        pm.Die();
    }
}
