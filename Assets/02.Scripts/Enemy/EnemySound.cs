using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _dieClip;
    [SerializeField] private AudioClip _damagedClip;

    public void PlayDieSound()
    {
        _audioSource.PlayOneShot(_dieClip);
    }

    public void PlayDamagedSound()
    {
        _audioSource.PlayOneShot(_damagedClip);
    }
}