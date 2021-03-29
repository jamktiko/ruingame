
using DefaultNamespace;
using UnityEngine;
using System.Collections;

public class LeviathansHide : ArtifactEffect
{
    public float blockedDamage = 10f;
    private PlayerHealth _playerHealth;

    public override void AddEffect()
    {
        _playerReference = PlayerManager.Instance;

        if (_playerReference.TryGetComponent(out _playerHealth))
        {
            _playerHealth.flatResistance += blockedDamage;
        }
    }

    private void OnDestroy()
    {
        _playerHealth.flatResistance -= blockedDamage;
    }
}
