using UnityEngine;

public class Player : MonoBehaviour
{
    // 캡슐화
    // 메서드를 통한 상태변경
    // 데이터 은닉
    [SerializeField]
    private int _health = 100;

    public int Health => _health; // 람다식 문법을 활용한 읽기 전용 프로퍼티
    // 잘 설계된 클래스는
    // - 필드 (인스턴스 변수)
    // - 필드에 잘못된 값이 할당되지 않게 막고, 정상적으로 동작하는 메서드
    // getter / setter : 특정 데이터를 get/set 해주는 메서드

    [Header("죽을때 폭발이펙트 프리펩")]
    [SerializeField]
    private GameObject _deathEffectPrefab;

    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            Debug.LogWarning("데미지는 음수일 수 없습니다");
            return;
        }

        _health -= damage;
        if (_health <= 0f)
        {
            SpawnDeathEffect();
            Destroy(gameObject);
        }
    }

    public void Heal(int healAmount)
    {
        if (healAmount < 0)
        {
            Debug.LogWarning("힐량은 음수일수 없습니다");
            return;
        }

        _health += healAmount;
    }

    private void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }
}