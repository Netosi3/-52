using UnityEngine;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Terrain Parameters")]
    public int mapSize = 50;
    public float noiseScale = 10f;
    public float heightMultiplier = 5f;
    public float waterLevel = 1.5f;
    public GameObject waterPrefab;
    
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    private List<Vector3> terrainVertices = new List<Vector3>();

    void Start() {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();
        GenerateTerrain();
        PlaceWater();
    }

    void GenerateTerrain() { /* исходный код */ }
    void PlaceWater() { /* исходный код */ }
}
