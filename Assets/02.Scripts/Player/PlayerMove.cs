using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고싶다

    // 매직넘버 방지 : 보는사람에 따라 의미가 달라질 수 있는 숫자 값을 매직넘버 라고함
    [SerializeField] private float _speed;
    public float Speed => _speed;
    public float MaxPositionY;
    public float MinPositionY;
    public float MaxPositionX;
    public float MinPositionX;
    public float speedCount = 1.0f;
    public float speedLimit = 1.0f;
    [SerializeField] private GameObject _trail;

    // 매 프레임마다 실행된다
    // 초당 프레임 실행 횟수 : 별다른 설정이 없을경우 가능한 많이
    private void Update()
    {
        Move();
        SpeedChange();
    }

    public float GetSpeed()
    {
        return _speed;
    }

    private void Move()
    {
        // 1. 키보드 입력을 받는다.
        float h = SimpleInput.GetAxisRaw("Horizontal");
        float v = SimpleInput.GetAxisRaw("Vertical");

        // 2. 키보드 입력에 따라 방향을 구한다.
        Vector2 normalizedDirection = new Vector2(h, v).normalized;

        // 3. 방향과 속력에 따라 이동한다.
        float finalSpeed = _speed + UpgradeManager.Instance.Upgrades[2].CurrentValue;
        Vector2 newPosition = transform.position + (Vector3)normalizedDirection * finalSpeed * Time.deltaTime;

        // 4. 위치 y에 제한이 있다.
        if (newPosition.y > MaxPositionY)
        {
            newPosition.y = MaxPositionY;
        }
        else if (newPosition.y < MinPositionY)
        {
            newPosition.y = MinPositionY;
        }

        // 5. 양 옆 끝으로 가면 반대쪽 방향으로 이동
        if (newPosition.x > MaxPositionX)
        {
            newPosition.x = MinPositionX;
        }
        else if (newPosition.x < MinPositionX)
        {
            newPosition.x = MaxPositionX;
        }

        transform.position = newPosition;
    }

    private void SpeedChange()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _speed++;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            _speed--;
        }
    }

    public void SpeedUp(float upValue)
    {
        if (upValue < 0)
        {
            Debug.LogWarning("속도 증가량은 음수일 수 없다");
            return;
        }

        _speed += upValue;
    }
}