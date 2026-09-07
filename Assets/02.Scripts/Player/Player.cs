using UnityEditor.Rendering;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int _maxHealth;
    public int _currentHealth;

    public void Start()
    {
        _maxHealth = 100;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
        {
            _currentHealth = 0;
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

        _currentHealth += healAmount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
    }
}