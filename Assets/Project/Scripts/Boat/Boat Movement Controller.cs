using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoatMovementController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f; // Speed of the boat movement.
    [SerializeField] private float _rotationSpeed = 100f; // Speed of the boat rotation.

    private Rigidbody _rigidbody;
    private Vector2 _input;
    private bool _isMoving;

    private InputManager _inputManager;

    private Action<InputAction.CallbackContext> _startMoveAction;
    private Action<InputAction.CallbackContext> _stopMoveAction;

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
        };

        _stopMoveAction = _ =>
        {
            _isMoving = false;
            _rigidbody.velocity = Vector3.zero;
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
        }
    }

    private void Move()
    {
        Vector3 forwardMovement = _input.y * _moveSpeed * Time.deltaTime * transform.forward;
        Vector3 rotationMovement = _input.x * _rotationSpeed * Time.deltaTime * Vector3.up;

        _rigidbody.AddForce(forwardMovement, ForceMode.VelocityChange);
        _rigidbody.AddTorque(rotationMovement, ForceMode.VelocityChange);
    }
}