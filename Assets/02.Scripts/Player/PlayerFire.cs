using System.Collections.Generic;
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

    public float FireCooldown = 2.0f;
    public float CurrentCooldown;

    public bool isAutoFire = false;

    private void Start()
    {
        CurrentCooldown = FireCooldown;
    }

    private void Update()
    {
        // 쿨타임 적용
        CurrentCooldown -= Time.deltaTime;

        if (CurrentCooldown <= 0 && Input.GetKeyDown(KeyCode.Space) && isAutoFire != true)
        {
            Fire();

            // 쿨타이머 초기화 (중요)
            CurrentCooldown = FireCooldown;
        }


        // 1번 눌러서 자동발사 모드 설정 , 다시 1번 누르면 자동모드 OFF
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isAutoFire = !isAutoFire;
        }

        if (isAutoFire == true && CurrentCooldown <= 0)
        {
            AutoFire();
        }
    }

    private void Fire()
    {
        // 1. 스페이스바를 누르면 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 2. 총알 프리펩을 생성한다
            /*
            GameObject leftBullet = Instantiate(BulletPrefab);
            leftBullet.transform.position = LeftFirePoint.position;
            GameObject rightBullet = Instantiate(BulletPrefab);
            rightBullet.transform.position = RightFirePoint.position;
            */

            foreach (Transform firePoint in FirePoints)
            {
                GameObject bullet = Instantiate(BulletPrefab);
                bullet.transform.position = firePoint.position;
            }

            foreach (Transform firePoint in SubFirePoints)
            {
                GameObject bullet = Instantiate(SubBulletPrefabs);
                bullet.transform.position = firePoint.position;
            }
        }
    }

    private void AutoFire()
    {
        foreach (Transform firePoint in FirePoints)
        {
            GameObject bullet = Instantiate(BulletPrefab);
            bullet.transform.position = firePoint.position;
        }

        foreach (Transform firePoint in SubFirePoints)
        {
            GameObject bullet = Instantiate(SubBulletPrefabs);
            bullet.transform.position = firePoint.position;
        }

        CurrentCooldown = FireCooldown;
    }
}