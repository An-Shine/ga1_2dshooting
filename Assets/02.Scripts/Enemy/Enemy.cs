using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] protected float moveSpeed;
    [SerializeField] private int _collisionDamage;

    [Header("스폰할 아이템 프리펩")]
    [SerializeField]
    private GameObject[] _itemPrefabs;

    [SerializeField] private float _itemSpawnRate;

    [Header("죽을때 폭발이펙트 프리펩")]
    [SerializeField]
    private GameObject _deathEffectPrefab;

    private Animator _animator;
    private static readonly int HitHash = Animator.StringToHash("Hit");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        Move();
    }

    protected abstract void Move();


    public void TakeDamage(int damage)
    {
        _health -= damage;

        _animator.SetTrigger(HitHash);

        if (_health <= 0)
        {
            // 충돌한 대상 파괴 (Enemy)
            SpawnDeathEffect();
            Destroy(gameObject);
            SpawnItem();
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
            Instantiate(_itemPrefabs[Random.Range(0, _itemPrefabs.Length)], transform.position, transform.rotation);
        }
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }
}