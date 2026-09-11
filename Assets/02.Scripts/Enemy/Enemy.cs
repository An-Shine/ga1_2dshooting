using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] protected float moveSpeed;
    [SerializeField] private int _collisionDamage;

    [Header("스폰할 아이템 프리펩")]
    [SerializeField] private GameObject[] _itemPrefabs;

    [SerializeField] private float _itemSpawnRate;

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
        _health -= damage;

        _enemySound.PlayDamagedSound();

        _animator.SetTrigger(HitHash);

        if (_health <= 0)
        {
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
        if (Random.Range(0, 100) < _itemSpawnRate)
        {
            // TODO : Scriptable Object 를 사용해서 리팩토링
            // 1. 배열을 사용했지만 각 아이템이 어떤 프리펩인지 알수가없음
            // 2. 각 에너미 스폰 확률을 매직넘버로 하드코딩해서 유지보수가 어려움

            Instantiate(_itemPrefabs[Random.Range(0, _itemPrefabs.Length)], transform.position, transform.rotation);
        }
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }
}