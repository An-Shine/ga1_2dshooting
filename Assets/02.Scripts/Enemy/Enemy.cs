using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;
    public float Health;
    public GameObject EnemyPrefab;
    public Transform EnemySpawnPoint;

    private void Update()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * (Speed * Time.deltaTime));
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(EnemyPrefab);
        enemy.transform.position = EnemySpawnPoint.position;
    }
}