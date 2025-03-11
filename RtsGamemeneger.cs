using UnityEngine;

public class RTSGameManager : MonoBehaviour
{
    [Header("References")]
    public TerrainGenerator terrainGenerator;
    public ResourceGenerator resourceGenerator;
    public BasePlacer basePlacer;
    public SaveLoadManager saveLoadManager;

    void Start() {
        terrainGenerator.GenerateTerrain();
        resourceGenerator.GenerateResources(terrainGenerator.terrainVertices);
        basePlacer.PlaceBases();
        saveLoadManager.LoadGame();
    }
}
