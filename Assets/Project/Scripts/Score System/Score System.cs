using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int _maxScore;
    [SerializeField] private UnityEvent _onScoreUpdate;
    [SerializeField] private UnityEvent _onMaxScoreReached;

    [Header("Water Material")]
    [SerializeField] private Material _material; // Add a reference to the material

    [SerializeField] private Color _originalShallowColor;
    [SerializeField] private Color _originalDeepColor;

    [SerializeField] private Color _shallowColor;
    [SerializeField] private Color _deepColor;

    private int _score = 0;
    private Coroutine _colorTransitionCoroutine;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService(this, false);
    }

    private void Start()
    {
        ServiceLocator.Instance.GetService<UISystem>().UpdateScoreUI(_score, _maxScore);

        if (_material != null)
        {
            _material.SetColor("_ShallowColor", _originalShallowColor);
            _material.SetColor("_DeepColor", _originalDeepColor);
        }
    }

    internal void UpdateScore(int _scoreToAdd)
    {
        _score += _scoreToAdd;

        _onScoreUpdate?.Invoke();
        ServiceLocator.Instance.GetService<UISystem>().UpdateScoreUI(_score, _maxScore);

        if (_colorTransitionCoroutine != null)
        {
            StopCoroutine(_colorTransitionCoroutine);
        }
        _colorTransitionCoroutine = StartCoroutine(SmoothColorTransition());

        if (_score >= _maxScore)
            _onMaxScoreReached?.Invoke();
    }

    private System.Collections.IEnumerator SmoothColorTransition()
    {
        float duration = 1.0f; // Duration of the transition in seconds
        float elapsedTime = 0f;

        Color startShallowColor = _material.GetColor("_ShallowColor");
        Color startDeepColor = _material.GetColor("_DeepColor");

        float t = (float)_score / _maxScore; // Calculate the interpolation factor
        Color targetShallowColor = Color.Lerp(_originalShallowColor, _shallowColor, t);
        Color targetDeepColor = Color.Lerp(_originalDeepColor, _deepColor, t);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float lerpFactor = elapsedTime / duration;

            Color interpolatedShallowColor = Color.Lerp(startShallowColor, targetShallowColor, lerpFactor);
            Color interpolatedDeepColor = Color.Lerp(startDeepColor, targetDeepColor, lerpFactor);

            _material.SetColor("_ShallowColor", interpolatedShallowColor);
            _material.SetColor("_DeepColor", interpolatedDeepColor);

            yield return null;
        }

        // Ensure the final color is set
        _material.SetColor("_ShallowColor", targetShallowColor);
        _material.SetColor("_DeepColor", targetDeepColor);
    }
}