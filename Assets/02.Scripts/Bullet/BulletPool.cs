using UnityEngine;

public class BulletPool : MonoBehaviour
{
    private static BulletPool _instance = null;
    public static BulletPool Instance => _instance;
    // 오브젝트 풀링이란: Pool(웅덩이, 창고)
    // 그 창고 안에 게임 오브젝트를 미리 필요한만큼 만들어두고
    // 필요할때 마다 꺼내서 사용하고 필요없으면 반환하는 식 (활성화/비활성화)
    // 메모리 할당과(객체의 생성) 해제(파괴)를 최소화해서 성능 향상

    // 필요속성
    [Header("총알 프리펩들")]
    [SerializeField] private Bullet[] _bulletPrefabs;

    [Header("풀 사이즈")]
    [SerializeField] private int _bulletPoolSize;
    [SerializeField] private int _subBulletPoolSize;

    // 생성한 총알을 담아둘 풀
    private Bullet[,] _bulletPool;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        // 창고를 창고 크기만큼 만든다
        _bulletPool = new Bullet[_bulletPrefabs.Length, _bulletPoolSize];

        // 총알 프리펩 종류와 창고 크기만큼 총알을 미리 만들어서 집어넣는다

        for (int i = 0; i < _bulletPrefabs.Length; i++)
        {
            Bullet bulletPrefab = _bulletPrefabs[i];

            for (int j = 0; j < _bulletPoolSize; j++)
            {
                Bullet bullet = Instantiate(bulletPrefab, gameObject.transform); // transform 을 넣어주면 해당 오브젝트의 하위에 생성됨
                bullet.gameObject.SetActive(false); // 딩징 사용할거 아니니까 비활성화
                _bulletPool[i, j] = bullet;
            }
        }
    }

    public Bullet GetBullet(BulletType bulletType)
    {
        for (int i = 0; i < _bulletPool.Length; i++) // 타입별로 순회하면서
        {
            if (_bulletPool[i, 0].Type != bulletType) // 첫번쨰 요소의 타입이 내가 원하는게 아니라면 스킵
            {
                continue;
            }

            for (int j = 0; j < _bulletPoolSize; j++) // 원하는 타입의 배열 순회
            {
                Bullet bullet = _bulletPool[i, j];
                // 비활성화 되어있는 (누가 빌려가지 않은 총알 반환)
                if (bullet.gameObject.activeSelf == false)
                {
                    bullet.gameObject.SetActive(true);
                    bullet.PlaySound();
                    return bullet;
                }
            }
        }

        return null;
    }
}