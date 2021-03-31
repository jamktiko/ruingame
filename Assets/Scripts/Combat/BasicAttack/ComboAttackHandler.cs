using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(ComboHandler))]
    public class ComboAttackHandler : BaseAttackHandler
    {
        public ComboHandler comboHandler;
        protected ComboAttack currentComboAttack;

        public override void Awake()
        {
            base.Awake();
            comboHandler = GetComponent<ComboHandler>();
        }

        public override void ExecuteAttack()
        {
            _characterAnimator.SetFloat("attackSpeed", _entityAttackSpeed);
            _characterAnimator.Play(currentComboAttack.animationClip.name);
        }
        protected override float GetAttackEndDuration()
        {
            return currentComboAttack.animationClip.length / _entityAttackSpeed;
        }
        protected override IAttack GetAttack()
        {
            currentComboAttack = comboHandler.ProcessCombo();
            return currentComboAttack.attackData;
        }
    }
}