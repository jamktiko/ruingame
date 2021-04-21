using UnityEngine;
using System.Collections;
using System;

public class BaseStatusEffect
{
    public float duration { get; set; }

    public float tickRate = 0.25f;
    
    public enum StatusEffectTypes {
        BURN,
        WEAKEN,
        CURSE,
        BLEED
    }

    public StatusEffectTypes statusEffectType;
    public void ApplyConstantEffect()
    {
        Debug.Log("base status effect");
    }

    public void RemoveConstantEffect()
    {
        OnStatusRemoved();
    }
    public void AppliedEffect()
    {
        
    }

    protected virtual void OnStatusInflicted()
    {
        
    }

    protected virtual void OnStatusRemoved()
    {
        
    }


}


