using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputSystem _input;
    internal InputSystem.PlayerActions PlayerActions { get; private set; }

    private ServiceLocator _serviceLocator;

    private void Awake()
    {
        _serviceLocator = ServiceLocator.Instance;
        _serviceLocator.RegisterService(this, true);

        _input = new InputSystem();

        // Initialize actions
        InitializeActions();
    }

    private void OnEnable() => _input.Enable();

    private void OnDisable() => _input?.Disable();

    private void InitializeActions()
    {
        PlayerActions = _input.Player;
    }
}