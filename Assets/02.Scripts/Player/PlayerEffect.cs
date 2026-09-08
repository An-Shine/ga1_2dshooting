using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [Header("죽을때 폭발이펙트 프리펩")]
    [SerializeField]
    private GameObject _deathEffectPrefab;

    public void SpawnDeathEffect()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }
}