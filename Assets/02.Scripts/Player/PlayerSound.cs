using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _dieClip;
    [SerializeField] private AudioClip _damagedClip;
    [SerializeField] private AudioClip _itemGetSound;

    public void PlayDieSound()
    {
        _audioSource.PlayOneShot(_dieClip);
    }

    public void PlayDamagedSound()
    {
        _audioSource.PlayOneShot(_damagedClip);
    }

    public void PlayItemGetSound()
    {
        _audioSource.PlayOneShot(_itemGetSound);
    }
}