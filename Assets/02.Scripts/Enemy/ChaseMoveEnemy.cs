using UnityEngine;

public class ChaseMoveEnemy : Enemy
{
    private GameObject _player;
    private Vector2 _direction;
    private float _spriteRotationOffset = 90f;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if (_player == null)
        {
            Debug.LogWarning("플레이어 태그를 가진 게임 오브젝트를 찾지 못했습니다.");
            _direction = Vector2.zero;
            return;
        }

        // 1. Player의 Transform 위치 확인
        _direction = _player.transform.position - transform.position;

        float dx = _direction.x;
        float dy = _direction.y;

        float seta = Mathf.Atan2(dy, dx);
        float angle = seta * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + _spriteRotationOffset);
        _direction.Normalize();
    }


    protected override void Move()
    {
        // 2. 방향과 속도에 맞게 이동
        //transform.Translate(_direction * moveSpeed * Time.deltaTime);
        transform.position += (Vector3)_direction * moveSpeed * Time.deltaTime;
    }
}