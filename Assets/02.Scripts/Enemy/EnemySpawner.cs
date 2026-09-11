using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("스폰 간격")]
    [SerializeField] private float _spawnInterval = 3f;
    private float _timer;

    // -생성할 프리펩
    [Header("스폰할 적 데이터")]
    [SerializeField] private EnemySpawnDataTableSO _spawnDataTable;

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            _timer = 0;
            _spawnInterval = Random.Range(1f, 3f);
            Spawn();
        }
    }

    private void Spawn()
    {
        // 가중치 랜덤 선택
        // 각 아이템에 가중치를 부여하고, 가중치가  클수록 높은 확률로 선택되도록 하는 방식

        // 1. 추첨할수있는 모든 가중치를 더한다
        int totalWeight = 0;
        foreach (EnemySpawnData data in _spawnDataTable.Datas)
        {
            totalWeight += data.Weight;
        }

        // 2. 전체 가중치 범위에서 랜덤한 정수를 뽑는다.
        int randomWeight = Random.Range(0, totalWeight);

        // 3. 가중치를 누적하면서 선택된 구간을 찾는다.
        int cumulativeWeight = 0;
        foreach (EnemySpawnData data in _spawnDataTable.Datas)
        {
            cumulativeWeight += data.Weight; // 누적
            if (randomWeight < cumulativeWeight) // 구간
            {
                GameObject enemy = Instantiate(data.EnemyPrefab);
                enemy.transform.position = transform.position;
                break;
            }
        }
    }
}