using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace DefaultNamespace
{
    public class ArtifactEffect : MonoBehaviour
    {
        private PlayerManager playerReference;
    
        
        [System.Serializable]
        public class ArtifactModifier
        {
            public float _modifier;
            [System.Serializable]
            public enum modifiedValues {
                MovementSpeed,
                Jump,
                Damage,
                AttackSpeed
            }
            [System.Serializable]
            public enum type
            {
                PLUS,
                MINUS
            }

            public type _type;
            public modifiedValues ModifiedValues;
        }
        public List<ArtifactModifier> Modifiers;

        public virtual void AddEffect(PlayerManager pm)
        {
            playerReference = pm;
            foreach (ArtifactModifier am in Modifiers)
            {
                AddSingleModifier(am);
            }
        }
        public virtual void AddSingleModifier(ArtifactModifier am)
        {
            int type;
            if (am._type == ArtifactModifier.type.PLUS)
            {
                type = 0;
            }
            else
            {
                type = 1;
            }
            switch (am.ModifiedValues)
            {
                case ArtifactModifier.modifiedValues.Damage:
                    playerReference.ModifyDamage(am._modifier, type);
                    break;
                case ArtifactModifier.modifiedValues.AttackSpeed:
                    playerReference.ModifyJump(am._modifier, type);
                    break;
                case ArtifactModifier.modifiedValues.MovementSpeed:
                    playerReference.ModifyMovementSpeed(am._modifier, type);
                    break;
                case ArtifactModifier.modifiedValues.Jump:
                    playerReference.ModifyAttackSpeed(am._modifier, type);
                    break;
            }
        }
    }
}