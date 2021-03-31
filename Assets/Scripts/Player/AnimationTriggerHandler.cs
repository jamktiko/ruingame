using UnityEngine;

public class AnimationTriggerHandler : MonoBehaviour
{
    private PlayerAttackHandler _attackHandler;
    public GameObject _weaponSlash;
    public Transform _weaponSlashPosition;
    public void Start()
    {
        _attackHandler = GetComponentInParent<PlayerAttackHandler>();
    }

    public void HandleDamage()
    {
        _attackHandler.ExecuteAttack();
    }

    public void WeaponSlash()

    {
        Instantiate(_weaponSlash, _weaponSlashPosition.position, _weaponSlashPosition.rotation);
    }
}
