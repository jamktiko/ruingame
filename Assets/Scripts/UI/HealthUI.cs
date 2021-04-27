
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.PlayerLoop;

public class HealthUI : MonoBehaviour
{
    public EntityHealth _entityHealth;
    public Canvas _entityUI;
    public Slider _healthSlider;


    void Start()
    {
        _entityUI = GetComponent<Canvas>();
        _entityHealth = GetComponentInParent<EntityHealth>();
        _healthSlider = _entityUI.GetComponentInChildren<Slider>();
        
        StartCoroutine("UpdateUI");
    }

    public void UpdateUIValue(float amount)
    {
        _healthSlider.value = _entityHealth.currentHealth;
    }
    public IEnumerator UpdateUI()
    {
        yield return new WaitForSeconds(1f);
        _healthSlider.maxValue = _entityHealth.maximumHealth;
        _healthSlider.minValue = 0;
        _healthSlider.value = _entityHealth.maximumHealth;
    }
}
