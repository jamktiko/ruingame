using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DefaultNamespace;
using UnityEngine;

public class TurboArtifact : ArtifactEffect
{
    public override void AddEffect(PlayerManager pm)
    {
        //Add all base modifiers
        base.AddEffect(pm);
        Debug.Log("TurboArtifact specific effect!");
    }
}
