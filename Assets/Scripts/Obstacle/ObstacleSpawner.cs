using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private float spawnZ = 50f;
    [SerializeField] private float minX = -30f;
    [SerializeField] private float maxX = 20f;
    [SerializeField] private float spawnInterval = 1.5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnInterval);
    }

    void SpawnObstacle()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver())
            return;
            
        float x = Random.Range(minX, maxX);
        Vector3 pos = new Vector3(x, 0.5f, spawnZ);
        Instantiate(obstaclePrefab, pos, Quaternion.identity);
    }
}
