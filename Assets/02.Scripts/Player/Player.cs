using UnityEngine;

public class Player : MonoBehaviour
{
    public float _maxHealth;
    public float _currentHealth;

    public void Start()
    {
        _maxHealth = 100f;
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0f)
        {
            _currentHealth = 0f;
            Destroy(gameObject);
        }
    }
}