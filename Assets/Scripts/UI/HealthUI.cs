
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.PlayerLoop;

public class HealthUI : MonoBehaviour
{
    public EntityHealth _entityHealth;
    public Canvas _entityUI;
    public Slider _healthSlider;
    private Animator _damageIndicator;


    void Start()
    {
        _entityUI = GetComponent<Canvas>();
        _entityHealth = GetComponentInParent<EntityHealth>();
        _healthSlider = _entityUI.GetComponentInChildren<Slider>();       
        try { _damageIndicator = _entityUI.GetComponentInChildren<Animator>();
        }
        catch { }
        
        StartCoroutine("UpdateUI");
    }

    public void UpdateUIValue(float amount)
    { 
        _healthSlider.value = _entityHealth.currentHealth;
    }

    public void PlayAnimation()
    {
        _damageIndicator.Play("TakeDamage");
    }

    public IEnumerator UpdateUI()
    {
        yield return new WaitForSeconds(0.2f);
        _healthSlider.maxValue = _entityHealth.maximumHealth;
        _healthSlider.minValue = 0;
        _healthSlider.value = _entityHealth.maximumHealth;
    }
}
