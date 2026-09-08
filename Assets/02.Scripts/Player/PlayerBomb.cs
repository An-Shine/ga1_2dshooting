using UnityEngine;

public class PlayerBomb : MonoBehaviour
{
    [SerializeField] private GameObject _bomb;
    [SerializeField] private float _spawnCoolTime;

    [SerializeField] private float _duration = 3f;

    private void Update()
    {
        ActiveBomb();
    }

    public void ActiveBomb()
    {
        if (_spawnCoolTime > 0)
        {
            _spawnCoolTime -= Time.deltaTime;
        }

        if (_duration > 0)
        {
            _duration -= Time.deltaTime;
            if (_duration <= 0)
            {
                _bomb.SetActive(false);
            }
        }

        if (_spawnCoolTime <= 0 && Input.GetKeyDown(KeyCode.B))
        {
            _spawnCoolTime = 10f;
            _duration = 3f;
            _bomb.SetActive(true);
        }
    }
}