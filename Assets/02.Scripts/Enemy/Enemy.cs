using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _health;

    private void Update()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * (_moveSpeed * Time.deltaTime));
    }

    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0)
        {
            // 충돌한 대상 파괴 (Enemy)
            Destroy(gameObject);
        }
    }
}