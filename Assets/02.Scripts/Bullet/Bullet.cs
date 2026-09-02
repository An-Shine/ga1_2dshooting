using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    private void Update()
    {
        Vector2 direction = Vector2.up;
        transform.Translate(direction * (Speed * Time.deltaTime));
    }
}
