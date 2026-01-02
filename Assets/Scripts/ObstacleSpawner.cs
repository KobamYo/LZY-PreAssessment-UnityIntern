using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public float spawnZ = 50f;
    public float minX = -30f;
    public float maxX = 20f;

    public float spawnInterval = 1.5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 1f, spawnInterval);
    }

    void SpawnObstacle()
    {
        float x = Random.Range(minX, maxX);
        Vector3 pos = new Vector3(x, 0.5f, spawnZ);
        Instantiate(obstaclePrefab, pos, Quaternion.identity);
    }
}
