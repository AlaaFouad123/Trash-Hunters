using UnityEngine;
using UnityEngine.Events;

public class TrashManager : MonoBehaviour
{
    [SerializeField] private GameObject _plane;
    [SerializeField] private UnityEvent _onScoreUpdate;

    private GameObject _currentGameObject;

    private void Awake()
    {
        ServiceLocator.Instance.RegisterService(this, false);
    }

    public void SetAsChild(GameObject obj)
    {
        if (_currentGameObject != null) return;

        _currentGameObject = obj;
        _plane.SetActive(false);

        obj.transform.SetParent(transform);
        obj.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    internal void CheckLineRenderer(LineRenderer _lineRenderer)
    {
        if (_lineRenderer.positionCount > 2 && _currentGameObject == null)
        {
            _lineRenderer.enabled = true;
            _plane.SetActive(true);
        }
        else if (_lineRenderer.positionCount < 2 && _currentGameObject == null)
        {
            _lineRenderer.enabled = false;
            _plane.SetActive(false);
        }
        else if (_lineRenderer.positionCount < 2 && _currentGameObject != null)
        {
            Destroy(_currentGameObject);
            _currentGameObject = null;

            _lineRenderer.enabled = true;
            _plane.SetActive(true);

            _onScoreUpdate?.Invoke();
        }
        else if (_lineRenderer.positionCount > 2 && _currentGameObject != null)
        {
            _lineRenderer.enabled = true;
            _plane.SetActive(false);
        }
    }
}