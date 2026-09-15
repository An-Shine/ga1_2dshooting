using System;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 목표 : 스페이스바를 누를때 마다 총알을 생성해서 발사하기
    // 필요 속성 : 총알 프리펩, 생성위치(발사지점)

    public GameObject BulletPrefab;
    public GameObject SubBulletPrefabs;
    public Transform[] FirePoints;

    public Transform[] SubFirePoints;

    //public Transform LeftFirePoint;
    //public Transform RightFirePoint;
    [SerializeField] private GameObject _fireSprite;

    [SerializeField] private float _fireCooldown = 1.0f;

    public float FireCooldown => _fireCooldown;
    private const float MinCoolTime = 0.06f;
    public float CurrentCooldown;

    public bool isAutoFire = false;

    public void SetAuto(bool auto)
    {
        isAutoFire = auto;
    }

    private void Start()
    {
        CurrentCooldown = _fireCooldown;
    }

    private void Update()
    {
        // 1번 눌러서 자동발사 모드 설정 , 다시 1번 누르면 자동모드 OFF
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isAutoFire = !isAutoFire;
        }

        // 쿨타임 적용
        CurrentCooldown -= Time.deltaTime;

        // 쿨타임이 끝났고 스페이스바를 누르거나 자동 발사 모드라면 발사
        if (CurrentCooldown <= 0 && (Input.GetKeyDown(KeyCode.Space) || isAutoFire))
        {
            Fire();

            // 업그레이드 수치를 적용한 쿨타임으로 초기화
            float finalFireRate = _fireCooldown - UpgradeManager.Instance.Upgrades[1].CurrentValue;
            CurrentCooldown = finalFireRate;
        }
    }

    private void Fire()
    {
        _fireSprite.SetActive(true);

        foreach (Transform firePoint in FirePoints)
        {
            Bullet bullet = BulletPool.Instance.GetBullet(BulletType.Main);
            bullet.transform.position = firePoint.position;
        }

        foreach (Transform firePoint in SubFirePoints)
        {
            Bullet subBullet = BulletPool.Instance.GetBullet(BulletType.Sub);
            subBullet.transform.position = firePoint.position;
        }
    }

    public void AutoFire()
    {
        Fire();

        float finalFireRate = _fireCooldown - UpgradeManager.Instance.Upgrades[1].CurrentValue;
        CurrentCooldown = finalFireRate;
    }

    public void FireRateUp(float upValue)
    {
        if (upValue < 0)
        {
            Debug.LogWarning("공격 속도 증가량은 0보다 작을 수 없습니다.");
            return;
        }

        // 최고 속도 제한
        _fireCooldown = Math.Max(_fireCooldown - upValue, MinCoolTime);
    }
}