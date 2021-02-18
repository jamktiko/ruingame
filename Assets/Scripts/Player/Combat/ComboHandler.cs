using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class ComboHandler : MonoBehaviour
{
    public Combo comboData;
    private int comboStep;
    private float lastAttack;
    public float comboTimer = 3f;
    private ComboAttackHandler attackHandler;
    private void Start()
    {
        attackHandler = GetComponent<ComboAttackHandler>();
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
}
