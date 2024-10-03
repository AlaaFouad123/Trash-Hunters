using TMPro;
using UnityEngine;

public class UISystem : MonoBehaviour
{
    [Header("Interaction UI Elements")]
    [SerializeField] private GameObject _prompt;

    [SerializeField] private TextMeshProUGUI _promptText;

    #region Interaction UI

    internal void UpdatePromptText(string _promptMessage)
    {
        _prompt.SetActive(true);
        _promptText.text = _promptMessage;
    }

    internal void DisablePromptText() => _prompt.SetActive(false);

    #endregion Interaction UI
}