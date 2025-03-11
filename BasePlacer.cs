using UnityEngine;

public class BasePlacer : MonoBehaviour
{
    [Header("Base Placement")]
    public GameObject playerBasePrefab;
    public GameObject enemyBasePrefab;
    public int enemyCount = 3;
    public float safeZoneRadius = 10f;
    public int mapSize = 50;

    public void PlaceBases() {
        Vector3 playerBasePos = GetValidBasePosition();
        Instantiate(playerBasePrefab, playerBasePos, Quaternion.identity);
        
        for (int i = 0; i < enemyCount; i++) {
            /* исходный код */
        }
    }

    Vector3 GetValidBasePosition() { /* исходный код */ }
}
