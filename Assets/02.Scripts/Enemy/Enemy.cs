using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] protected float moveSpeed;
    [SerializeField] private int _collisionDamage;
    private bool _isDead;

    [Header("스폰할 아이템 데이터")]
    [SerializeField] private float _spawnRate;
    [SerializeField] private ItemSpawnDataTableSO _itemSpawnDataTable;

    [Header("죽을때 폭발이펙트 프리펩")]
    [SerializeField] private GameObject _deathEffectPrefab;

    private Animator _animator;
    private static readonly int HitHash = Animator.StringToHash("Hit");

    private EnemySound _enemySound;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _enemySound = GetComponent<EnemySound>();
    }

    private void Update()
    {
        Move();
    }

    protected abstract void Move();

    public void TakeDamage(int damage)
    {
        if (_isDead) return;
        _health -= damage;

        _enemySound.PlayDamagedSound();

        _animator.SetTrigger(HitHash);

        if (_health <= 0)
        {
            _isDead = true;
            // 충돌한 대상 파괴 (Enemy)
            _enemySound.PlayDieSound();
            SpawnDeathEffect();
            SpawnItem();

            ScoreManager scoreManager = ScoreManager.Instance;
            scoreManager.AddScore(100);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Player player = collision.gameObject.GetComponent<Player>();

        player.TakeDamage(_collisionDamage);
        Destroy(gameObject);
    }

    private void SpawnItem()
    {
        if (Random.Range(0, 100) < _spawnRate)
        {
            return;
        }

        int totalWeight = 0;
        foreach (ItemSpawnData data in _itemSpawnDataTable.Datas)
        {
            totalWeight += data.Weight;
        }

        int randomWeight = Random.Range(0, totalWeight);

        int cumulativeWeight = 0;
        foreach (ItemSpawnData data in _itemSpawnDataTable.Datas)
        {
            cumulativeWeight += data.Weight;
            if (randomWeight < cumulativeWeight)
            {
                ItemPool.Instance.GetItem(data.Type, transform.position);
                break;
            }
        }
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }
}