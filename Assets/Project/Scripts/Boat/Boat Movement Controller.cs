using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BoatMovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f; // Maximum speed of the boat movement.
    [SerializeField] private float _acceleration = 2f; // Acceleration of the boat.
    [SerializeField] private float _rotationSpeed = 100f; // Speed of the boat rotation.

    [Header("VFX")]
    [SerializeField] private ParticleSystem _boatVFX; // List of boat VFX.

    private Rigidbody _rigidbody;
    private Vector2 _input;
    private bool _isMoving;

    private InputManager _inputManager;

    private Action<InputAction.CallbackContext> _startMoveAction;
    private Action<InputAction.CallbackContext> _stopMoveAction;

    [Header("Events")]
    public UnityEvent OnBoatMovementStart;

    public UnityEvent OnBoatMovementStop;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _inputManager = ServiceLocator.Instance.GetService<InputManager>();
        _startMoveAction = _ =>
        {
            _isMoving = true;
            OnBoatMovementStart?.Invoke(); // Fire event when boat starts moving.
        };

        _stopMoveAction = _ =>
        {
            _isMoving = false;
            OnBoatMovementStop?.Invoke(); // Fire event when boat stops moving.
        };

        _inputManager.PlayerActions.Move.started += _startMoveAction;
        _inputManager.PlayerActions.Move.canceled += _stopMoveAction;
    }

    private void OnDisable()
    {
        _inputManager.PlayerActions.Move.started -= _startMoveAction;
        _inputManager.PlayerActions.Move.canceled -= _stopMoveAction;
    }

    private void OnDestroy()
    {
        _inputManager.PlayerActions.Move.started -= _startMoveAction;
        _inputManager.PlayerActions.Move.canceled -= _stopMoveAction;
    }

    private void Update()
    {
        if (_isMoving)
        {
            _input = _inputManager.PlayerActions.Move.ReadValue<Vector2>().normalized;
            Move();
            PlayVFX();
        }
        else
        {
            StopVFX();
        }
    }

    private void Move()
    {
        Vector3 targetVelocity = -_input.y * _moveSpeed * transform.forward;
        _rigidbody.velocity = Vector3.MoveTowards(_rigidbody.velocity, targetVelocity, _acceleration * Time.deltaTime);

        float targetRotation = _input.x * _rotationSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * targetRotation);
        Quaternion targetRotationQuaternion = _rigidbody.rotation * deltaRotation;

        // Smoothly interpolate between the current rotation and the target rotation
        _rigidbody.MoveRotation(Quaternion.Slerp(_rigidbody.rotation, targetRotationQuaternion, Time.deltaTime * _rotationSpeed));
    }

    private void PlayVFX() => _boatVFX.Play();

    private void StopVFX() => _boatVFX.Stop();
}