using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TrajectoryPredictor))]
public class ProjectileThrow : MonoBehaviour
{
    private TrajectoryPredictor trajectoryPredictor;

    [SerializeField]
    private Rigidbody objectToThrow;

    [SerializeField, Range(0.0f, 50.0f)]
    private float force;

    [SerializeField]
    private Transform StartPosition;

    private InputManager _inputManager;
    private Camera _camera;

    private void OnEnable()
    {
        trajectoryPredictor = GetComponent<TrajectoryPredictor>();

        if (StartPosition == null)
            StartPosition = transform;

        _inputManager = ServiceLocator.Instance.GetService<InputManager>();
        _inputManager.PlayerActions.Fire.performed += ThrowObject;

        _camera = Camera.main;
    }

    private void Update()
    {
        AdjustForceBasedOnMouseDistance();
        Predict();
    }

    private void AdjustForceBasedOnMouseDistance()
    {
        Vector3 mousePosition = Mouse.current.position.ReadValue();
        Vector3 worldMousePosition = _camera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _camera.nearClipPlane));
        float distance = Vector3.Distance(worldMousePosition, StartPosition.position);

        // Adjust the force based on the distance, reduce the maximum force
        force = Mathf.Clamp(distance * 0.5f, 0.0f, 50.0f); // Reduced the maximum force to 25.0f
    }

    private void Predict()
    {
        trajectoryPredictor.PredictTrajectory(ProjectileData());
    }

    private ProjectileProperties ProjectileData()
    {
        ProjectileProperties properties = new();
        Rigidbody r = objectToThrow;

        properties.direction = StartPosition.forward;
        properties.initialPosition = StartPosition.position;
        properties.initialSpeed = force;
        properties.mass = r.mass;
        properties.drag = r.drag;

        return properties;
    }

    private void ThrowObject(InputAction.CallbackContext ctx)
    {
        if (trajectoryPredictor.IsTrajectoryVisible())
        {
            Rigidbody thrownObject = Instantiate(objectToThrow, StartPosition.position, Quaternion.identity);
            thrownObject.AddForce(StartPosition.forward * force, ForceMode.Impulse);
        }
    }
}