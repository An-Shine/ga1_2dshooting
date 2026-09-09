using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 캡슐화
    // 메서드를 통한 상태변경
    // 데이터 은닉
    [SerializeField] private int _health = 100;
    [SerializeField] private int _maxHealth = 100;
    private PlayerEffect _playerEffect;
    private Animator _animator;
    private static readonly int IsDieHash = Animator.StringToHash("IsDie");
    private PlayerSound _playerSound;


    public int Health => _health; // 람다식 문법을 활용한 읽기 전용 프로퍼티

    // 잘 설계된 클래스는
    // - 필드 (인스턴스 변수)
    // - 필드에 잘못된 값이 할당되지 않게 막고, 정상적으로 동작하는 메서드
    // getter / setter : 특정 데이터를 get/set 해주는 메서드
    private void Awake()
    {
        _playerEffect = GetComponent<PlayerEffect>();
        _animator = GetComponent<Animator>();
        _playerSound = GetComponent<PlayerSound>();
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogWarning("데미지는 음수일 수 없습니다");
            return;
        }

        _playerSound.PlayDamagedSound();
        _health -= damage;
        if (_health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        // 죽음 애니메이션 재생
        _animator.SetTrigger(IsDieHash);

        // 사망 이펙트
        // _playerEffect.SpawnDeathEffect();

        _playerSound.PlayDieSound();
    }

    public void OnDeathAnimationEnd()
    {
        Time.timeScale = 0f;
    }

    public void Heal(int healAmount)
    {
        if (healAmount < 0)
        {
            Debug.LogWarning("힐량은 음수일수 없습니다");
            return;
        }

        _health += healAmount;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            _playerSound.PlayItemGetSound();
        }
    }
}