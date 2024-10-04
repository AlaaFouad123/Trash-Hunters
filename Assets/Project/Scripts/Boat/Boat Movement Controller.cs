using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BoatMovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f; // Speed of the boat movement.
    [SerializeField] private float _rotationSpeed = 100f; // Speed of the boat rotation.
    [SerializeField] private float _drag = 0.1f; // Drag to simulate water resistance.

    [Header("VFX")]
    [SerializeField] private List<ParticleSystem> _boatVFX; // List of boat VFX.

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
        _rigidbody.drag = _drag; // Apply drag to the rigidbody.
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
        _rigidbody.velocity = Vector3.Lerp(_rigidbody.velocity, targetVelocity, Time.deltaTime * _moveSpeed);

        float targetRotation = _input.x * _rotationSpeed * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(Vector3.up * targetRotation);
        _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
    }

    private void PlayVFX()
    {
        foreach (var vfx in _boatVFX)
        {
            if (!vfx.isPlaying)
            {
                vfx.Play();
            }
        }
    }

    private void StopVFX()
    {
        foreach (var vfx in _boatVFX)
        {
            if (vfx.isPlaying)
            {
                vfx.Stop();
            }
        }
    }
}