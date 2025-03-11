using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RTSGameManager : MonoBehaviour {
    [Header("Параметры карты")]
    public int mapSize = 50;
    public float noiseScale = 10f;
    public float heightMultiplier = 5f;
    public float waterLevel = 1.5f;

    [Header("Объекты")]
    public GameObject waterPrefab;
    public GameObject treePrefab;
    public GameObject rockPrefab;
    public GameObject playerBasePrefab;
    public GameObject enemyBasePrefab;
    public int enemyCount = 3;
    public float safeZoneRadius = 10f;

    [Header("Сохранение игры")]
    public float autoSaveInterval = 30f;
    public Button saveButton;
    public Button loadButton;
    private string savePath;

    [Header("UI и сцены")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject gameSettingsPanel;
    public GameObject victoryScreen;
    public GameObject defeatScreen;
    public Button playButton;
    public Button settingsButton;
    public Button exitButton;
    public Button startGameButton;
    
    private MeshFilter meshFilter;
    private MeshCollider meshCollider;
    private List<Vector3> terrainVertices = new List<Vector3>();

    void Start() {
        savePath = Application.persistentDataPath + "/savegame.json";
        meshFilter = GetComponent<MeshFilter>();
        meshCollider = GetComponent<MeshCollider>();

        // Привязка кнопок
        playButton.onClick.AddListener(OpenGameSettings);
        settingsButton.onClick.AddListener(OpenSettings);
        exitButton.onClick.AddListener(QuitGame);
        startGameButton.onClick.AddListener(StartGame);

        GenerateTerrain();
        PlaceWater();
        GenerateResources();
        PlaceBases();

        if (saveButton != null) saveButton.onClick.AddListener(SaveGame);
        if (loadButton != null) loadButton.onClick.AddListener(LoadGame);
        
        LoadGame();
        InvokeRepeating("AutoSave", autoSaveInterval, autoSaveInterval);
    }

    // **Генерация карты**
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

    void PlaceWater() {
        foreach (Vector3 pos in terrainVertices) {
            if (pos.y < waterLevel) {
                Instantiate(waterPrefab, pos, Quaternion.identity);
            }
        }
    }

    void GenerateResources() {
        GameObject resourceParent = new GameObject("Resources");

        foreach (Vector3 pos in terrainVertices) {
            float chance = Random.value;
            if (chance < 0.1f) {
                Instantiate(treePrefab, pos + Vector3.up, Quaternion.identity, resourceParent.transform);
            } else if (chance < 0.05f) {
                Instantiate(rockPrefab, pos + Vector3.up, Quaternion.identity, resourceParent.transform);
            }
        }
    }

    void PlaceBases() {
        Vector3 playerBasePos = GetValidBasePosition();
        Instantiate(playerBasePrefab, playerBasePos, Quaternion.identity);

        for (int i = 0; i < enemyCount; i++) {
            Vector3 enemyBasePos = GetValidBasePosition();
            Instantiate(enemyBasePrefab, enemyBasePos, Quaternion.identity);
        }
    }

    Vector3 GetValidBasePosition() {
        Vector3 pos;
        do {
            pos = new Vector3(Random.Range(5, mapSize - 5), 0, Random.Range(5, mapSize - 5));
        } while (Vector3.Distance(pos, Vector3.zero) < safeZoneRadius);
        return pos;
    }

    // **Автосохранение**
    void AutoSave() {
        SaveGame();
    }

    public void SaveGame() {
        // Здесь код сохранения игры в JSON
    }

    public void LoadGame() {
        // Здесь код загрузки игры
    }

    // **Экран победы/поражения**
    public void ShowVictoryScreen() {
        victoryScreen.SetActive(true);
    }

    public void ShowDefeatScreen() {
        defeatScreen.SetActive(true);
    }

    // **Кнопки меню**
    public void OpenGameSettings() {
        mainMenuPanel.SetActive(false);
        gameSettingsPanel.SetActive(true);
    }

    public void OpenSettings() {
        settingsPanel.SetActive(true);
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void StartGame() {
        SceneManager.LoadScene("GameScene"); // Загружает игровую сцену после настроек
    }
}
