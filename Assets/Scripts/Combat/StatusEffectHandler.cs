using System;
using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class StatusEffectHandler : MonoBehaviour
{
    private IDamageable _entityReceivingEffect;

    [SerializeField]
    private BaseStatusEffect[] _currentStatusEffects = new BaseStatusEffect[Enum.GetValues(typeof(BaseStatusEffect.StatusEffectTypes)).Length];

    public string showText;
    public float showduration;

    private Coroutine lastBurn;
    public void OnStatusInflicted(BaseStatusEffect statusEffect)
    {
        StatusEffectType(statusEffect);
    }

    public void Update()
    {
        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            Debug.Log("O was pressed");
            OnStatusInflicted(new BaseStatusEffect());
        }
    }

    private void StatusEffectType(BaseStatusEffect statusEffect)
    {
        switch (statusEffect.statusEffectType)
        {
            case BaseStatusEffect.StatusEffectTypes.BURN:
                HandleStatusInSlot(0, statusEffect);
                break;
            case BaseStatusEffect.StatusEffectTypes.BLEED:
                HandleStatusInSlot(1, statusEffect);
                break;
            case BaseStatusEffect.StatusEffectTypes.CURSE:
                HandleStatusInSlot(2, statusEffect);
                break;
            case BaseStatusEffect.StatusEffectTypes.WEAKEN:
                HandleStatusInSlot(3, statusEffect);
                break;
        }
    }

    private void HandleStatusInSlot(int slot, BaseStatusEffect incomingStatusEffect)
    {
        showText = incomingStatusEffect.statusEffectType.ToString();
        incomingStatusEffect.duration = 5f;
        IEnumerator coroutine = ApplyConstantEffect(incomingStatusEffect);
        if (_currentStatusEffects[slot] == null)
        {
            _currentStatusEffects[slot] = incomingStatusEffect;
            StartCoroutine(coroutine);
        }
        else
        {
            StopCoroutine(coroutine);
            Debug.Log("Stopped coroutine" + coroutine);
            _currentStatusEffects[slot] = incomingStatusEffect;
            StartCoroutine(coroutine);
        }
    }
    public IEnumerator ApplyConstantEffect(BaseStatusEffect statusEffect)
    {
        while (statusEffect.duration > 0)
        {
            yield return new WaitForSeconds(statusEffect.tickRate);
            statusEffect.ApplyConstantEffect();
            statusEffect.duration -= statusEffect.tickRate;
            showduration = statusEffect.duration;
        }
        statusEffect.RemoveConstantEffect();
    }
}
