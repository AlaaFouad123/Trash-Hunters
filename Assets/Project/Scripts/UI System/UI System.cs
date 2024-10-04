using TMPro;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    [Header("Interaction UI Elements")]
    [SerializeField] private GameObject _prompt;

    [SerializeField] private TextMeshProUGUI _promptText;

    [Header("Health UI Elements")]
    [SerializeField] private TextMeshProUGUI _healthText;

    private void Awake() => ServiceLocator.Instance.RegisterService(this, false);

    #region Interaction UI

    internal void UpdatePromptText(string _promptMessage)
    {
        _prompt.SetActive(true);
        _promptText.text = _promptMessage;
    }

    internal void DisablePromptText() => _prompt.SetActive(false);

    #endregion Interaction UI

    #region Health UI

    internal void UpdateHealthUI(float _currentHealth, float _maxHealth) => _healthText.text = _currentHealth + " / " + _maxHealth;

    #endregion Health UI
}