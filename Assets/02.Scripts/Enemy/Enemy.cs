using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed;

    private void Update()
    {
        Vector2 direction = Vector2.down;
        transform.Translate(direction * (Speed * Time.deltaTime));
    }
}