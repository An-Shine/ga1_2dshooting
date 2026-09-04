using UnityEngine;

public class ChaseEnemy : Enemy
{
    private GameObject _player;
    private Vector2 _direction;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    protected override void Move()
    {
        if (_player == null) return; // 방어 코드

        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }
}