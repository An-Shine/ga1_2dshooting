using UnityEngine;

public class Item : MonoBehaviour
{
    public enum ItemType
    {
        Heal,
        SpeedUp,
        FireRateUp
    }

    [SerializeField]
    private ItemType _type;
    [SerializeField]
    private float _value;

    public ItemType Type => _type;
    public float Value => _value;

    [SerializeField]
    private float _chaseInterval;
    private float _timer;

    [SerializeField]
    protected float _moveSpeed;
    private GameObject _player;
    private Vector2 _direction;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        _timer += Time.deltaTime;
        if (_timer >= _chaseInterval)
        {
            _timer = 0;
        }

        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        transform.Translate(direction * _moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Player player = collision.GetComponent<Player>();

        if (player == null)
        {
            Debug.Log("플레이어 태그 오브젝트에 Player 컴포넌트가 없음");
            return;
        }

        switch (Type)
        {
            case ItemType.Heal:
                player.Heal((int)(Value));
                break;

            case ItemType.SpeedUp:
                player.GetComponent<PlayerMove>().SpeedUp(Value);
                break;
            // TODO : 속성을 직접 수정하는게 아니라 메서드를 통해 수정
            case ItemType.FireRateUp:
                player.GetComponent<PlayerFire>().FireCooldown -= Value;
                break;
        }

        Destroy(gameObject);
    }
}