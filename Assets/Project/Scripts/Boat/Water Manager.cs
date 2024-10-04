using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class WaterManager : MonoBehaviour
{
    MeshFilter _meshFilter;
    WaveManager _waveManager;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _waveManager = ServiceLocator.Instance.GetService<WaveManager>();
    }

    private void Update()
    {
        Vector3[] vertices = _meshFilter.mesh.vertices;

        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = _waveManager.GetWaveHeight(transform.position.x + vertices[i].x);
        }

        _meshFilter.mesh.vertices = vertices;
        _meshFilter.mesh.RecalculateNormals();
    }
}