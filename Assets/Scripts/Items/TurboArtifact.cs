﻿using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DefaultNamespace;
using UnityEngine;

public class TurboArtifact : ArtifactEffect
{
    public override void AddEffect()
    {
        //Add all base modifiers
        base.AddEffect();
        Debug.Log("TurboArtifact specific effect!");
    }
}
