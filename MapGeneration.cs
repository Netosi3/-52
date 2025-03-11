using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {
    [Header("Параметры карты")]
    public int mapSize = 50;
    public float noiseScale = 10f;
    public float heightMultiplier = 5f;
    public float waterLevel = 1.5f; // Уровень воды для рек и озёр

    [Header("Объекты")]
    public GameObject waterPrefab;
    public GameObject playerBasePrefab;
    public GameObject enemyBasePrefab;
    public int enemyCount = 3;
    public float safeZoneRadius = 10f;

    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    private List<Vector3> terrainVertices = new List<Vector3>();

    void Start() {
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        GenerateTerrain();
        PlaceWater();
        PlaceBases();
    }

    // **1. Генерация рельефа**
    void GenerateTerrain() {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(mapSize + 1) * (mapSize + 1)];
        int[] triangles = new int[mapSize * mapSize * 6];

        for (int z = 0, i = 0; z <= mapSize; z++) {
            for (int x = 0; x <= mapSize; x++, i++) {
                float y = Mathf.PerlinNoise(x / noiseScale, z / noiseScale) * heightMultiplier;
                vertices[i] = new Vector3(x, y, z);
                terrainVertices.Add(vertices[i]);
            }
        }

        for (int z = 0, ti = 0, vi = 0; z < mapSize; z++, vi++) {
            for (int x = 0; x < mapSize; x++, ti += 6, vi++) {
                triangles[ti] = vi;
                triangles[ti + 1] = vi + mapSize + 1;
                triangles[ti + 2] = vi + 1;
                triangles[ti + 3] = vi + 1;
                triangles[ti + 4] = vi + mapSize + 1;
                triangles[ti + 5] = vi + mapSize + 2;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        meshCollider.sharedMesh = mesh;
    }

    // **2. Размещение воды (реки и озёра)**
    void PlaceWater() {
        foreach (Vector3 pos in terrainVertices) {
            if (pos.y < waterLevel) { // Вода появляется там, где высота ниже порога
                Instantiate(waterPrefab, pos, Quaternion.identity);
            }
        }
    }

    // **3. Размещение баз игрока и врагов**
    void PlaceBases() {
        Vector3 playerBasePos = GetValidBasePosition();
        Instantiate(playerBasePrefab, playerBasePos, Quaternion.identity);

        for (int i = 0; i < enemyCount; i++) {
            Vector3 enemyBasePos = GetValidBasePosition();
            Instantiate(enemyBasePrefab, enemyBasePos, Quaternion.identity);
        }
    }

    // **Функция поиска свободного места для базы**
    Vector3 GetValidBasePosition() {
        Vector3 pos;
        do {
            pos = new Vector3(Random.Range(5, mapSize - 5), 0, Random.Range(5, mapSize - 5));
        } while (Vector3.Distance(pos, Vector3.zero) < safeZoneRadius);
        return pos;
    }
}
