using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    private void Update()
    {   
        transform.Translate(Vector2.up * (Speed * Time.deltaTime));
    }
}
