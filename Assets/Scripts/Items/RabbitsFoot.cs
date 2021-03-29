
using DefaultNamespace;
using UnityEngine;

public class RabbitsFoot : ArtifactEffect
{

    private PlayerHealth _playerHealth;

    public override void AddEffect()
    {
        _playerReference = PlayerManager.Instance;
        if (_playerReference.TryGetComponent(out _playerHealth))
        {
            _playerHealth.DamagePlayerEvent += DodgeAttack;
        }
    }

    private void DodgeAttack()
    {
        float rnd = Random.Range(0f, 21f);
        if (rnd <= 1f)
            _playerHealth.dodgeAttack = true;
    }
    private void OnDestroy()
    {
        _playerHealth.DamagePlayerEvent -= DodgeAttack;
    }
}
