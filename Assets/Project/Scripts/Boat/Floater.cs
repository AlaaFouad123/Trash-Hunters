using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody; // Reference to the rigidbody component.

    [SerializeField] private float _depthBeforeSubmerged = 1f; // Depth at which the object is considered submerged.
    [SerializeField] private float _displacementAmount = 3f; // Amount of force to apply to the object when submerged.
    [SerializeField] private int _floaterCount = 1; // Number of floaters on the object.
    [SerializeField] private float _waterDrag = 0.99f; // Drag when submerged.
    [SerializeField] private float _waterAngularDrag = 0.5f; // Angular drag when submerged.

    private WaveManager _waveManager;

    private void Start()
    {
        _waveManager = ServiceLocator.Instance.GetService<WaveManager>();
    }

    //private void FixedUpdate()
    //{
    //    float waveHeight = ServiceLocator.Instance.GetService<WaveManager>().GetWaveHeight(transform.position.x);
    //    if (transform.position.y < waveHeight)
    //    {
    //        float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / _depthBeforeSubmerged) * _displacementAmount;
    //        _rigidbody.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), ForceMode.Acceleration);
    //    }
    //}

    private void FixedUpdate()
    {
        _rigidbody.AddForceAtPosition(Physics.gravity / _floaterCount, transform.position, ForceMode.Acceleration);

        float waveHeight = _waveManager.GetWaveHeight(transform.position.x);
        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / _depthBeforeSubmerged) * _displacementAmount;
            _rigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);

            _rigidbody.AddForce(_waterDrag * displacementMultiplier * Time.fixedDeltaTime * -_rigidbody.velocity, ForceMode.VelocityChange);
            _rigidbody.AddTorque(_waterAngularDrag * displacementMultiplier * Time.fixedDeltaTime * -_rigidbody.angularVelocity, ForceMode.VelocityChange);
        }
    }
}