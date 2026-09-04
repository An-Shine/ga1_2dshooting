using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 간격")][SerializeField] private float _spawnInterval = 3f;
    private float _timer;

    // -생성할 프리펩
    [Header("스폰할 적 프리펩")][SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private int[] _spawnRate;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            _timer = 0;
            _spawnInterval = UnityEngine.Random.Range(1f, 3f);
            Spawn();
        }
    }

    private void Spawn()
    {
        int randomRate = Random.Range(0, 100);
        int spawnCount = 0;

        for (int i = 0; i < _enemyPrefabs.Length; i++)
        {
            spawnCount += _spawnRate[i];

            if (randomRate < spawnCount)
            {
                GameObject enemy = Instantiate(_enemyPrefabs[i]);
                enemy.transform.position = transform.position;
                return;
            }
        }
    }
}