using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damage;

    private void Update()
    {
        Vector2 direction = Vector2.up;
        transform.Translate(direction * (_moveSpeed * Time.deltaTime));
    }

    // 트리거 관련 이벤트
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("충돌했다!");

        // 본인 파괴 (총알)
        Destroy(this.gameObject);

        // 충돌한 대상이 Enemy 일때만 죽여보자
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // GetComponent<타입>() -> 게임 오브젝트가 가지고 있는 컴포넌트를 참조
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(_damage);
        }
    }
}