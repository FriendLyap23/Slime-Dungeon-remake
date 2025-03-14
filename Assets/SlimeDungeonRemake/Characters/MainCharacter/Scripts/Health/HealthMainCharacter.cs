using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthMainCharacter : MonoBehaviour
{
    [Header("Layer")]
    [SerializeField] private LayerMask _enemyLayer;

    public int _currentHealth { get; set; }
    [SerializeField] private int _maxHealth = 100;

    public event Action<float> OnHealthChanged;
    public event Action OnDeathCharacter;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void ChangeHealth(int value)
    {
        _currentHealth += value;
        if (_currentHealth <= 0)
            Death();
        else
        {
            float _currentHealthAsPercantage = (float)_currentHealth / _maxHealth;
            OnHealthChanged?.Invoke(_currentHealthAsPercantage);
        }
    }

    private void Death() 
    {
        OnHealthChanged?.Invoke(0);
        OnDeathCharacter?.Invoke();
    }

}
