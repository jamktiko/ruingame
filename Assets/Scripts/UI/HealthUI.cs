using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class HealthUI : MonoBehaviour
{
    public Health _entityHealth;
    private Canvas _entityUI;
    private Slider _healthSlider;
    
    void Awake()
    {
        _entityUI = GetComponent<Canvas>();
        _entityHealth = GetComponentInParent<Health>();
        _healthSlider = _entityUI.GetComponentInChildren<Slider>();
        _healthSlider.maxValue = _entityHealth._maximumHealth;
        _healthSlider.minValue = 0;
        _healthSlider.value = _entityHealth._maximumHealth;
    }

    private void Start()
    {
        try
        {
            var cam = GameObject.FindGameObjectWithTag("Cameras");
            _entityUI.worldCamera = cam.GetComponentInChildren<Camera>();
        }
        catch {}
    }

    public void UpdateUIValue(float amount)
    {
        _healthSlider.value = _entityHealth.CurrentHealth;
    }
}
