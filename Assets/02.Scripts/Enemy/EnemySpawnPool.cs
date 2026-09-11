using UnityEngine;

public class EnemySpawnPool : MonoBehaviour
{
    private static EnemySpawnPool _instance;
    public static EnemySpawnPool Instance => _instance;

    [Header("적 프리펩")]
    [SerializeField] private Enemy[] _enemyPrefabs;

    [Header("풀 사이즈")]
    [SerializeField] private int _enemyPoolSize;

    private Enemy[] _enemyPool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        _enemyPool = new Enemy[_enemyPrefabs.Length * _enemyPoolSize];

        int poolIndex = 0;

        foreach (Enemy enemyPrefab in _enemyPrefabs)
        {
            for (int i = 0; i < _enemyPoolSize; i++)
            {
                Enemy enemy = Instantiate(enemyPrefab, transform);
                enemy.gameObject.SetActive(false);
                _enemyPool[poolIndex] = enemy;
                poolIndex++;
            }
        }
    }

    public Enemy GetEnemy(EnemyType type, Vector3 spawnPosition)
    {
        foreach (Enemy enemy in _enemyPool)
        {
            if (enemy.Type != type)
            {
                continue;
            }

            if (enemy.gameObject.activeSelf)
            {
                continue;
            }

            enemy.transform.position = spawnPosition;
            enemy.gameObject.SetActive(true);

            return enemy;
        }

        return null;
    }
}