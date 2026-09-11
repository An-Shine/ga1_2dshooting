using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private BulletType _type;
    public BulletType Type => _type;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // 활성화 될때 마다 호출되는 이벤트 함수
    public void PlaySound()
    {
        _audioSource.pitch = UnityEngine.Random.Range(1f, 3f);
        _audioSource.Play();
    }

    private void Update()
    {
        Vector2 direction = Vector2.up;
        transform.Translate(direction * (_moveSpeed * Time.deltaTime));
        if (transform.position.y > 5)
        {
            gameObject.SetActive(false);
        }
    }

    // 트리거 관련 이벤트
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Debug.Log("충돌했다!");

        // 본인 파괴 (총알)

        // 충돌한 대상이 Enemy 일때만 죽여보자
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // GetComponent<타입>() -> 게임 오브젝트가 가지고 있는 컴포넌트를 참조
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(_damage);
            gameObject.SetActive(false); // 비활성화
        }
    }

    private void OnSpawn()
    {
        // 프리펩이 풀에의해서 활성화 될때마다
        // 초기화 하는 코드들이 들어간다
        PlaySound();
    }
}