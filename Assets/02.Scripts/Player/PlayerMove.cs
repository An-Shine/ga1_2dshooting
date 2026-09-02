using UnityEngine;
using UnityEngine.InputSystem.Layouts;

public class PlayerMove : MonoBehaviour
{
    // 목적 : 키보드 입력에 따라서 플레이어 이동 처리를 하고싶다
    
    // 매직넘버 방지 : 보는사람에 따라 의미가 달라질 수 있는 숫자 값을 매직넘버 라고함
    public float Speed;
    
    // 매 프레임마다 실행된다
    // 초당 프레임 실행 횟수 : 별다른 설정이 없을경우 가능한 많이
    private void Update()
    {
        // 1. 키보드 입력을 받는다 (GetAxis / GetAxisRaw)
        float h = Input.GetAxisRaw("Horizontal");  // 키보드 왼/오른쪽 입력 상태에 따라 -1f ~ 0 ~ 1f 를 반환
        float v = Input.GetAxisRaw("Vertical");    // 키보드 위/아래 입력 상태에 따라 -1f ~ 0 ~ 1f 를 반환
        
        Debug.Log($"h:{h}, v:{v}");
        
        // 2. 키보드 입력에 따라 방향을 구한다
        // 게임에는 벡터 라는 타입이 있다. 벡터는 크기와 방향을 의미한다
        
        Vector2 direction = new Vector2(h, v);
        
        // 3. 방향과 속력에 따라 이동한다
        // 속도 : 방향 * 속력

        Vector2 normalizedspeed = (direction * Speed).normalized; // 벡터의 길이를 1로 만들어주는것 -> 방향만 유지한다
        transform.Translate(direction * Speed * Time.deltaTime);
            
        //deltaTime : 이전 프레임으로부터 지금 프레임까지 시간이 얼마나 지났는지 ms 단위로 반환
        
        // 새로운 위치 : 현재위치 + (방향 * 속력 * 시간)
        //transform.position += (Vector3)direction * Speed * Time.deltaTime;
        
        // 실습과제 1번
        if (transform.position.y > -0.6f)
        {
            transform.position = new Vector2(transform.position.x, -0.6f);
        }
        else if (transform.position.y < -4.5f)
        {
            transform.position = new Vector2(transform.position.x, -4.5f);
        }
        
        // 실습과제 2번
        if (transform.position.x > 3.0f)
        {
            transform.position = new Vector2(-4.5f, transform.position.y);
        }
        
        else if (transform.position.x < -4.5f)
        {
            transform.position = new Vector2(3.0f, transform.position.y);
        }
        
        

    }
}
