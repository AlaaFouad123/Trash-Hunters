using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private float _amplitude = 1f; // Amplitude of the wave.
    [SerializeField] private float _length = 2f; // Frequency of the wave.
    [SerializeField] private float _speed = 1f; // Speed of the wave.
    [SerializeField] private float _offset = 0f; // Speed of the wave.

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService(this, false);
    }

    private void Update()
    {
        _offset += Time.deltaTime * _speed;
    }

    public float GetWaveHeight(float _x) => _amplitude * Mathf.Sin(_x / _length + _offset);
}