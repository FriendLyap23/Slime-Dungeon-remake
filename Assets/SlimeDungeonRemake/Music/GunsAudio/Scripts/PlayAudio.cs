using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private AudioSource _audioClip;
    [SerializeField] private Weapon _weapon;

    private void OnEnable()
    {
        _weapon.OnBulletInitialize += PlaySounds;
    }

    private void PlaySounds() 
    {
        _audioClip.Play();
    }

    private void OnDisable()
    {
        _weapon.OnBulletInitialize -= PlaySounds;
    }
}
