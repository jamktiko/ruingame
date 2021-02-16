using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboHandler : MonoBehaviour
{
    public Combo comboData;
    private int comboStep;
    private float lastAttack;
    public float comboTimer = 5f;
    private AttackHandler attackHandler;
    private void Start()
    {
        attackHandler = GetComponent<AttackHandler>();
    }
    public ComboAttack GetComboAttack()
    {
        return comboData.ComboAttacks[comboStep];
    }

    public ComboAttack ProcessCombo()
    {
        ComboAttack currentComboStepAttack = GetComboAttack();
        OnAttack();
        return currentComboStepAttack;
    }

    public void OnAttack()
    {
        float timeSinceLastAttack = Time.time - lastAttack;
        if (timeSinceLastAttack < comboTimer)
        {
            comboStep++;
            if (comboStep == comboData.ComboAttacks.Length)
            {
                comboStep = 0;
            }
        }
        else
        {
            comboStep = 0;
        }
        lastAttack = Time.time;
    }

    private void EndAttack()
    {
        attackHandler.attacking = false;
    }
}
