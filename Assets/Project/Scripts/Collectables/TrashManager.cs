using UnityEngine;

public class TrashManager : MonoBehaviour
{
    [SerializeField] private GameObject _plane;

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

            int randomScore = Random.Range(1, 11); // Generates a random value between 1 and 10 (inclusive)
            ServiceLocator.Instance.GetService<ScoreSystem>().UpdateScore(randomScore);
        }
        else if (_lineRenderer.positionCount > 2 && _currentGameObject != null)
        {
            _lineRenderer.enabled = true;
            _plane.SetActive(false);
        }
    }
}