using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed;
    [SerializeField] private int _health;
    public int Test = 1;

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
}