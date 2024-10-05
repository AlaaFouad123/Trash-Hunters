using UnityEngine;

public class EnableCursor : MonoBehaviour
{
    [SerializeField] private bool _enableCursor = true;

    private void Start()
    {
        Cursor.visible = _enableCursor;
        Cursor.lockState = _enableCursor ? CursorLockMode.None : CursorLockMode.Locked;
    }
}