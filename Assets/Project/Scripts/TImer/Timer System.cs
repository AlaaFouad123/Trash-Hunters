using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimerSystem : MonoBehaviour
{
    [SerializeField] private float _timeToCountdown = 60f;
    [SerializeField] private UnityEvent _onTimeUp;
    private float _currentTime;

    private void Start()
    {
        _currentTime = _timeToCountdown;
        ServiceLocator.Instance.GetService<UISystem>().UpdateTimer(_currentTime);
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        while (_currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            _currentTime--;
            ServiceLocator.Instance.GetService<UISystem>().UpdateTimer(_currentTime);
        }

        _onTimeUp?.Invoke();
    }
}