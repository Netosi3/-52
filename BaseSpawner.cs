using UnityEngine;

public class BaseSpawner : MonoBehaviour {
    public GameObject playerBasePrefab;
    public GameObject enemyBasePrefab;
    public int enemyCount = 3;
    public float safeZoneRadius = 10f; // Зона без ресурсов вокруг базы

    void Start() {
        Vector3 playerBasePos = new Vector3(Random.Range(5, 45), 0, Random.Range(5, 45));
        Instantiate(playerBasePrefab, playerBasePos, Quaternion.identity);

        for (int i = 0; i < enemyCount; i++) {
            Vector3 enemyBasePos = GetValidPosition();
            Instantiate(enemyBasePrefab, enemyBasePos, Quaternion.identity);
        }
    }

    Vector3 GetValidPosition() {
        Vector3 pos;
        do {
            pos = new Vector3(Random.Range(5, 45), 0, Random.Range(5, 45));
        } while (Vector3.Distance(pos, Vector3.zero) < safeZoneRadius); // Проверка, чтобы базы не появлялись рядом
        return pos;
    }
}
