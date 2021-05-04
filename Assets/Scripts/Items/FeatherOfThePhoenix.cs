
using DefaultNamespace;
using UnityEngine;

public class FeatherOfThePhoenix : ArtifactEffect
{
    private float counter = 1;
    private PlayerHealth _playerHealth;
    private BaseAttackHandler _attackHandler;
    private MeleeAttack _featherOfPhoenixDamage;
    //public float burningDamage = 50f;
    //public float burningRadius = 20f;
    //public float burningTime = 3f;

    public override void AddEffect()
    {
        _playerReference = PlayerManager.Instance;
        _playerHealth = _playerReference.GetComponent<PlayerHealth>();
        _attackHandler = _playerReference.GetComponent<BaseAttackHandler>();
        _featherOfPhoenixDamage = FeatherOfPhoenixDamage();
        _playerHealth.DiePlayerEvent += RevivePlayer;
        _playerHealth.revivePlayer = true;
    }

    private void RevivePlayer()
    {
        if (counter == 0)
            _playerHealth.revivePlayer = false;
        else
        {
            _playerHealth.currentHealth = _playerHealth.maximumHealth;
            counter--;
            try { _attackHandler.HandleAttack(_featherOfPhoenixDamage); }
            catch { Debug.Log("FeatherOfPhoenix"); }
        }
    }

    public MeleeAttack FeatherOfPhoenixDamage()
    {
        var _fopDamage = ScriptableObject.CreateInstance<MeleeAttack>();
        _fopDamage.TargetingType = basetargetingType.AOE;
        _fopDamage.Radius = 10f;
        _fopDamage.DamageType = baseDamageType.DIRECT;
        _fopDamage.baseDamage = 50f;
        _fopDamage.KnockBack = true;
        _fopDamage.KnockBackStrength = 10f;
        return _fopDamage;
    }
}
