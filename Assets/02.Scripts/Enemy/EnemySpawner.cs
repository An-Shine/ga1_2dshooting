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

        // TODO : Scriptable Object 를 사용해서 리팩토링
        // 1. 배열을 사용했지만 각 아이템이 어떤 프리펩인지 알수가없음
        // 2. 각 에너미 스폰 확률을 매직넘버로 하드코딩해서 유지보수가 어려움

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