using UnityEngine;

public class DestroyAfterCollisionOrTime : MonoBehaviour
{
    [SerializeField]
    private float timeToDestroy = 5.0f;

    private void Start()
    {
        // Destroy the object after a specified time
        Destroy(gameObject, timeToDestroy);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Destroy the object upon collision
        Destroy(gameObject);
    }
}
