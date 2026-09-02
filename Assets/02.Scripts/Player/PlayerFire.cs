using UnityEngine;

public class PlayerFire : MonoBehaviour
{   
    
    // 목표 : 스페이스바를 누를때 마다 총알을 생성해서 발사하기
    // 필요 속성 : 총알 프리펩, 생성위치(발사지점)

    public GameObject BulletPrefab;
    public Transform FirePoint;
    
    private void Update()
    {
        Fire();
    }

    private void Fire()
    {
        // 1. 스페이스바를 누르면 
        if (Input.GetKeyDown(KeyCode.Space))
        {   
            // 2. 총알 프리펩을 생성한다
            GameObject bullet = Instantiate(BulletPrefab);
            bullet.transform.position = FirePoint.position;
            
        }
    }
    
    
}
