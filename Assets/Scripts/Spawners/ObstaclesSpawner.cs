using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    private Transform player;
    
    [Header("Spawner Settings")]
    [SerializeField] private float spawnDistanceFromPlayer = 100f;
    [SerializeField] private float spawnHeight = 2f;
    [Range(1f, 3f)]
    [SerializeField] private float spawnDelay = 3f;

    private Transform spawnPoint;
    private float timeToSpawn = 0f;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        // Increases game difficulty every 20s
        GameManager.Instance.OnGameStart += 
            () => { InvokeRepeating("DecreaseSpawnDelay", 0f, 20f); };
        InitializeSpawner();
    }

    private void InitializeSpawner()
    {
        if (player == null)
            return;
        spawnPoint = new GameObject {name = "Spawn Point"}.transform;
        spawnPoint.position = 
            new Vector3(0f, spawnHeight, player.transform.position.z + spawnDistanceFromPlayer);
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameStarted)
            Tick();
    }

    private void Tick()
    {
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn <= 0)
        {
            SpawnObstacle();
            timeToSpawn = spawnDelay;
        }  
    }
    
    private void DecreaseSpawnDelay()
    {
        if (spawnDelay > 0.5f)
            spawnDelay -= .05f;
    }

    private void SpawnObstacle()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-3f, 3f), spawnHeight, spawnPoint.transform.position.z);
        PoolManager.Instance.GetObject("Obstacle", spawnPos, Quaternion.identity);
    }
}
