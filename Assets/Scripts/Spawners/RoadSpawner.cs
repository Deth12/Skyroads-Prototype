using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTreshold = 60f;
    private float tileLength = 0f;
    // References
    private Transform spawnPoint;
    private Transform player;
    private Transform lastSpawnedTile;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        InitializeSpawner();
    }

    private void InitializeSpawner()
    {
        GameObject tile = PoolManager.Instance.GetObject("Road", Vector3.zero, Quaternion.identity);
        tileLength = tile.GetComponent<MeshFilter>().mesh.bounds.extents.z * tile.transform.localScale.z;
        lastSpawnedTile = tile.transform;
    }

    private void SpawnTile(float zOffset)
    {
        Vector3 spawnPos = new Vector3(0f, 0f, zOffset);
        lastSpawnedTile = PoolManager.Instance.GetObject("Road", spawnPos, Quaternion.identity).transform;
    }
    
    private void Update()
    {
        if(lastSpawnedTile.position.z - player.position.z < spawnTreshold)
            SpawnTile(lastSpawnedTile.transform.position.z + tileLength * 2);
    }
}
