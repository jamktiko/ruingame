
using System.Collections.Generic;
using UnityEngine;



namespace DefaultNamespace
{
    public class ArtifactEffect : MonoBehaviour
    {
        protected PlayerManager _playerReference;
        [System.Serializable]
        public class ArtifactModifier
        {
            public float modifier;
            [System.Serializable]
            public enum enumModifiedValues {
                MovementSpeed,
                Jump,
                Damage,
                AttackSpeed
            }
            [System.Serializable]
            public enum Type
            {
                Plus,
                Minus
            }

            public Type type;
            public enumModifiedValues modifiedValues;
        }
        public List<ArtifactModifier> artifactModifiers;

        public virtual void AddEffect()
        {
            _playerReference = PlayerManager.Instance;
            foreach (ArtifactModifier am in artifactModifiers)
            {
                AddSingleModifier(am);
            }
        }
        public virtual void AddSingleModifier(ArtifactModifier am)
        {
            int type;
            if (am.type == ArtifactModifier.Type.Minus)
            {
                type = 0;
            }
            else
            {
                type = 1;
            }
            switch (am.modifiedValues)
            {
                case ArtifactModifier.enumModifiedValues.Damage:
                    _playerReference.ModifyDamage(am.modifier, type);
                    break;
                case ArtifactModifier.enumModifiedValues.Jump:
                    _playerReference.ModifyJump(am.modifier, type);
                    break;
                case ArtifactModifier.enumModifiedValues.MovementSpeed:
                    _playerReference.ModifyMovementSpeed(am.modifier, type);
                    break;
                case ArtifactModifier.enumModifiedValues.AttackSpeed:
                    _playerReference.ModifyAttackSpeed(am.modifier, type);
                    break;
            }
        }
    }
}