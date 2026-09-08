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
    [SerializeField]
    private float _chaseInterval = 2f;
    private float _waitTimer;

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
        _waitTimer += Time.deltaTime;
        if (_waitTimer >= _chaseInterval)
        {
            ChasePlayer();
        }
    }

    private void ChasePlayer()
    {
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

        switch (_type)
        {
            case ItemType.Heal:
                player.Heal((int)(_value));
                Debug.Log($"플레이어 체력: {player.Health})");
                break;

            case ItemType.SpeedUp:
                PlayerMove playerMove = player.GetComponent<PlayerMove>();
                playerMove.SpeedUp(_value);
                Debug.Log($"플레이어 이동속도: {playerMove.Speed}");
                break;
            // TODO : 속성을 직접 수정하는게 아니라 메서드를 통해 수정
            case ItemType.FireRateUp:
                PlayerFire playerFire = player.GetComponent<PlayerFire>();
                playerFire.FireRateUp(_value);
                Debug.Log($"플레이어 공격속도: {playerFire.FireCooldown}");
                break;
        }

        Destroy(gameObject);
    }
}