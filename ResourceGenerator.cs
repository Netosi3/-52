using UnityEngine;
using System.Collections.Generic;

public class ResourceGenerator : MonoBehaviour
{
    [Header("Resources")]
    public GameObject treePrefab;
    public GameObject rockPrefab;
    
    public void GenerateResources(List<Vector3> terrainVertices) {
        GameObject resourceParent = new GameObject("Resources");
        foreach (Vector3 pos in terrainVertices) {
            /* исходный код */
        }
    }
}
