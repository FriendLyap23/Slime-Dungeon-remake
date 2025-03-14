using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private HealthMainCharacter _healthMainCharacter;
    [SerializeField] private Gradient _gradient;

    private void Awake()
    {
        _healthMainCharacter.OnHealthChanged += OnHealthChanged;
        _healthBarFilling.color = _gradient.Evaluate(1);
    }

    private void OnDestroy()
    {
        _healthMainCharacter.OnHealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(float valueAsPercantage) 
    {
        Debug.Log(valueAsPercantage);
        _healthBarFilling.fillAmount = valueAsPercantage;
        _healthBarFilling.color = _gradient.Evaluate(valueAsPercantage);
    }
}
