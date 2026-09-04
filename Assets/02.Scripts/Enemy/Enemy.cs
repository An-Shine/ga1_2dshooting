using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] private int _collisionDamage;


    private void Update()
    {
        Move();
    }

    protected abstract void Move();


    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            // 충돌한 대상 파괴 (Enemy)
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            Destroy(gameObject);
            playerHealth.TakeDamage(_collisionDamage);
        }
    }
}