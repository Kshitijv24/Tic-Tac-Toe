using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;
    [SerializeField] AudioClip clickSound;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.Log("There are more than one " + this.GetType() + " Instances", this);
            return;
        }
    }

    public void PlaySound(AudioClip clip, float volumeLevel) => audioSource.PlayOneShot(clip, volumeLevel);

    public void PlayClickSound(float volumeLevel) => audioSource.PlayOneShot(clickSound, 1f);

    public void PlayWinSound(float volumeLevel) => audioSource.PlayOneShot(winSound, 0.4f);

    public void PlayLoseSound(float volumeLevel) => audioSource.PlayOneShot(loseSound, 0.4f);
}
