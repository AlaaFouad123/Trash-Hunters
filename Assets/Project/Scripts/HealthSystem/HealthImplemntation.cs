using UnityEngine;
using UnityEngine.Events;

public class HealthImplemntation : MonoBehaviour
{
    [SerializeField] private UnityEvent OnApplyDamage;
    [SerializeField] private UnityEvent OnDeath;
    private readonly Health health = new();

    private UISystem _uiSystem;

    private void Start()
    {
        _uiSystem = ServiceLocator.Instance.GetService<UISystem>();

        _uiSystem.UpdateHealthUI(health.CurrentHealth, health.MaxHealth);
    }

    public void ApplyDamage(int damage)
    {
        OnApplyDamage?.Invoke();

        health.TakeDamage(damage);
        _uiSystem.UpdateHealthUI(health.CurrentHealth, health.MaxHealth);

        if (health.IsDead)
            OnDeath?.Invoke();
    }
}