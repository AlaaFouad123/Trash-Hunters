using Bitgem.VFX.StylisedWater;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody; // Reference to the rigidbody component.

    [SerializeField] private float _depthBeforeSubmerged = 1f; // Depth at which the object is considered submerged.
    [SerializeField] private float _displacementAmount = 3f; // Amount of force to apply to the object when submerged.
    [SerializeField] private int _floaterCount = 4; // Number of floaters on the object.
    [SerializeField] private float _waterDrag = 0.99f; // Drag when submerged.
    [SerializeField] private float _waterAngularDrag = 0.5f; // Angular drag when submerged.
    [SerializeField] private Transform[] _floaters; // Array of floater positions.

    private WaterVolumeHelper _waterVolumeHelper;

    private void Start()
    {
        _waterVolumeHelper = WaterVolumeHelper.Instance;
    }

    private void FixedUpdate()
    {
        foreach (var floater in _floaters)
        {
            ApplyBuoyancy(floater);
        }
    }

    private void ApplyBuoyancy(Transform floater)
    {
        Vector3 position = floater.position;
        _rigidbody.AddForceAtPosition(Physics.gravity / _floaterCount, position, ForceMode.Acceleration);

        float? waveHeight = _waterVolumeHelper.GetHeight(position);
        if (waveHeight.HasValue && position.y < waveHeight.Value)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight.Value - position.y) / _depthBeforeSubmerged) * _displacementAmount;
            _rigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), position, ForceMode.Acceleration);

            _rigidbody.AddForce(_waterDrag * displacementMultiplier * Time.fixedDeltaTime * -_rigidbody.velocity, ForceMode.VelocityChange);
            _rigidbody.AddTorque(_waterAngularDrag * displacementMultiplier * Time.fixedDeltaTime * -_rigidbody.angularVelocity, ForceMode.VelocityChange);
        }
    }
}