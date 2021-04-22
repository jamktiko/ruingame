using UnityEngine;

public class AnimationTriggerHandler : MonoBehaviour
{
    private BaseAttackHandler _attackHandler;


    public GameObject _weaponSlash;
    public Transform _weaponSlashPosition;

    public GameObject _whirlWind;
    public Transform _whirlWindPosition;

    public GameObject _stanceChange;
    public Transform _stanceChangePosition;

    private GameObject currentEffectSpawned;



    public void Start()
    {
        _attackHandler = GetComponentInParent<BaseAttackHandler>();
    }

    public void HandleDamage()
    {
        _attackHandler.HandleAttack(_attackHandler.currentAttack);
    }

    public void EndAttack()
    {
        _attackHandler.EndAttack();
    }

    public void WeaponSlash()

    {
        Instantiate(_weaponSlash, _weaponSlashPosition.position, _weaponSlashPosition.rotation);
    }

    public void WhirlWind()

    {
        currentEffectSpawned = Instantiate(_whirlWind, _whirlWindPosition);

        Destroy(currentEffectSpawned, 1.5f);
    }

    public void StanceChange()
    {
        Instantiate(_stanceChange, _stanceChangePosition);

    }
    

    public void DetachFromParent()
    {
    currentEffectSpawned.transform.parent = null;
    }



}
