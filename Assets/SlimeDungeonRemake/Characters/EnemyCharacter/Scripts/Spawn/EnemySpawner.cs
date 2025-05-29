using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private Transform[] _points;
    [SerializeField] private AnimationCurve _difficultyCurve;

    private float _nextSpawnTime;

    private void Update()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies() 
    {
        float timeSinceStart = Time.timeSinceLevelLoad;

        float spawnInterval = _difficultyCurve.Evaluate(timeSinceStart);

        if (Time.time >= _nextSpawnTime)
        {
            SpawnEnemy();
            _nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnEnemy() 
    {
        Debug.Log($"Spawn ENEMY!!");
        int randomIndexForSpawn = Random.Range(0, _points.Length);
        int randomIndexForEnemyPrefabs = Random.Range(0, _enemyPrefabs.Length);

        var spawnPoint = _points[randomIndexForSpawn].transform.position;
       
        if (!Physics.CheckSphere(spawnPoint, 0.5f)) 
        {
            Instantiate(_enemyPrefabs[randomIndexForEnemyPrefabs], spawnPoint, Quaternion.identity);
        }
        else
        {
            Debug.Log("Spawn point is blocked!");
        }
    }
}

